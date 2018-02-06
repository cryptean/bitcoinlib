// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using BitcoinLib.CoinParameters.Smartcash;

namespace BitcoinLib.Services.Coins.Smartcash
{
    public class SmartcashService : CoinService, ISmartcashService
    {
        public SmartcashService(bool useTestnet = false) : base(useTestnet)
        {
        }

        public SmartcashService(string daemonUrl, string rpcUsername, string rpcPassword, string walletPassword = null)
            : base(daemonUrl, rpcUsername, rpcPassword, walletPassword)
        {
        }

        public SmartcashService(string daemonUrl, string rpcUsername, string rpcPassword, string walletPassword, short rpcRequestTimeoutInSeconds)
            : base(daemonUrl, rpcUsername, rpcPassword, walletPassword, rpcRequestTimeoutInSeconds)
        {
        }

        public SmartcashConstants.Constants Constants => SmartcashConstants.Constants.Instance;
    }
}