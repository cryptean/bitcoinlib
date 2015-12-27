// Copyright (c) 2015 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using BitcoinLib.CoinParameters.Bitcoin;

namespace BitcoinLib.Services.Coins.Sarcoin
{
    public class SarcoinService : CoinService, ISarcoinService
    {
        public SarcoinService(Boolean useTestnet = false) : base(useTestnet)
        {
        }

        public SarcoinService(String daemonUrl, String rpcUsername, String rpcPassword, String walletPassword) 
            : base(daemonUrl, rpcUsername, rpcPassword, walletPassword)
        {
        }

        public SarcoinService(String daemonUrl, String rpcUsername, String rpcPassword, String walletPassword, Int16 rpcRequestTimeoutInSeconds)
            : base(daemonUrl, rpcUsername, rpcPassword, walletPassword, rpcRequestTimeoutInSeconds)
        {
        }
    }
}