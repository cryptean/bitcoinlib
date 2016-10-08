// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

namespace BitcoinLib.Responses
{
    public class GetNetTotalsResponse
    {
        public ulong TotalBytesRecv { get; set; }
        public ulong TotalBytesSent { get; set; }
        public ulong TimeMillis { get; set; }
    }
}