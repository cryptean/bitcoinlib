using System;

namespace BitcoinWrapper.Responses
{
    public class ListAddressGroupingsResponse
    {
        public String Address { get; set; }
        public Decimal Balance { get; set; }
        public String Account { get; set; }
    }
}
