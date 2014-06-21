// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.Responses
{
    public class GetBlockchainInfoResponse
    {
        public String Chain { get; set; }
        public UInt64 Blocks { get; set; }
        public String BestBlockHash { get; set; }
        public Double Difficulty { get; set; }
        public Double VerificationProgress { get; set; }
        public String ChainWork { get; set; }
    }
}