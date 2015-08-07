// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.ExtensionMethods
{
    public static class DecimalExtensionMethods
    {
        public static UInt16 GetNumberOfDecimalPlaces(this Decimal number)
        {
            return BitConverter.GetBytes(Decimal.GetBits(number)[3])[2];
        }
    }
}