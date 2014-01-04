using System;

namespace BitcoinWrapper.Responses
{
    public class ListUnspentResponse
    {
        public String TxId { get; set; }
        public Int32 VOut { get; set; }
        public String Address { get; set; }
        public String ScriptPubKey { get; set; }
        public Decimal Amount { get; set; }
        public Int32 Confirmations { get; set; }
    }
}
