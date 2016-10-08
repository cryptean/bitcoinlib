// Copyright (c) 2015 George Kimionis & Shaun Barratt
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using BitcoinLib.CoinParameters.Dogecoin;

namespace BitcoinLib.Services.Coins.Dogecoin
{
    public class DogecoinService : CoinService, IDogecoinService
    {
        public DogecoinService(bool useTestnet = false) : base(useTestnet)
        {
        }

        public DogecoinService(string daemonUrl, string rpcUsername, string rpcPassword, string walletPassword)
            : base(daemonUrl, rpcUsername, rpcPassword, walletPassword)
        {
        }

        public DogecoinService(string daemonUrl, string rpcUsername, string rpcPassword, string walletPassword, short rpcRequestTimeoutInSeconds)
            : base(daemonUrl, rpcUsername, rpcPassword, walletPassword, rpcRequestTimeoutInSeconds)
        {
        }

        public DogecoinConstants.Constants Constants => DogecoinConstants.Constants.Instance;
    }
}