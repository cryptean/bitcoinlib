// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using Newtonsoft.Json;

namespace BitcoinLib.RPC.RequestResponse
{
    public class JsonRpcResponse<T>
    {
        [JsonProperty(PropertyName = "result", Order = 0)]
        public T Result { get; set; }

        [JsonProperty(PropertyName = "id", Order = 1)]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "error", Order = 2)]
        public JsonRpcError Error { get; set; }
    }
}