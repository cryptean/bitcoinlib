// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

namespace BitcoinLib.Responses
{
    public class GetMiningInfoResponse
    {
        public int Blocks { get; set; }
        public int CurrentBockSize { get; set; }
        public int CurrentBlockTx { get; set; }
        public double Difficulty { get; set; }
        public string Errors { get; set; }
        public int GenProcLimit { get; set; }
        public long NetworkHashPS { get; set; }
        public int PooledTx { get; set; }
        public bool Testnet { get; set; }
        public string Chain { get; set; }
        public bool Generate { get; set; }
        public long HashesPerSec { get; set; }
    }
}