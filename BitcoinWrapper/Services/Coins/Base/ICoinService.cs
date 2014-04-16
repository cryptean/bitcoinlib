// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using BitcoinLib.CoinParameters.Base;
using BitcoinLib.Services.RpcServices.RpcExtenderService;
using BitcoinLib.Services.RpcServices.RpcService;

namespace BitcoinLib.Services.Coins.Base
{
    public interface ICoinService : IRpcService, IRpcExtenderService, ICoinParameters
    {
    }
}