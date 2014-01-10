using System;

namespace BitcoinLib.Responses
{
    public class ValidateAddressResponse
    {
        public Boolean IsValid { get; set; }
        public String Address { get; set; }
        public Boolean IsMine { get; set; }
        public Boolean IsScript { get; set; }
        public String PubKey { get; set; }
        public Boolean IsCompressed { get; set; }
        public String Account { get; set; }
    }
}