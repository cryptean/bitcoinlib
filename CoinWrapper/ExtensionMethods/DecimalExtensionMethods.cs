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
