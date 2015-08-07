// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;

namespace BitcoinLib.Responses
{
    public class ListTransactionsResponse
    {
        public String Account { get; set; }
        public String Address { get; set; }
        public String Category { get; set; }
        public Decimal Amount { get; set; }
        public Int32 Vout { get; set; }
        public Decimal Fee { get; set; }
        public Int32 Confirmations { get; set; }
        public String BlockHash { get; set; }
        public Double BlockIndex { get; set; }
        public Double BlockTime { get; set; }
        public String TxId { get; set; }
        public List<String> WalletConflicts { get; set; }
        public Double Time { get; set; }
        public Double TimeReceived { get; set; }
        public String Comment { get; set; }
        public String OtherAccount { get; set; }
        public Boolean InvolvesWatchonly { get; set; }
    }
}