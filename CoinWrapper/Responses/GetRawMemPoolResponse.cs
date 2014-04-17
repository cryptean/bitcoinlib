// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;

namespace BitcoinLib.Responses
{
    public class GetRawMemPoolResponse
    {
        public GetRawMemPoolResponse()
        {
            TxIds = new List<String>();
            VerboseResponses = new List<GetRawMemPoolVerboseResponse>();
        }

        public IList<String> TxIds { get; set; }
        public Boolean IsVerbose { get; set; }
        public IList<GetRawMemPoolVerboseResponse> VerboseResponses { get; set; }
    }

    public class GetRawMemPoolVerboseResponse
    {
        public GetRawMemPoolVerboseResponse()
        {
            Depends = new List<String>();
        }

        public String TxId { get; set; }
        public Int32? Size { get; set; }
        public Decimal? Fee { get; set; }
        public Int32? Time { get; set; }
        public Int32? Height { get; set; }
        public Double? StartingPriority { get; set; }
        public Double? CurrentPriority { get; set; }
        public IList<String> Depends { get; set; }
    }
}