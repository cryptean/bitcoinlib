// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using BitcoinLib.Services;

namespace BitcoinLib.CoinParameters.Base
{
    public interface ICoinParameters
    {
        CoinService.CoinParameters Parameters { get; }
    }
}