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
    }
}