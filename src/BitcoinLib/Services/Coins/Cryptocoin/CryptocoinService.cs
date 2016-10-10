// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

namespace BitcoinLib.Services.Coins.Cryptocoin
{
    public class CryptocoinService : CoinService, ICryptocoinService
    {
        public CryptocoinService(string daemonUrl, string rpcUsername, string rpcPassword, string walletPassword)
            : base(daemonUrl, rpcUsername, rpcPassword, walletPassword)
        {
        }

        public CryptocoinService(string daemonUrl, string rpcUsername, string rpcPassword, string walletPassword, short rpcRequestTimeoutInSeconds)
            : base(daemonUrl, rpcUsername, rpcPassword, walletPassword, rpcRequestTimeoutInSeconds)
        {
        }
    }
}