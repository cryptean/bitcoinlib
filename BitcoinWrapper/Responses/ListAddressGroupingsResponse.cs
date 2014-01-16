// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.Responses
{
    public class ListAddressGroupingsResponse
    {
        public String Address { get; set; }
        public Decimal Balance { get; set; }
        public String Account { get; set; }
    }
}