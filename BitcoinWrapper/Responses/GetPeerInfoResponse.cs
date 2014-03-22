// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.Responses
{
    public class GetPeerInfoResponse
    {
        public String Addr { get; set; }
        public String Services { get; set; }
        public Int32 LastSend { get; set; }
        public Int32 LastRecv { get; set; }
        public Int32 BytesSent { get; set; }
        public Int32 BytesRecv { get; set; }
        public Int32 ConnTime { get; set; }
        public Double PingTime { get; set; }
        public Double Version { get; set; }
        public String SubVer { get; set; }
        public Boolean Inbound { get; set; }
        public Int32 StartingHeight { get; set; }
        public Int32 BanScore { get; set; }
        public Boolean SyncNode { get; set; }
    }
}