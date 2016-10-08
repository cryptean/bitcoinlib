// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

namespace BitcoinLib.Responses
{
    public class CreateMultiSigResponse
    {
        public string Address { get; set; }
        public string RedeemScript { get; set; }
    }
}