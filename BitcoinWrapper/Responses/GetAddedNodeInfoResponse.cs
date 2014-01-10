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