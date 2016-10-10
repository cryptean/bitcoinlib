// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

namespace BitcoinLib.Responses
{
    public class GetChainTipsResponse
    {
        public uint Height { get; set; }
        public string Hash { get; set; }
        public int BranchLen { get; set; }
        public string Status { get; set; }
    }
}