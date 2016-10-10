// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

namespace BitcoinLib.Responses
{
    public class GetTxOutSetInfoResponse
    {
        public int Height { get; set; }
        public string BestBlock { get; set; }
        public int Transactions { get; set; }
        public int TxOuts { get; set; }
        public int BytesSerialized { get; set; }
        public string HashSerialized { get; set; }
        public double TotalAmount { get; set; }
    }
}