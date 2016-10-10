// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

namespace BitcoinLib.Responses
{
    public class ListAddressGroupingsResponse
    {
        public string Address { get; set; }
        public decimal Balance { get; set; }
        public string Account { get; set; }
    }
}