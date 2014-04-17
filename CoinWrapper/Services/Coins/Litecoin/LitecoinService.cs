// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using BitcoinLib.CoinParameters.Litecoin;

namespace BitcoinLib.Services.Coins.Litecoin
{
    public class LitecoinService : CoinService, ILitecoinService
    {
        public LitecoinService(Boolean useTestnet = false) : base(useTestnet)
        {
        }

        public LitecoinConstants.Constants Constants
        {
            get
            {
                return LitecoinConstants.Constants.Instance;
            }
        }
    }
}