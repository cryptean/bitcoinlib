using BitcoinLib.CoinParameters.Base;

namespace BitcoinLib.CoinParameters.Colx
{
    public static class ColxConstants
    {
        public sealed class Constants : CoinConstants<Constants>
        {
            public readonly ushort CoinReleaseHalfsEveryXInYears = 7;
            public readonly ushort DifficultyIncreasesEveryXInBlocks = 34560;
            public readonly string Symbol = "COLX";
        }
    }
}