// Copyright (c) 2015 Jean-Francois Gagnon
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System.Collections.Generic;

namespace BitcoinLib.Responses
{
    public class GetBlockTemplateTransaction
    {
        string Data { get; set; }
        string Hash{ get; set; }
        List<string> Depends{ get; set; }
        int Fee{ get; set; }
        int Sigops{ get; set; }
    }
}
