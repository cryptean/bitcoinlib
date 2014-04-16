// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using Newtonsoft.Json;

namespace BitcoinLib.Requests.CreateRawTransaction
{
    public class CreateRawTransactionInput
    {
        [JsonProperty("txid")]
        public String TxId { get; set; }

        [JsonProperty("vout")]
        public Int32 Vout { get; set; }
    }
}