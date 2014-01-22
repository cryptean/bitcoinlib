// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;
using BitcoinLib.Responses;

namespace BitcoinLib.Services
{
    public interface IRpcExtenderService
    {
        Dictionary<String, String> GetMyPublicAndPrivateKeyPairs();
        DecodeRawTransactionResponse GetPublicTransaction(String txId);
        String GetTransactionSenderAddress(String txId);
        IEnumerable<String> GetTransactionSenderAddresses(String txId);
        Boolean IsInWalletTransaction(String txId);
    }
}