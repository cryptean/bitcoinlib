// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

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