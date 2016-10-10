// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using BitcoinLib.CoinParameters.Base;
using BitcoinLib.Services.RpcServices.RpcExtenderService;
using BitcoinLib.Services.RpcServices.RpcService;

namespace BitcoinLib.Services.Coins.Base
{
    public interface ICoinService : IRpcService, IRpcExtenderService, ICoinParameters
    {
    }
}