// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

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
        public Decimal Amount { get; set; }
        public Decimal Fee { get; set; }
        public Int32 Confirmations { get; set; }
        public String BlockHash { get; set; }
        public Int64 BlockIndex { get; set; }
        public Int32 BlockTime { get; set; }
        public String TxId { get; set; }
        public Int32 Time { get; set; }
        public Int32 TimeReceived { get; set; }
        public String Comment { get; set; }
        public String To { get; set; }
    }

    //  Note: Do not alter the capitalization of the enum members
    public enum TransactionSinceBlockCategory
    {
        send,
        receive
    }
}