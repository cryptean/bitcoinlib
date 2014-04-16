// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.Responses
{
    public class ListUnspentResponse
    {
        public String TxId { get; set; }
        public Int32 VOut { get; set; }
        public String Address { get; set; }
        public String Account { get; set; }
        public String ScriptPubKey { get; set; }
        public Decimal Amount { get; set; }
        public Int32 Confirmations { get; set; }

        public override String ToString()
        {
            return String.Format("Account: {0}, Address: {1}, Amount: {2}, Confirmations: {3}", Account, Address, Amount, Confirmations);
        }
    }
}