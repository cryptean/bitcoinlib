// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.Requests.SignRawTransaction
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