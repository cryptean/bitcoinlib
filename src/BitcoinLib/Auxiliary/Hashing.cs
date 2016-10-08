// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BitcoinLib.Auxiliary
{
    public class Hashing
    {
        public static string GetSha256(string text)
        {
            return new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(text)).Aggregate(string.Empty, (current, x) => current + $"{x:x2}");
        }
    }
}