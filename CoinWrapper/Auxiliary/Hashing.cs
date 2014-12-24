// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BitcoinLib.Auxiliary
{
    public class Hashing
    {
        public static String GetSha256(String text)
        {
            return new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(text)).Aggregate(String.Empty, (current, x) => current + String.Format("{0:x2}", x));
        }
    }
}
