// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;

namespace BitcoinLib.Responses
{
    public class ListReceivedByAddressResponse
    {
        public String Account { get; set; }
        public String Address { get; set; }
        public Decimal Amount { get; set; }
        public Int32 Confirmations { get; set; }
        public List<String> TxIds { get; set; }
    }
}