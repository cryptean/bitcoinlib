// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using Newtonsoft.Json;

namespace BitcoinLib.RPC.RequestResponse
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