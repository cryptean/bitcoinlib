// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using Newtonsoft.Json;

namespace BitcoinLib.Requests.CreateRawTransaction
{
    public class CreateRawTransactionInput
    {
        [JsonProperty("txid")]
        public string TxId { get; set; }

        [JsonProperty("vout")]
        public int Vout { get; set; }
    }
}