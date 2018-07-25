// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using BitcoinLib.CoinParameters.Dallar;
using BitcoinLib.Responses;
using BitcoinLib.Services.Coins.Base;

namespace BitcoinLib.Services.Coins.Dallar
{
    public interface IDallarService : ICoinService, IDallarConstants
    {
        GetFundRawTransactionResponse GetFundRawTransaction(string rawTransactionHex);
        decimal GetEstimateFeeForSendToAddress(string Address, decimal Amount);
    }
}