// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;

namespace BitcoinLib.Responses.SharedComponents
{
    public class Vout
    {
        public Decimal Value { get; set; }
        public Int32 N { get; set; }
        public ScriptPubKey ScriptPubKey { get; set; }
    }

    public class ScriptPubKey
    {
        public String Asm { get; set; }
        public String Hex { get; set; }
        public Int32 ReqSigs { get; set; }
        public String Type { get; set; }
        public List<String> Addresses { get; set; }
    }
}
