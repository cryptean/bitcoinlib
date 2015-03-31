// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.Requests.CreateRawTransaction
{
    public class CreateRawTransactionInput
    {
        public String TxId { get; set; }
        public Int32 Vout { get; set; }
    }
}