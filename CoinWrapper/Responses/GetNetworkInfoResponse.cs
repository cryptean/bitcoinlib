// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;

namespace BitcoinLib.Responses
{
    public class GetNetworkInfoResponse
    {
        public UInt32 Version { get; set; }
        public UInt32 ProtocolVersion { get; set; }
        public Int32 TimeOffset { get; set; }
        public UInt32 Connections { get; set; }
        public String Proxy { get; set; }
        public IList<LocalAddress> LocalAddresses { get; set; }
    }

    public class LocalAddress
    {
        public String Address { get; set; }
        public UInt16 Port { get; set; }
        public Int32 Score { get; set; }
    }
}