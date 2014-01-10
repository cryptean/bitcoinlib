using System;

namespace BitcoinLib.Responses
{
    public class CreateMultiSigResponse
    {
        public String Address { get; set; }
        public String RedeemScript { get; set; }
    }
}