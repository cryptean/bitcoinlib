// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

namespace BitcoinLib.Responses
{
    public class DecodeScriptResponse
    {
        public string Asm { get; set; }
        public string P2SH { get; set; }
        public string Type { get; set; }
    }
}