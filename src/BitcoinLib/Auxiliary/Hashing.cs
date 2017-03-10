// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace BitcoinLib.Auxiliary
{
    public class Hashing
    {
        public static string GetSha256(string text)
        { 
            return SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(text)).Aggregate(string.Empty, (current, x) => current + $"{x:x2}");
        }
    }
}