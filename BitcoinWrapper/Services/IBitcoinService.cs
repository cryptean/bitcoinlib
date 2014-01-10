using System;
using BitcoinLib.Responses;

namespace BitcoinLib.Services
{
    public interface IBitcoinService
    {
        DecodeRawTransactionResponse GetTransaction(String txId);
    }
}