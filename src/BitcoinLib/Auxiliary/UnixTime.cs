// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System;

namespace BitcoinLib.Auxiliary
{
    public static class UnixTime
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        public static DateTime UnixTimeToDateTime(double unixTimeStamp)
        {
            return Epoch.AddSeconds(unixTimeStamp).ToUniversalTime();
        }
    }
}