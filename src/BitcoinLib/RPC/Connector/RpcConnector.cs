// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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

        public async Task<T> MakeRequestAsync<T>(RpcMethods rpcMethod, params object[] parameters)
        {
            if (!_init)
            {
                var @params = _coinService.Parameters;
                InitHttpClient(@params.SelectedDaemonUrl, @params.RpcUsername, @params.RpcPassword);
                _init = true;
            }

            var jsonRpcRequest = new JsonRpcRequest(1, rpcMethod.ToString(), parameters);
            var byteContent = new ByteArrayContent(jsonRpcRequest.GetBytes());
            byteContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json-rpc");
            byteContent.Headers.ContentLength = jsonRpcRequest.GetBytes().Length;

            try
            {
                var res = await _httpClient.PostAsync(_coinService.Parameters.SelectedDaemonUrl, byteContent);

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

                                if (json == "The operation has timed out")
                                    throw new RpcRequestTimeoutException(json);
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
                var queryParameters = jsonRpcRequest.Parameters.Cast<string>().Aggregate(string.Empty, (current, parameter) => current + (parameter + " "));
                throw new Exception($"A problem was encountered while calling MakeRpcRequest() for: {jsonRpcRequest.Method} with parameters: {queryParameters}. \nException: {exception.Message}");
            }

        }

        private void InitHttpClient(string daemonUrl, string rpcUser, string rpcPassword)
        {
            _httpClient = new HttpClient();
            var authBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(rpcUser + ":" + rpcPassword));
            var auth = new AuthenticationHeaderValue("Basic", authBase64);
            _httpClient.DefaultRequestHeaders.Authorization = auth;
            _httpClient.Timeout = TimeSpan.FromSeconds(_coinService.Parameters.RpcRequestTimeoutInSeconds);
        }
    }
}