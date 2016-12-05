// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System.Collections.Generic;
using System.Threading.Tasks;
using BitcoinLib.Requests.CreateRawTransaction;
using BitcoinLib.Responses;

namespace BitcoinLib.Services.RpcServices.RpcExtenderService
{
    public interface IRpcExtenderService
    {
        Task<decimal> GetAddressBalance(string inWalletAddress, int minConf = 0, bool validateAddressBeforeProcessing = true);
        Task<decimal> GetMinimumNonZeroTransactionFeeEstimate(short numberOfInputs = 1, short numberOfOutputs = 1);
        Task<Dictionary<string, string>> GetMyPublicAndPrivateKeyPairs();
        Task<DecodeRawTransactionResponse> GetPublicTransaction(string txId);
        Task<decimal> GetTransactionFee(CreateRawTransactionRequest createRawTransactionRequest, bool checkIfTransactionQualifiesForFreeRelay = true, bool enforceMinimumTransactionFeePolicy = true);
        Task<decimal> GetTransactionPriority(CreateRawTransactionRequest createRawTransactionRequest);
        decimal GetTransactionPriority(IList<ListUnspentResponse> transactionInputs, int numberOfOutputs);
        Task<string> GetTransactionSenderAddress(string txId);
        int GetTransactionSizeInBytes(CreateRawTransactionRequest createRawTransactionRequest);
        int GetTransactionSizeInBytes(int numberOfInputs, int numberOfOutputs);
        Task<GetRawTransactionResponse> GetRawTxFromImmutableTxId(string rigidTxId, int listTransactionsCount = int.MaxValue, int listTransactionsFrom = 0, bool getRawTransactionVersbose = true, bool rigidTxIdIsSha256 = false);
        Task<string> GetImmutableTxId(string txId, bool getSha256Hash = false);
        Task<bool> IsInWalletTransaction(string txId);
        Task<bool> IsTransactionFree(CreateRawTransactionRequest createRawTransactionRequest);
        bool IsTransactionFree(IList<ListUnspentResponse> transactionInputs, int numberOfOutputs, decimal minimumAmountAmongOutputs);
        Task<bool> IsWalletEncrypted();
    }
}