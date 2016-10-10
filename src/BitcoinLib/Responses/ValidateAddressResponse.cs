// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

namespace BitcoinLib.Responses
{
    public class ValidateAddressResponse
    {
        public bool IsValid { get; set; }
        public string Address { get; set; }
        public bool IsMine { get; set; }
        public bool IsScript { get; set; }
        public string PubKey { get; set; }
        public bool IsCompressed { get; set; }
        public string Account { get; set; }
    }
}