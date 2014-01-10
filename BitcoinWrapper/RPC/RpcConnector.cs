using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using BitcoinWrapper.ExceptionHandling;
using Newtonsoft.Json;

namespace BitcoinWrapper.RPC
{
    public sealed class RpcConnector : IRpcConnector
    {
        private readonly String _daemonUrl = !Boolean.Parse(ConfigurationManager.AppSettings.Get("UseTestNet")) ? ConfigurationManager.AppSettings.Get("DaemonUrl") : ConfigurationManager.AppSettings.Get("TestNetDaemonUrl");
        private readonly String _rpcUsername = ConfigurationManager.AppSettings.Get("RpcUsername");
        private readonly String _rpcPassword = ConfigurationManager.AppSettings.Get("RpcPassword");

        public T MakeRequest<T>(RpcMethods rpcMethod, params object[] parameters)
        {
            JsonRpcResponse<T> rpcResponse = MakeRpcRequest<T>(new JsonRpcRequest(1, rpcMethod.ToString(), parameters));
            return rpcResponse.Result;
        }

        private JsonRpcResponse<T> MakeRpcRequest<T>(JsonRpcRequest jsonRpcRequest)
        {
            HttpWebRequest httpWebRequest = MakeHttpRequest(jsonRpcRequest);
            return GetRpcResponse<T>(httpWebRequest);
        }

        private HttpWebRequest MakeHttpRequest(JsonRpcRequest jsonRpcRequest)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(_daemonUrl);
            SetBasicAuthHeader(webRequest, _rpcUsername, _rpcPassword);
            webRequest.Credentials = new NetworkCredential(_rpcUsername, _rpcPassword);
            webRequest.ContentType = "application/json-rpc";
            webRequest.Method = "POST";
            webRequest.Timeout = 2000;

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
                throw new RpcException("There was a problem deserializing the response from the bitcoin wallet", jsonException);
            }
        }

        private static String GetJsonResponse(HttpWebRequest httpWebRequest)
        {
            try
            {
                WebResponse webResponse = httpWebRequest.GetResponse();

                // Deserialize the json response
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
                throw new RpcException("An unknown web exception occured while trying to read the JSON response", webException);
            }
            catch (Exception exception)
            {
                throw new RpcException("An unknown exception occured while trying to read the JSON response", exception);
            }
        }

        private static void SetBasicAuthHeader(WebRequest request, String userName, String userPassword)
        {
            string authInfo = userName + ":" + userPassword;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;
        }
    }
}
