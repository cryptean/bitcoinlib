using System;

namespace BitcoinWrapper.Responses
{
    public class SignRawTransactionResponse
    {
        public String Hex { get; set; }
        public Boolean Complete { get; set; }
    }
}
