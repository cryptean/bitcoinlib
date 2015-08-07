// Copyright (c) 2015 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.Responses
{
    public class GetMemPoolInfoResponse
    {
        public UInt32 Size { get; set; }
        public UInt64 Bytes { get; set; }
    }
}
