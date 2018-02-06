using BitcoinLib.CoinParameters.Base;

namespace BitcoinLib.CoinParameters.Smartcash
{
    public static class SmartcashConstants
  {
        public sealed class Constants : CoinConstants<Constants>
        {
            public readonly ushort CoinReleaseHalfsEveryXInYears = 4;
            public readonly ushort DifficultyIncreasesEveryXInBlocks = 2016;
            public readonly uint OneSmartInSmartoshis = 100000000;
            public readonly decimal OneSmartoshisInSmart = 0.00000001M;
            public readonly decimal OneMicrosmartInSmart = 0.000001M;
            public readonly decimal OneMillismartInSmart = 0.001M;
            public readonly string Symbol = "SMART";
        }
    }
}