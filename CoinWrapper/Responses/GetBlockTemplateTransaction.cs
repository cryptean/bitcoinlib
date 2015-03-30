// Copyright (c) 2015 Jean-Francois Gagnon
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System.Collections.Generic;

namespace BitcoinLib.Responses
{
    public class GetBlockTemplateTransaction
    {
        public string Data { get; set; }
        public string Hash { get; set; }
        public List<string> Depends { get; set; }
        public int Fee { get; set; }
        public int Sigops { get; set; }
    }
}
