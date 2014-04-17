// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using BitcoinLib.Auxiliary;
using BitcoinLib.ExceptionHandling.Rpc;
using BitcoinLib.RPC.RequestResponse;
using BitcoinLib.Services.Coins.Base;
using Newtonsoft.Json;

namespace BitcoinLib.RPC.Connector
{
    public sealed class RpcConnector : IRpcConnector
    {
        private readonly ICoinService _coinService;

        public RpcConnector(ICoinService coinService)
        {
            _coinService = coinService;
        }

        public T MakeRequest<T>(RpcMethods rpcMethod, params object[] parameters)
        {
            JsonRpcResponse<T> rpcResponse = MakeRpcRequest<T>(new JsonRpcRequest(1, rpcMethod.ToString(), parameters), 0);
            return rpcResponse.Result;
        }

        private JsonRpcResponse<T> MakeRpcRequest<T>(JsonRpcRequest jsonRpcRequest, Int16 timedOutRequests)
        {
            JsonRpcResponse<T> response;

            try
            {
                HttpWebRequest httpWebRequest = MakeHttpRequest(jsonRpcRequest);
                response = GetRpcResponse<T>(httpWebRequest);
            }
            catch (RpcRequestTimeoutException rpcRequestTimeoutException)
            {
                if (_coinService.Parameters.RpcResendTimedOutRequests && ++timedOutRequests <= _coinService.Parameters.RpcTimedOutRequestsResendAttempts)
                {
                    //  Note: effective delay = delayInSeconds + _rpcRequestTimeoutInSeconds
                    if (_coinService.Parameters.RpcDelayResendingTimedOutRequests)
                    {
                        Double delayInSeconds = _coinService.Parameters.RpcUseBase2ExponentialDelaysWhenResendingTimedOutRequests ? Math.Pow(2, timedOutRequests) : timedOutRequests;

                        if (Debugger.IsAttached)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("RPC request timeout: {0}, will resend {1} of {2} total attempts after {3} seconds (request timeout: {4} seconds)", jsonRpcRequest.Method, timedOutRequests, _coinService.Parameters.RpcTimedOutRequestsResendAttempts, delayInSeconds, _coinService.Parameters.RpcRequestTimeoutInSeconds);
                            Console.ResetColor();
                        }

                        Thread.Sleep((Int32) delayInSeconds * GlobalConstants.MillisecondsInASecond);
                    }

                    return MakeRpcRequest<T>(jsonRpcRequest, timedOutRequests);
                }

                throw rpcRequestTimeoutException;
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return response;
        }

        private HttpWebRequest MakeHttpRequest(JsonRpcRequest jsonRpcRequest)
        {
            HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(_coinService.Parameters.SelectedDaemonUrl);
            SetBasicAuthHeader(webRequest, _coinService.Parameters.RpcUsername, _coinService.Parameters.RpcPassword);
            webRequest.Credentials = new NetworkCredential(_coinService.Parameters.RpcUsername, _coinService.Parameters.RpcPassword);
            webRequest.ContentType = "application/json-rpc";
            webRequest.Method = "POST";
            webRequest.Timeout = _coinService.Parameters.RpcRequestTimeoutInSeconds * GlobalConstants.MillisecondsInASecond;
            Byte[] byteArray = jsonRpcRequest.GetBytes();
            webRequest.ContentLength = byteArray.Length;

            try
            {
                using (Stream dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new RpcException("There was a problem sending the request to the wallet", exception);
            }

            return webRequest;
        }

        private JsonRpcResponse<T> GetRpcResponse<T>(HttpWebRequest httpWebRequest)
        {
            String json = GetJsonResponse(httpWebRequest);

            try
            {
                return JsonConvert.DeserializeObject<JsonRpcResponse<T>>(json);
            }
            catch (JsonException jsonException)
            {
                throw new RpcResponseDeserializationException("There was a problem deserializing the response from the wallet", jsonException);
            }
        }

        private static String GetJsonResponse(HttpWebRequest httpWebRequest)
        {
            try
            {
                WebResponse webResponse = httpWebRequest.GetResponse();

                using (Stream stream = webResponse.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        String result = reader.ReadToEnd();
                        reader.Dispose();
                        return result;
                    }
                }
            }
            catch (ProtocolViolationException protocolViolationException)
            {
                throw new RpcException("Unable to connect to the server", protocolViolationException);
            }
            catch (WebException webException)
            {
                HttpWebResponse webResponse = webException.Response as HttpWebResponse;

                if (webResponse != null)
                {
                    switch (webResponse.StatusCode)
                    {
                        case HttpStatusCode.InternalServerError:
                            throw new RpcException("The RPC request was either not understood by the server or there was a problem executing the request", webException);
                    }
                }
                else if (webException.Message == "The operation has timed out")
                {
                    throw new RpcRequestTimeoutException(webException.Message);
                }

                throw new RpcException("An unknown web exception occured while trying to read the JSON response", webException);
            }
            catch (Exception exception)
            {
                throw new RpcException("An unknown exception occured while trying to read the JSON response", exception);
            }
        }

        private static void SetBasicAuthHeader(WebRequest webRequest, String username, String password)
        {
            String authInfo = username + ":" + password;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            webRequest.Headers["Authorization"] = "Basic " + authInfo;
        }
    }
}