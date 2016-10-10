// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System.Linq;

namespace BitcoinLib.Auxiliary.Encoders
{
    public class ASCIIEncoder : DataEncoder
    {
        public override byte[] DecodeData(string encoded)
        {
            return string.IsNullOrEmpty(encoded) ? new byte[0] : encoded.ToCharArray().Select(o => (byte) o).ToArray();
        }

        public override string EncodeData(byte[] data, int offset, int count)
        {
            return new string(data.Skip(offset).Take(count).Select(o => (char) o).ToArray()).Replace("\0", "");
        }
    }
}