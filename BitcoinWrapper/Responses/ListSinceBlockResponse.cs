using System;
using System.Collections.Generic;

namespace BitcoinLib.Responses
{
    public class ListSinceBlockResponse
    {
        public List<TransactionSinceBlock> Transactions { get; set; }
        public String Lastblock { get; set; }
    }

    public class TransactionSinceBlock
    {
        public String Account { get; set; }
        public String Address { get; set; }
        public String Category { get; set; }
        public Double Amount { get; set; }
        public Int32 Confirmations { get; set; }
        public String TxId { get; set; }
        public Int32 Time { get; set; }
        public Int32 TimeReceived { get; set; }
    }
}