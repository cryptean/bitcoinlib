// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System.Collections.Generic;
using BitcoinLib.Responses.Bridges;

namespace BitcoinLib.Responses
{
    //  Note: Local wallet transactions only
    public class GetTransactionResponse : ITransactionResponse
    {
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public string BlockHash { get; set; }
        public int BlockIndex { get; set; }
        public int BlockTime { get; set; }
        public int Confirmations { get; set; }
        public List<GetTransactionResponseDetails> Details { get; set; }
        public string Hex { get; set; }
        public int Time { get; set; }
        public int TimeReceived { get; set; }
        public List<string> WalletConflicts { get; set; }
        public string TxId { get; set; }
    }

    public class GetTransactionResponseDetails
    {
        public string Account { get; set; }
        public string Address { get; set; }
        public decimal Amount { get; set; }
        public string Label { get; set; }
        public decimal Fee { get; set; }
        public int Vout { get; set; }
        public string Category { get; set; }
    }
}