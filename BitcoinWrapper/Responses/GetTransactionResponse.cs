// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;
using BitcoinLib.Responses.Bridges;

namespace BitcoinLib.Responses
{
    //  Note: Local wallet transactions only
    public class GetTransactionResponse : ITransactionResponse
    {
        public Decimal Amount { get; set; }
        public String Blockhash { get; set; }
        public Int32 BlockIndex { get; set; }
        public Int32 BlockTime { get; set; }
        public Int32 Confirmations { get; set; }
        public List<GetTransactionResponseDetails> Details { get; set; }
        public String Hex { get; set; }
        public Int32 Time { get; set; }
        public Int32 TimeReceived { get; set; }
        public List<String> WalletConflicts { get; set; }
        public String TxId { get; set; }
    }

    public class GetTransactionResponseDetails
    {
        public String Account { get; set; }
        public String Address { get; set; }
        public Decimal Amount { get; set; }
        public String Category { get; set; }
    }
}