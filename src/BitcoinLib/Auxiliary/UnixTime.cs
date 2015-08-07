// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.Auxiliary
{
    public static class UnixTime
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        public static DateTime UnixTimeToDateTime(Double unixTimeStamp)
        {
            return Epoch.AddSeconds(unixTimeStamp).ToUniversalTime();
        }
    }
}