// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System.Collections.Generic;
using Newtonsoft.Json;

namespace BitcoinLib.Responses
{
    public class GetPeerInfoResponse
    {
        public uint Id { get; set; }
        public string Addr { get; set; }
        public string AddrLocal { get; set; }
        public string Services { get; set; }
        public int LastSend { get; set; }
        public int LastRecv { get; set; }
        public int BytesSent { get; set; }
        public int BytesRecv { get; set; }
        public int ConnTime { get; set; }
        public double PingTime { get; set; }
        public double Version { get; set; }
        public string SubVer { get; set; }
        public bool Inbound { get; set; }
        public int StartingHeight { get; set; }
        public int BanScore { get; set; }

        [JsonProperty("synced_headers")]
        public int SyncedHeaders { get; set; }

        [JsonProperty("synced_blocks")]
        public int SyncedBlocks { get; set; }

        public IList<int> InFlight { get; set; }
        public bool WhiteListed { get; set; }
    }
}