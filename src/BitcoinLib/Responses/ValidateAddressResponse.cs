// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.Responses
{
    public class ValidateAddressResponse
    {
        public Boolean IsValid { get; set; }
        public String Address { get; set; }
        public Boolean IsMine { get; set; }
        public Boolean IsScript { get; set; }
        public String PubKey { get; set; }
        public Boolean IsCompressed { get; set; }
        public String Account { get; set; }
    }
}