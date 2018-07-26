// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using BitcoinLib.CoinParameters.Base;

namespace BitcoinLib.CoinParameters.Dallar
{
    public static class DallarConstants
    {
        public sealed class Constants : CoinConstants<Constants>
        {
            public readonly ushort CoinReleaseReduceEveryXInBlocks = 10080;
            public readonly ushort DifficultyIncreasesEveryXInBlocks = 6;
            public readonly uint OneDallarInAllars = 100000000;
            public readonly decimal OneAllarInDAL = 0.00000001M;
            public readonly decimal OneMicroDallarInDAL = 0.000001M;
            public readonly decimal OneMilliDallarInDAL = 0.001M;
            public readonly string Symbol = "D";
        }
    }
}