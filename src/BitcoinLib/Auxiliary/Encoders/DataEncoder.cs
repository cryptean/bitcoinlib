// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

namespace BitcoinLib.Auxiliary.Encoders
{
    public abstract class DataEncoder
    {
        internal DataEncoder()
        {
        }

        // char.IsWhiteSpace fits well but it match other whitespaces 
        // characters too and also works for unicode characters.
        public static bool IsSpace(char c)
        {
            return c == ' ' || c == '\t' || c == '\n' || c == '\v' || c == '\f' || c == '\r';
        }

        public string EncodeData(byte[] data)
        {
            return EncodeData(data, 0, data.Length);
        }

        public abstract string EncodeData(byte[] data, int offset, int count);

        public abstract byte[] DecodeData(string encoded);
    }

    public static class Encoders
    {
        private static readonly ASCIIEncoder _ASCII = new ASCIIEncoder();

        private static readonly HexEncoder _Hex = new HexEncoder();

        public static DataEncoder ASCII => _ASCII;

        public static DataEncoder Hex => _Hex;
    }
}