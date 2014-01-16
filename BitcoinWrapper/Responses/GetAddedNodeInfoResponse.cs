// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;

namespace BitcoinLib.Responses
{
    public class GetAddedNodeInfoResponse
    {
        public String AddedNode { get; set; }
        public Boolean Connected { get; set; }
        public List<NodeAddress> Addresses { get; set; }
    }

    public class NodeAddress
    {
        public String Address { get; set; }
        public Boolean Connected { get; set; }
    }
}