// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using Newtonsoft.Json;

namespace BitcoinLib.Requests.SignRawTransaction
{
    public class SignRawTransactionInput
    {
        [JsonProperty(PropertyName = "txid", Order = 0)]
        public String TransactionId { get; set; }

        [JsonProperty(PropertyName = "vout", Order = 1)]
        public Int32 Output { get; set; }

        [JsonProperty(PropertyName = "scriptPubKey", Order = 2)]
        public String ScriptPubKey { get; set; }

        [JsonProperty(PropertyName = "redeemScript", Order = 3)]
        public String RedeemScript { get; set; }
    }
}