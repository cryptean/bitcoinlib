// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using Newtonsoft.Json;

namespace BitcoinLib.Responses
{
    public class GetInfoResponse
    {
        public string Version { get; set; }
        public string ProtocolVersion { get; set; }
        public string WalletVersion { get; set; }
        public decimal Balance { get; set; }
        public double Blocks { get; set; }
        public double TimeOffset { get; set; }
        public double Connections { get; set; }
        public string Proxy { get; set; }
        public double Difficulty { get; set; }
        public bool Testnet { get; set; }
        public double KeyPoolEldest { get; set; }
        public double KeyPoolSize { get; set; }

        [JsonProperty("unlocked_until")]
        public ulong UnlockedUntil { get; set; }

        public decimal PayTxFee { get; set; }
        public decimal RelayTxFee { get; set; }
        public string Errors { get; set; }
    }
}