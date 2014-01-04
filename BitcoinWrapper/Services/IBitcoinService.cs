using System;
using BitcoinWrapper.Responses;

namespace BitcoinWrapper.Services
{
    public interface IBitcoinService
    {
        DecodeRawTransactionResponse GetTransaction(String txId);
    }
}