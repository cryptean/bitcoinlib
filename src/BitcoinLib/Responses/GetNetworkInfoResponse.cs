// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System.Collections.Generic;

namespace BitcoinLib.Responses
{
    public class GetNetworkInfoResponse
    {
        public uint Version { get; set; }
        public string Subversion { get; set; }
        public uint ProtocolVersion { get; set; }
        public string LocalServices { get; set; }
        public int TimeOffset { get; set; }
        public uint Connections { get; set; }
        public IList<Network> Networks { get; set; }
        public decimal RelayFee { get; set; }
        public IList<LocalAddress> LocalAddresses { get; set; }
    }

    public class LocalAddress
    {
        public string Address { get; set; }
        public ushort Port { get; set; }
        public int Score { get; set; }
    }

    public class Network
    {
        public string Name { get; set; }
        public bool Limited { get; set; }
        public bool Reachable { get; set; }
        public string Proxy { get; set; }
    }
}