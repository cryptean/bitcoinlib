// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.Responses.Bridges
{
    //  Note: This serves as a common interface for the cases that a strongly-typed response is required while it is not yet clear whether the transaction in question is in-wallet or not 
    //  A practical example is the bridging of GetTransaction() and DecodeRawTransaction()
    public interface ITransactionResponse
    {
        String TxId { get; set; }
    }
}