// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

namespace BitcoinLib.Responses
{
    public class ListUnspentResponse
    {
        public string TxId { get; set; }
        public int Vout { get; set; }
        public string Address { get; set; }
        public string Account { get; set; }
        public string ScriptPubKey { get; set; }
        public decimal Amount { get; set; }
        public int Confirmations { get; set; }
        public bool Spendable { get; set; }

        public override string ToString()
        {
            return $"Account: {Account}, Address: {Address}, Amount: {Amount}, Confirmations: {Confirmations}";
        }
    }
}