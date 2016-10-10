// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System.Collections.Generic;

namespace BitcoinLib.Responses
{
    public class GetBlockResponse
    {
        public GetBlockResponse()
        {
            Tx = new List<string>();
        }

        public List<string> Tx { get; set; }
        public string Hash { get; set; }
        public int Confirmations { get; set; }
        public int Size { get; set; }
        public int Height { get; set; }
        public int Version { get; set; }
        public string MerkleRoot { get; set; }
        public double Difficulty { get; set; }
        public string ChainWork { get; set; }
        public string PreviousBlockHash { get; set; }
        public string NextBlockHash { get; set; }
        public string Bits { get; set; }
        public int Time { get; set; }
        public string Nonce { get; set; }
    }
}