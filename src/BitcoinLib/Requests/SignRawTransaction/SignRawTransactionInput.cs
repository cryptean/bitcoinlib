// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using Newtonsoft.Json;

namespace BitcoinLib.Requests.SignRawTransaction
{
    public class SignRawTransactionInput
    {
        [JsonProperty(PropertyName = "txid", Order = 0)]
        public string TxId { get; set; }

        [JsonProperty(PropertyName = "vout", Order = 1)]
        public int Vout { get; set; }

        [JsonProperty(PropertyName = "scriptPubKey", Order = 2)]
        public string ScriptPubKey { get; set; }

        [JsonProperty(PropertyName = "redeemScript", Order = 3)]
        public string RedeemScript { get; set; }
    }
}