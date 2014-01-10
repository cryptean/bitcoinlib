using System;
using System.Collections.Generic;

namespace BitcoinLib.Responses
{
    //  Wiki reference: https://en.bitcoin.it/wiki/Transactions

    //  todo: alter the member types to serve most specific purposes that String does
    public class DecodeRawTransactionResponse
    {
        public String TxId { get; set; }
        public String Version { get; set; }
        public String LockTime { get; set; }
        public List<Vin> Vin { get; set; }
        public List<Vout> Vout { get; set; }
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
        public String ReqSigs { get; set; }
        public String Type { get; set; }
        public List<String> Addresses { get; set; }
    }

    public class ScriptSig
    {
        public String Asm { get; set; }
        public String Hex { get; set; }
    }
}