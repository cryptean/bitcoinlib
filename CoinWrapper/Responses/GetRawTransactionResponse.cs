// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;
using BitcoinLib.Responses.Bridges;
using BitcoinLib.Responses.SharedComponents;

namespace BitcoinLib.Responses
{
    public class GetRawTransactionResponse : ITransactionResponse
    {
        public String Hex { get; set; }
        public String TxId { get; set; }
        public UInt32 Version { get; set; }
        public UInt32 LockTime { get; set; }
        public List<Vin> Vin { get; set; }
        public List<Vout> Vout { get; set; }
        public String BlockHash { get; set; }
        public Int32 Confirmations { get; set; }
        public UInt32 Time { get; set; }
        public UInt32 BlockTime { get; set; }
    }
}
