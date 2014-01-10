using System;

namespace BitcoinLib.Responses
{
    public class ListReceivedByAccountResponse
    {
        public String Account { get; set; }
        public Double Amount { get; set; }
        public Int32 Confirmations { get; set; }
    }
}