// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.Responses
{
    public class DecodeScriptResponse
    {
        public String Asm { get; set; }
        public String P2SH { get; set; }
        public String Type { get; set; }
    }
}