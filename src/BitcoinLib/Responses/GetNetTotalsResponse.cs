// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

namespace BitcoinLib.Responses
{
    public class GetNetTotalsResponse
    {
        public ulong TotalBytesRecv { get; set; }
        public ulong TotalBytesSent { get; set; }
        public ulong TimeMillis { get; set; }
    }
}