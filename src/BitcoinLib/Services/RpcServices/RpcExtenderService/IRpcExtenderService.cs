// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;
using BitcoinLib.Requests.CreateRawTransaction;
using BitcoinLib.Responses;

namespace BitcoinLib.Services.RpcServices.RpcExtenderService
{
    public interface IRpcExtenderService
    {
        Decimal GetAddressBalance(String inWalletAddress, Int32 minConf = 0, Boolean validateAddressBeforeProcessing = true);
        Decimal GetMinimumNonZeroTransactionFeeEstimate(Int16 numberOfInputs = 1, Int16 numberOfOutputs = 1);
        Dictionary<String, String> GetMyPublicAndPrivateKeyPairs();
        DecodeRawTransactionResponse GetPublicTransaction(String txId);
        Decimal GetTransactionFee(CreateRawTransactionRequest createRawTransactionRequest, Boolean checkIfTransactionQualifiesForFreeRelay = true, Boolean enforceMinimumTransactionFeePolicy = true);
        Decimal GetTransactionPriority(CreateRawTransactionRequest createRawTransactionRequest);
        Decimal GetTransactionPriority(IList<ListUnspentResponse> transactionInputs, Int32 numberOfOutputs);
        String GetTransactionSenderAddress(String txId);
        Int32 GetTransactionSizeInBytes(CreateRawTransactionRequest createRawTransactionRequest);
        Int32 GetTransactionSizeInBytes(Int32 numberOfInputs, Int32 numberOfOutputs);
        GetRawTransactionResponse GetRawTxFromImmutableTxId(String rigidTxId, Int32 listTransactionsCount = Int32.MaxValue, Int32 listTransactionsFrom = 0, Boolean getRawTransactionVersbose = true, Boolean rigidTxIdIsSha256 = false);
        String GetImmutableTxId(String txId, Boolean getSha256Hash = false);
        Boolean IsInWalletTransaction(String txId);
        Boolean IsTransactionFree(CreateRawTransactionRequest createRawTransactionRequest);
        Boolean IsTransactionFree(IList<ListUnspentResponse> transactionInputs, Int32 numberOfOutputs, Decimal minimumAmountAmongOutputs);
        Boolean IsWalletEncrypted();
    }
}