// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.Responses
{
    public class GetMiningInfoResponse
    {
        public Int32 Blocks { get; set; }
        public Int32 CurrentBockSize { get; set; }
        public Int32 CurrentBlockTx { get; set; }
        public Double Difficulty { get; set; }
        public String Errors { get; set; }
        public Boolean Generate { get; set; }
        public Int32 GenProcLimit { get; set; }
        public Int32 HashesPerSec { get; set; }
        public Int32 PooledTx { get; set; }
        public Boolean Testnet { get; set; }
    }
}