// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System.Collections.Generic;

namespace BitcoinLib.Responses
{
    public class ListTransactionsResponse
    {
        public string Account { get; set; }
        public string Address { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public int Vout { get; set; }
        public decimal Fee { get; set; }
        public int Confirmations { get; set; }
        public string BlockHash { get; set; }
        public double BlockIndex { get; set; }
        public double BlockTime { get; set; }
        public string TxId { get; set; }
        public List<string> WalletConflicts { get; set; }
        public double Time { get; set; }
        public double TimeReceived { get; set; }
        public string Comment { get; set; }
        public string OtherAccount { get; set; }
        public bool InvolvesWatchonly { get; set; }
    }
}