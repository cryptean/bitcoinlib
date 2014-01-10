using System;

namespace BitcoinLib.Responses
{
    public class SignRawTransactionResponse
    {
        public String Hex { get; set; }
        public Boolean Complete { get; set; }
    }
}