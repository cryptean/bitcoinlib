using System;
using System.Collections.Generic;

namespace BitcoinWrapper.Responses
{
    public class ValidateAddressResponse
    {
        public Boolean IsValid { get; set; }
        public String Address { get; set; }
        public Boolean IsMine { get; set; }
        public Boolean IsScript { get; set; }
        public String Script { get; set; }
        public List<String> Addresses { get; set; }
        public Int32 SigsRequired { get; set; }
        public String Account { get; set; }
    }
}
