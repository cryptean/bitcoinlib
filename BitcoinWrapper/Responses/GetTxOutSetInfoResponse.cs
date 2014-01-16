// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.Responses
{
    public class GetTxOutSetInfoResponse
    {
        public Int32 Height { get; set; }
        public String BestBlock { get; set; }
        public Int32 Transactions { get; set; }
        public Int32 TxOuts { get; set; }
        public Int32 BytesSerialized { get; set; }
        public String HashSerialized { get; set; }
        public Double TotalAmount { get; set; }
    }
}