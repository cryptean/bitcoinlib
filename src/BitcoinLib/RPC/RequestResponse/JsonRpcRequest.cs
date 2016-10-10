// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace BitcoinLib.RPC.RequestResponse
{
    public class JsonRpcRequest
    {
        public JsonRpcRequest(int id, string method, params object[] parameters)
        {
            Id = id;
            Method = method;
            Parameters = parameters?.ToList() ?? new List<object>();
        }

        [JsonProperty(PropertyName = "method", Order = 0)]
        public string Method { get; set; }

        [JsonProperty(PropertyName = "params", Order = 1)]
        public IList<object> Parameters { get; set; }

        [JsonProperty(PropertyName = "id", Order = 2)]
        public int Id { get; set; }

        public byte[] GetBytes()
        {
            var json = JsonConvert.SerializeObject(this);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}