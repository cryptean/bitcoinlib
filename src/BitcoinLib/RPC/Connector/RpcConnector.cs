// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BitcoinLib.ExceptionHandling.Rpc;
using BitcoinLib.RPC.RequestResponse;
using BitcoinLib.RPC.Specifications;
using BitcoinLib.Services.Coins.Base;
using Newtonsoft.Json;

namespace BitcoinLib.RPC.Connector
{
    public sealed class RpcConnector : IRpcConnector
    {
        private readonly ICoinService _coinService;
        private HttpClient _httpClient;
        private bool _init = false;

        public RpcConnector(ICoinService coinService)
        {
            _coinService = coinService;
        }

        public T MakeRequest<T>(RpcMethods rpcMethod, params object[] parameters)
        {
            return MakeRequestAsync<T>(rpcMethod, parameters).Result;
        }

        public async Task<T> MakeRequestAsync<T>(RpcMethods rpcMethod, params object[] parameters)
        {
            if (!_init)
            {
                var @params = _coinService.Parameters;
                InitHttpClient(@params.RpcUsername, @params.RpcPassword);
                _init = true;
            }

            var jsonRpcRequest = new JsonRpcRequest(1, rpcMethod.ToString(), parameters);
            var byteContent = new ByteArrayContent(jsonRpcRequest.GetBytes());
            byteContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json-rpc");
            byteContent.Headers.ContentLength = jsonRpcRequest.GetBytes().Length;
            var cancelTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(_coinService.Parameters.RpcRequestTimeoutInSeconds));
            HttpResponseMessage res;
            try
            {
                res = await _httpClient.PostAsync(_coinService.Parameters.SelectedDaemonUrl, byteContent, cancelTokenSource.Token);

                if (res.StatusCode == HttpStatusCode.OK)
                {
                    var json = await res.Content.ReadAsStringAsync();
                    var rpcResponse = JsonConvert.DeserializeObject<JsonRpcResponse<T>>(json);
                    return rpcResponse.Result;
                }
                else
                    switch (res.StatusCode)
                    {
                        case HttpStatusCode.InternalServerError:
                            var stream = await res.Content.ReadAsStreamAsync();

                            if (stream != null)
                            {
                                var json = await res.Content.ReadAsStringAsync();

                                try
                                {
                                    var jsonRpcResponseObject =
                                        JsonConvert.DeserializeObject<JsonRpcResponse<object>>(json);

                                    var internalServerErrorException =
                                        new RpcInternalServerErrorException(jsonRpcResponseObject.Error.Message)
                                        {
                                            RpcErrorCode = jsonRpcResponseObject.Error.Code
                                        };

                                    throw internalServerErrorException;
                                }
                                catch (JsonException)
                                {
                                    throw new RpcException(json);
                                }
                            }
                            else goto default;
                        default:
                            throw new RpcException(
                                "The RPC request was either not understood by the server or there was a problem executing the request");
                    }
                //throw new RpcException("Unable to connect to the server", protocolViolationException);
            }

            catch (Exception exception)
            {
                if (cancelTokenSource.IsCancellationRequested)
                    throw new RpcRequestTimeoutException("The operation has timed out");

                var queryParameters = jsonRpcRequest.Parameters.Aggregate(string.Empty, (current, p) => current + p.ToString() + " ");
                throw new Exception($"A problem was encountered while calling MakeRpcRequest() for: {jsonRpcRequest.Method} with parameters: {queryParameters}. \nException: {exception.Message}");
            }

        }

        private void InitHttpClient(string rpcUser, string rpcPassword)
        {
            _httpClient = new HttpClient();
            var authBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(rpcUser + ":" + rpcPassword));
            var auth = new AuthenticationHeaderValue("Basic", authBase64);
            _httpClient.DefaultRequestHeaders.Authorization = auth;
            _httpClient.Timeout = TimeSpan.FromSeconds(_coinService.Parameters.RpcRequestTimeoutInSeconds);
        }
    }
}