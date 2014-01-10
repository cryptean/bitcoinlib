using System;

namespace BitcoinLib.Responses
{
    public class ListReceivedByAddressResponse
    {
        public String Account { get; set; }
        public String Address { get; set; }
        public Decimal Amount { get; set; }
        public Int32 Confirmations { get; set; }
    }
}