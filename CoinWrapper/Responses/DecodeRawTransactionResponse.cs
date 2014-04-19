// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;
using BitcoinLib.Responses.Bridges;

namespace BitcoinLib.Responses
{
    public class DecodeRawTransactionResponse : ITransactionResponse
    {
        public String Version { get; set; }
        public String LockTime { get; set; }
        public List<Vin> Vin { get; set; }
        public List<Vout> Vout { get; set; }
        public String TxId { get; set; }
    }

    public class Vin
    {
        public String TxId { get; set; }
        public String Vout { get; set; }
        public ScriptSig ScriptSig { get; set; }
        public String Sequence { get; set; }
    }

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

    public class ScriptSig
    {
        public String Asm { get; set; }
        public String Hex { get; set; }
    }
}