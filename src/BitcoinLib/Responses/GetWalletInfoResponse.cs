// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using Newtonsoft.Json;

namespace BitcoinLib.Responses
{
    public class GetWalletInfoResponse
    {
        public string WalletVersion { get; set; }
        public decimal Balance { get; set; }

        [JsonProperty("unconfirmed_balance")]
        public decimal UnconfirmedBalance { get; set; }

        [JsonProperty("immature_balance")]
        public decimal ImmatureBalance { get; set; }

        public ulong TxCount { get; set; }
        public double KeyPoolOldest { get; set; }

        [JsonProperty("unlocked_until")]
        public ulong UnlockedUntil { get; set; }

        public ulong KeyPoolSize { get; set; }
    }
}