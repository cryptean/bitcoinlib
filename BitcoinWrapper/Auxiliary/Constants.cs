// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.Auxiliary
{
    public static class Constants
    {
        public const Int16 MillisecondsInASecond = 1000;
        public const Decimal OneSatoshiInBTC = 0.00000001M;
        public const Decimal MinimumNonDustTransactionAmountInBTC = 0.0000543M;
        public const Decimal FeePerThousandBytesInBTC = 0.0001M;
        public const Int16 FreeTransactionMaximumSizeInBytes = 1000;
        public const Decimal FreeTransactionMinimumOutputAmountInBTC = 0.01M;
    }
}