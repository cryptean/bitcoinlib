// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using BitcoinLib.CoinParameters.Litecoin;

namespace BitcoinLib.Services.Coins.Litecoin
{
    public class LitecoinService : CoinService, ILitecoinService
    {
        public LitecoinService(bool useTestnet = false) : base(useTestnet)
        {
        }

        public LitecoinService(string daemonUrl, string rpcUsername, string rpcPassword, string walletPassword = null)
            : base(daemonUrl, rpcUsername, rpcPassword, walletPassword)
        {
        }

        public LitecoinService(string daemonUrl, string rpcUsername, string rpcPassword, string walletPassword, short rpcRequestTimeoutInSeconds)
            : base(daemonUrl, rpcUsername, rpcPassword, walletPassword, rpcRequestTimeoutInSeconds)
        {
        }

        public LitecoinConstants.Constants Constants => LitecoinConstants.Constants.Instance;
    }
}