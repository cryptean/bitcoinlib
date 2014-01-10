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