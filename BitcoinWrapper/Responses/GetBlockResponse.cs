using System;
using System.Collections.Generic;

namespace BitcoinLib.Responses
{
    public class GetBlockResponse
    {
        public GetBlockResponse()
        {
            Tx = new List<String>();
        }

        public List<String> Tx { get; set; }
        public String Hash { get; set; }
        public Int32 Confirmations { get; set; }
        public Int32 Size { get; set; }
        public Int32 Height { get; set; }
        public Int32 Version { get; set; }
        public String MerkleRoot { get; set; }
        public Double Difficulty { get; set; }
        public String PreviousBlockHash { get; set; }
        public String NextBlockHash { get; set; }
        public String Bits { get; set; }
        public Int32 Time { get; set; }
        public String Nonce { get; set; }
    }
}