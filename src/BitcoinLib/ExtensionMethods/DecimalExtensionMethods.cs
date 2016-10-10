// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System;

namespace BitcoinLib.ExtensionMethods
{
    public static class DecimalExtensionMethods
    {
        public static ushort GetNumberOfDecimalPlaces(this decimal number)
        {
            return BitConverter.GetBytes(decimal.GetBits(number)[3])[2];
        }
    }
}