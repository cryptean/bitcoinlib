using System;

namespace BitcoinWrapper.Requests.SignRawTransaction
{
    public static class SigHashType
    {
        public const String All = "ALL";
        public const String None = "NONE";
        public const String Single = "SINGLE";
        public const String AllAnyoneCanPay = "ALL|ANYONECANPAY";
        public const String NoneAnyoneCanPay = "NONE|ANYONECANPAY";
        public const String SingleAnyoneCanPay = "SINGLE|ANYONECANPAY";
    }
}
