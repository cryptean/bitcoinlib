 // Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

namespace BitcoinLib.Responses
{
    public class GetBlockchainInfoResponse
    {
        public string Chain { get; set; }
        public ulong Blocks { get; set; }
        public ulong Headers { get; set; }
        public string BestBlockHash { get; set; }
        public double Difficulty { get; set; }
        public double VerificationProgress { get; set; }
        public string ChainWork { get; set; }
        public bool Pruned { get; set; }
    }
}