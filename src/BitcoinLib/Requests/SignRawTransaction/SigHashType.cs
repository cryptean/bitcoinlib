// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

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