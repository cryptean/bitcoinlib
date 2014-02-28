// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using BitcoinLib.Auxiliary;
using BitcoinLib.ExceptionHandling;
using Newtonsoft.Json;

namespace BitcoinLib.RPC
{
    public sealed class RpcConnector : IRpcConnector
    {
        private readonly String _daemonUrl = !Boolean.Parse(ConfigurationManager.AppSettings.Get("UseTestNet"))
            ? ConfigurationManager.AppSettings.Get("DaemonUrl")
            : ConfigurationManager.AppSettings.Get("TestNetDaemonUrl");

        private readonly String _rpcUsername = ConfigurationManager.AppSettings.Get("RpcUsername");
        private readonly String _rpcPassword = ConfigurationManager.AppSettings.Get("RpcPassword");
        private readonly Int16 _rpcRequestTimeoutInSeconds = Int16.Parse(ConfigurationManager.AppSettings.Get("RpcRequestTimeoutInSeconds"));
        private readonly Boolean _rpcResendTimedOutRequests = Boolean.Parse(ConfigurationManager.AppSettings.Get("RpcResendTimedOutRequests"));
        private readonly Int16 _rpcTimedOutRequestsResendAttempts = Int16.Parse(ConfigurationManager.AppSettings.Get("RpcTimedOutRequestsResendAttempts"));
        private readonly Boolean _rpcDelayResendingTimedOutRequests = Boolean.Parse(ConfigurationManager.AppSettings.Get("RpcDelayResendingTimedOutRequests"));
        private readonly Boolean _rpcUseBase2ExpotentialDelaysWhenResendingTimedOutRequests = Boolean.Parse(ConfigurationManager.AppSettings.Get("RpcUseBase2ExpotentialDelaysWhenResendingTimedOutRequests"));

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
                if (_rpcResendTimedOutRequests && ++timedOutRequests <= _rpcTimedOutRequestsResendAttempts)
                {
                    //  Note: effective delay = delayInSeconds + _rpcRequestTimeoutInSeconds
                    if (_rpcDelayResendingTimedOutRequests)
                    {
                        Double delayInSeconds = _rpcUseBase2ExpotentialDelaysWhenResendingTimedOutRequests ? Math.Pow(2, timedOutRequests) : timedOutRequests;
                        Thread.Sleep((Int32) delayInSeconds*Constants.MillisecondsInASecond);
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
            HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(_daemonUrl);
            SetBasicAuthHeader(webRequest, _rpcUsername, _rpcPassword);
            webRequest.Credentials = new NetworkCredential(_rpcUsername, _rpcPassword);
            webRequest.ContentType = "application/json-rpc";
            webRequest.Method = "POST";
            webRequest.Timeout = _rpcRequestTimeoutInSeconds*Constants.MillisecondsInASecond;

            Byte[] byteArray = jsonRpcRequest.GetBytes();
            webRequest.ContentLength = byteArray.Length;

            try
            {
                using (Stream dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }
            }
            catch (Exception exception)
            {
                throw new RpcException("There was a problem sending the request to the bitcoin wallet", exception);
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
                throw new RpcResponseDeserializationException("There was a problem deserializing the response from the bitcoin wallet", jsonException);
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
                        reader.Close();
                        return result;
                    }
                }
            }
            catch (ProtocolViolationException protocolViolationException)
            {
                throw new RpcException("Unable to connect to the Bitcoin server", protocolViolationException);
            }
            catch (WebException webException)
            {
                HttpWebResponse webResponse = webException.Response as HttpWebResponse;

                if (webResponse != null)
                {
                    switch (webResponse.StatusCode)
                    {
                        case HttpStatusCode.InternalServerError:
                            throw new RpcException("The RPC request was either not understood by the Bitcoin server or there was a problem executing the request", webException);
                    }
                }
                    //  Qt RPC specific
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