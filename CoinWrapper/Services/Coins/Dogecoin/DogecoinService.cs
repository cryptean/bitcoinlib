// Copyright (c) 2015 George Kimionis & Shaun Barratt
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using BitcoinLib.CoinParameters.Dogecoin;

namespace BitcoinLib.Services.Coins.Dogecoin
{
    public class DogecoinService : CoinService, IDogecoinService
    {
        public DogecoinService(Boolean useTestnet = false) : base(useTestnet)
        {
        }

        public DogecoinService(String daemonUrl, String rpcUsername, String rpcPassword, String walletPassword = null) : base(daemonUrl, rpcUsername, rpcPassword, walletPassword)
        {
        }

        public DogecoinConstants.Constants Constants
        {
            get
            {
                return DogecoinConstants.Constants.Instance;
            }
        }
    }
}
