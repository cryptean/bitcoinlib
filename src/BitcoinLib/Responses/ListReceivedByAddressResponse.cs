// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System.Collections.Generic;

namespace BitcoinLib.Responses
{
    public class ListReceivedByAddressResponse
    {
        public string Account { get; set; }
        public string Address { get; set; }
        public decimal Amount { get; set; }
        public int Confirmations { get; set; }
        public List<string> TxIds { get; set; }
    }
}