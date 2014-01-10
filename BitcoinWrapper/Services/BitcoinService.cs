using System;
using BitcoinLib.Responses;

namespace BitcoinLib.Services
{
    public sealed class BitcoinService : IBitcoinService
    {
        private readonly IRpcService _rpcService;

        public BitcoinService()
        {
            _rpcService = new RpcService();
        }

        public DecodeRawTransactionResponse GetTransaction(String txId)
        {
            String rawTransaction = _rpcService.GetRawTransaction(txId);
            return _rpcService.DecodeRawTransaction(rawTransaction);
        }
    }
}