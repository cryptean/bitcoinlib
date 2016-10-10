// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System;
using System.Linq;

namespace BitcoinLib.Auxiliary.Encoders
{
    public class HexEncoder : DataEncoder
    {
        private static readonly string[] HexTbl = Enumerable.Range(0, 256).Select(v => v.ToString("x2")).ToArray();

        private static readonly int[] HexValueArray;

        static HexEncoder()
        {
            var hexDigits = new[]
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
                'A', 'B', 'C', 'D', 'E', 'F'
            };
            var hexValues = new byte[]
            {
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15,
                10, 11, 12, 13, 14, 15
            };

            var max = hexDigits.Max();
            HexValueArray = new int[max + 1];
            for (var i = 0; i < HexValueArray.Length; i++)
            {
                var idx = Array.IndexOf(hexDigits, (char) i);
                var value = -1;
                if (idx != -1)
                    value = hexValues[idx];
                HexValueArray[i] = value;
            }
        }

        public bool Space { get; set; }

        public override string EncodeData(byte[] data, int offset, int count)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var pos = 0;
            var spaces = (Space ? Math.Max((count - 1), 0) : 0);
            var s = new char[2 * count + spaces];
            for (var i = offset; i < offset + count; i++)
            {
                if (Space && i != 0)
                    s[pos++] = ' ';
                var c = HexTbl[data[i]];
                s[pos++] = c[0];
                s[pos++] = c[1];
            }
            return new string(s);
        }

        public override byte[] DecodeData(string encoded)
        {
            if (encoded == null)
                throw new ArgumentNullException(nameof(encoded));
            if (encoded.Length % 2 == 1)
                throw new FormatException("Invalid Hex String");

            var result = new byte[encoded.Length / 2];
            for (int i = 0, j = 0; i < encoded.Length; i += 2, j++)
            {
                var a = IsDigit(encoded[i]);
                var b = IsDigit(encoded[i + 1]);
                if (a == -1 || b == -1)
                    throw new FormatException("Invalid Hex String");
                result[j] = (byte) (((uint) a << 4) | (uint) b);
            }
            return result;
        }

        public bool IsValid(string str)
        {
            return str.ToCharArray().All(c => IsDigit(c) != -1) && str.Length % 2 == 0;
        }

        public static int IsDigit(char c)
        {
            return c + 1 <= HexValueArray.Length
                ? HexValueArray[c]
                : -1;
        }

        public static bool IsWellFormed(string str)
        {
            try
            {
                Encoders.Hex.DecodeData(str);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}