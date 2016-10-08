// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using BitcoinLib.CoinParameters.Base;

namespace BitcoinLib.CoinParameters.Bitcoin
{
    public static class BitcoinConstants
    {
        public sealed class Constants : CoinConstants<Constants>
        {
            public readonly int OneBitcoinInSatoshis = 100000000;
            public readonly decimal OneSatoshiInBTC = 0.00000001M;
            public readonly int SatoshisPerBitcoin = 100000000;
            public readonly string Symbol = "฿";

            #region Custom constructor example - commented out on purpose

            //private static readonly Lazy<Constants> Lazy = new Lazy<Constants>(() => new Constants());

            //public static Constants Instance
            //{
            //    get
            //    {
            //        return Lazy.Value;
            //    }
            //}

            //private Constants()
            //{
            //  //  custom logic here
            //}

            #endregion
        }
    }
}