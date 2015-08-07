// Copyright (c) 2015 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.Responses
{
    public class GetChainTipsResponse
    {
        public UInt32 Height { get; set; }
        public String Hash { get; set; }
        public Int32 BranchLen { get; set; }
        public String Status { get; set; }
    }
}
