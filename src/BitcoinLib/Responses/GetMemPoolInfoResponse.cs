// Copyright (c) 2015 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

namespace BitcoinLib.Responses
{
    public class GetMemPoolInfoResponse
    {
        public uint Size { get; set; }
        public ulong Bytes { get; set; }
    }
}