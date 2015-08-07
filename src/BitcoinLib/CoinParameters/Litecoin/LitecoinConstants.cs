// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using BitcoinLib.CoinParameters.Base;

namespace BitcoinLib.CoinParameters.Litecoin
{
    public static class LitecoinConstants
    {
        public sealed class Constants : CoinConstants<Constants>
        {
            public readonly UInt16 CoinReleaseHalfsEveryXInYears = 4;
            public readonly UInt16 DifficultyIncreasesEveryXInBlocks = 2016;
            public readonly UInt32 OneLitecoinInLitetoshis = 100000000;
            public readonly Decimal OneLitetoshiInLTC = 0.00000001M;
            public readonly Decimal OneMicrocoinInLTC = 0.000001M;
            public readonly Decimal OneMillicoinInLTC = 0.001M;
            public readonly String Symbol = "Ł";
        }
    }
}