// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.Responses
{
    public class ListTransactionsResponse
    {
        public String Account { get; set; }
        public String Address { get; set; }
        public String Category { get; set; }
        public Decimal Amount { get; set; }
        public Int32 Confirmations { get; set; }
        public String BlockHash { get; set; }
        public Double BlockIndex { get; set; }
        public Double BlockTime { get; set; }
        public String TxId { get; set; }
        public Double Time { get; set; }
        public Double TimeReceived { get; set; }
    }
}