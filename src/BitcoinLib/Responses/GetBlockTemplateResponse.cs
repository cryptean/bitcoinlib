// Copyright (c) 2015 Jean-Francois Gagnon
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;

namespace BitcoinLib.Responses
{
    public class GetBlockTemplateResponse
    {
        public Int32 Version { get; set; }
        public String PreviousBlockHash { get; set; }
        public List<GetBlockTemplateTransaction> Transactions { get; set; }
        public GetBlockTemplateCoinbaseAux CoinbaseAux { get; set; }
        public Int64 CoinbaseValue { get; set; }
        public String Target { get; set; }
        public Int32 MinTime { get; set; }
        public List<String> Mutable { get; set; }
        public String NonceRange { get; set; }
        public Int32 SigopLimit { get; set; }
        public Int32 SizeLimit { get; set; }
        public UInt32 CurTime { get; set; }
        public String Bits { get; set; }
        public Int32 Height { get; set; }
    }

    public class GetBlockTemplateCoinbaseAux
    {
        public String Flags { get; set; }
    }

    public class GetBlockTemplateTransaction
    {
        public String Data { get; set; }
        public String Hash { get; set; }
        public List<String> Depends { get; set; }
        public Int32 Fee { get; set; }
        public Int32 Sigops { get; set; }
    }
}
