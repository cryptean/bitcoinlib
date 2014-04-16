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
        Decimal GetTransactionFee(CreateRawTransactionRequest createRawTransactionRequest, Boolean checkIfTransactionQualifiesForFreeRelay = true);
        Decimal GetTransactionPriority(CreateRawTransactionRequest createRawTransactionRequest);
        String GetTransactionSenderAddress(String txId);
        Int32 GetTransactionSizeInBytes(CreateRawTransactionRequest createRawTransactionRequest);
        Boolean IsInWalletTransaction(String txId);
        Boolean IsWalletEncrypted();
        Boolean TransactionQualifiesForFreeRelay(CreateRawTransactionRequest createRawTransactionRequest);
    }
}