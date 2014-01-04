using System;
using Newtonsoft.Json;

namespace BitcoinWrapper.RPC
{
    public class JsonRpcResponse<T>
    {
        public JsonRpcResponse(Int32 id, String error, T result)
        {
            Id = id;
            Error = error;
            Result = result;
        }

        [JsonProperty(PropertyName = "result", Order = 0)]
        public T Result { get; set; }

        [JsonProperty(PropertyName = "id", Order = 1)]
        public Int32 Id { get; set; }

        [JsonProperty(PropertyName = "error", Order = 2)]
        public String Error { get; set; }
    }
}
