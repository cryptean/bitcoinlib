// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

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