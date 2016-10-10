// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

namespace BitcoinLib.Responses
{
    public class SignRawTransactionResponse
    {
        public string Hex { get; set; }
        public bool Complete { get; set; }
    }
}