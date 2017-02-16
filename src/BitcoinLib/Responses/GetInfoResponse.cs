// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using Newtonsoft.Json;

namespace BitcoinLib.Responses
{
    public class GetInfoResponse
    {
        public string Version { get; set; }
        public string ProtocolVersion { get; set; }
        public string WalletVersion { get; set; }
        public decimal Balance { get; set; }
        public long Blocks { get; set; }
        public long TimeOffset { get; set; }
        public int Connections { get; set; }
        public string Proxy { get; set; }
        [JsonIgnore]
        public double Difficulty { get; set; }
        public bool Testnet { get; set; }
        public long KeyPoolEldest { get; set; }
        public long KeyPoolSize { get; set; }

        [JsonProperty("unlocked_until")]
        public long UnlockedUntil { get; set; }

        [JsonIgnore]
        [JsonProperty("moneysupply")]
        public decimal MoneySupply { get; set; }

        public decimal PayTxFee { get; set; }
        public decimal RelayTxFee { get; set; }
        public string Errors { get; set; }
    }
}