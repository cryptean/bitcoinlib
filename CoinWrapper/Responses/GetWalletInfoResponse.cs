// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using Newtonsoft.Json;

namespace BitcoinLib.Responses
{
    public class GetWalletInfoResponse
    {
        public String WalletVersion { get; set; }
        public Decimal Balance { get; set; }
        public UInt64 TxCount { get; set; }
        public Double KeyPoolOldest { get; set; }

        [JsonProperty("unlocked_until")]
        public UInt64 UnlockedUntil { get; set; }
    }
}