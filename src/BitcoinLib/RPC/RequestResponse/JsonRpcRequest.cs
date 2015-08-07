// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace BitcoinLib.RPC.RequestResponse
{
    public class JsonRpcRequest
    {
        public JsonRpcRequest(Int32 id, String method, params object[] parameters)
        {
            Id = id;
            Method = method;
            Parameters = parameters != null ? parameters.ToList() : new List<object>();
        }

        [JsonProperty(PropertyName = "method", Order = 0)]
        public String Method { get; set; }

        [JsonProperty(PropertyName = "params", Order = 1)]
        public IList<object> Parameters { get; set; }

        [JsonProperty(PropertyName = "id", Order = 2)]
        public Int32 Id { get; set; }

        public Byte[] GetBytes()
        {
            String json = JsonConvert.SerializeObject(this);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}