// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System.Collections.Generic;
using BitcoinLib.Requests.CreateRawTransaction;
using BitcoinLib.Responses;

namespace BitcoinLib.Services.RpcServices.RpcExtenderService
{
    public interface IRpcExtenderService
    {
        decimal GetAddressBalance(string inWalletAddress, int minConf = 0, bool validateAddressBeforeProcessing = true);
        decimal GetMinimumNonZeroTransactionFeeEstimate(short numberOfInputs = 1, short numberOfOutputs = 1);
        Dictionary<string, string> GetMyPublicAndPrivateKeyPairs();
        DecodeRawTransactionResponse GetPublicTransaction(string txId);
        decimal GetTransactionFee(CreateRawTransactionRequest createRawTransactionRequest, bool checkIfTransactionQualifiesForFreeRelay = true, bool enforceMinimumTransactionFeePolicy = true);
        decimal GetTransactionPriority(CreateRawTransactionRequest createRawTransactionRequest);
        decimal GetTransactionPriority(IList<ListUnspentResponse> transactionInputs, int numberOfOutputs);
        string GetTransactionSenderAddress(string txId);
        int GetTransactionSizeInBytes(CreateRawTransactionRequest createRawTransactionRequest);
        int GetTransactionSizeInBytes(int numberOfInputs, int numberOfOutputs);
        GetRawTransactionResponse GetRawTxFromImmutableTxId(string rigidTxId, int listTransactionsCount = int.MaxValue, int listTransactionsFrom = 0, bool getRawTransactionVersbose = true, bool rigidTxIdIsSha256 = false);
        string GetImmutableTxId(string txId, bool getSha256Hash = false);
        bool IsInWalletTransaction(string txId);
        bool IsTransactionFree(CreateRawTransactionRequest createRawTransactionRequest);
        bool IsTransactionFree(IList<ListUnspentResponse> transactionInputs, int numberOfOutputs, decimal minimumAmountAmongOutputs);
        bool IsWalletEncrypted();
    }
}