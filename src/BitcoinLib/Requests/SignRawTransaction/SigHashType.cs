// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

namespace BitcoinLib.Requests.SignRawTransaction
{
    public static class SigHashType
    {
        public const string All = "ALL";
        public const string None = "NONE";
        public const string Single = "SINGLE";
        public const string AllAnyoneCanPay = "ALL|ANYONECANPAY";
        public const string NoneAnyoneCanPay = "NONE|ANYONECANPAY";
        public const string SingleAnyoneCanPay = "SINGLE|ANYONECANPAY";
        public const string AllForkId = "ALL|FORKID";
        public const string NoneForkId = "NONE|FORKID";
        public const string SingleForkId = "SINGLE|FORKID";
        public const string ALlForkIdAnyoneCanPay = "ALL|FORKID|ANYONECANPAY";
        public const string NoneForkIdAnyoneCanPay = "NONE|FORKID|ANYONECANPAY";
        public const string SingleForkIdAnyoneCanPay = "SINGLE|FORKID|ANYONECANPAY";
    }
}