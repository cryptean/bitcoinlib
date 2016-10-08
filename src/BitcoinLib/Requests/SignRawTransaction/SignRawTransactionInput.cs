// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

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