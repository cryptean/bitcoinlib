using System;

namespace BitcoinWrapper.Responses
{
    public class GetInfoResponse
    {
        public String Version { get; set; }
        public String ProtocolVersion { get; set; }
        public String WalletVersion { get; set; }
        public Decimal Balance { get; set; }
        public Double Blocks { get; set; }
        public Double TimeOffset { get; set; }
        public Double Connections { get; set; }
        public String Proxy { get; set; }
        public Double Difficulty { get; set; }
        public Boolean Testnet { get; set; }
        public Double KeyPoolEldest { get; set; }
        public Double KeyPoolSize { get; set; }
        public Decimal PayTxFee { get; set; }
        public String Errors { get; set; }
    }
}
