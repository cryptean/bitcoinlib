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
        Dictionary<String, String> GetMyPublicAndPrivateKeyPairs();
        DecodeRawTransactionResponse GetPublicTransaction(String txId);
        Decimal GetTransactionFee(CreateRawTransactionRequest createRawTransactionRequest, Boolean checkIfTransactionQualifiesForFreeRelay = true, Boolean enforceMinimumTransactionFeePolicy = true);
        Decimal GetTransactionPriority(CreateRawTransactionRequest createRawTransactionRequest);
        Decimal GetTransactionPriority(IList<ListUnspentResponse> transactionInputs, Int32 numberOfOutputs);
        String GetTransactionSenderAddress(String txId);
        Int32 GetTransactionSizeInBytes(CreateRawTransactionRequest createRawTransactionRequest);
        Int32 GetTransactionSizeInBytes(Int32 numberOfInputs, Int32 numberOfOutputs);
        Boolean IsInWalletTransaction(String txId);
        Boolean IsTransactionFree(CreateRawTransactionRequest createRawTransactionRequest);
        Boolean IsTransactionFree(IList<ListUnspentResponse> transactionInputs, Int32 numberOfOutputs, Decimal minimumAmountAmongOutputs);
        Boolean IsWalletEncrypted();
    }
}