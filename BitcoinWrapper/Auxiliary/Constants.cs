// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.Auxiliary
{
    public static class Constants
    {
        public const String GenesisBlock = "0x000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f";
        public const Decimal OneSatoshiInBTC = 0.00000001M;
        public const Decimal MinimumNonDustTransactionAmountInBTC = 0.0000543M;
    }
}