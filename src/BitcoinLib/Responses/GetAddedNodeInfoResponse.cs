// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System.Collections.Generic;

namespace BitcoinLib.Responses
{
    public class GetAddedNodeInfoResponse
    {
        public string AddedNode { get; set; }
        public bool Connected { get; set; }
        public List<NodeAddress> Addresses { get; set; }
    }

    public class NodeAddress
    {
        public string Address { get; set; }
        public bool Connected { get; set; }
    }
}