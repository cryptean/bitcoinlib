// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

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