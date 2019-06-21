// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using BitcoinLib.Responses.SharedComponents;
using System.Collections.Generic;

namespace BitcoinLib.Responses
{
    public abstract class GetBlockResponseBase
    {
        public string Hash { get; set; }
        public int Confirmations { get; set; }
        public int Size { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int Version { get; set; }
        public string MerkleRoot { get; set; }
        public double Difficulty { get; set; }
        public string ChainWork { get; set; }
        public string PreviousBlockHash { get; set; }
        public string NextBlockHash { get; set; }
        public string Bits { get; set; }
        public int Time { get; set; }
        public int MedianTime { get; set; }
        public string Nonce { get; set; }
    }

    public class GetBlockResponse : GetBlockResponseBase
    {
        public GetBlockResponse()
        {
            Tx = new List<string>();
        }

        public List<string> Tx { get; set; }
    }

    public class GetBlockResponseVerbose : GetBlockResponseBase
    {
        public GetBlockResponseVerbose()
        {
            Tx = new List<IncludedTransaction>();
        }

        public List<IncludedTransaction> Tx { get; set; }
    }

    public class IncludedTransaction
    {
        public string Hex { get; set; }
        public long Version { get; set; }
        public uint LockTime { get; set; }
        public List<Vin> Vin { get; set; }
        public List<Vout> Vout { get; set; }
        public string TxId { get; set; }
        public int Size { get; set; }
        public int VSize { get; set; }
        public int Weight { get; set; }
    }

}