// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.Services.Coins.Cryptocoin
{
    public class CryptocoinService : CoinService, ICryptocoinService
    {
        public CryptocoinService(String daemonUrl, String rpcUsername, String rpcPassword, String walletPassword = null) : base(daemonUrl, rpcUsername, rpcPassword, walletPassword)
        {
        }
    }
}