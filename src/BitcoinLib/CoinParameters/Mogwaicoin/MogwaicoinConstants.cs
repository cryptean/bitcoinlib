using BitcoinLib.CoinParameters.Base;

namespace BitcoinLib.CoinParameters.Mogwaicoin
{
    public static class MogwaicoinConstants
    {
        public sealed class Constants : CoinConstants<Constants>
        {
            public readonly ushort CoinReleaseHalfsEveryXInYears = 7;
            public readonly ushort DifficultyIncreasesEveryXInBlocks = 34560;
            public readonly uint OneDashInDuffs = 100000000;
            public readonly decimal OneDuffInDash = 0.00000001M;
            public readonly decimal OneMicrodashInDash = 0.000001M;
            public readonly decimal OneMillidashInDash = 0.001M;
            public readonly string Symbol = "MOG";
        }
    }
}