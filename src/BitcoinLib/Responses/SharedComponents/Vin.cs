// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

namespace BitcoinLib.Responses.SharedComponents
{
    public class Vin
    {
        public string TxId { get; set; }
        public string Vout { get; set; }
        public ScriptSig ScriptSig { get; set; }
        public string CoinBase { get; set; }
        public string Sequence { get; set; }
    }

    public class ScriptSig
    {
        public string Asm { get; set; }
        public string Hex { get; set; }
    }
}