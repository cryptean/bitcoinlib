// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.Responses.SharedComponents
{
    public class Vin
    {
        public String TxId { get; set; }
        public String Vout { get; set; }
        public ScriptSig ScriptSig { get; set; }
        public String Sequence { get; set; }
    }

    public class ScriptSig
    {
        public String Asm { get; set; }
        public String Hex { get; set; }
    }
}
