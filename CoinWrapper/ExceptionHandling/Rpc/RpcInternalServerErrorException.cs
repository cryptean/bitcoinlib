// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using BitcoinLib.RPC;
using BitcoinLib.RPC.RequestResponse;
using BitcoinLib.RPC.Specifications;

namespace BitcoinLib.ExceptionHandling.Rpc
{
    public class RpcInternalServerErrorException : Exception
    {
        public RpcInternalServerErrorException()
        {
        }

        public RpcInternalServerErrorException(String customMessage) : base(customMessage)
        {
        }

        public RpcInternalServerErrorException(String customMessage, Exception exception) : base(customMessage, exception)
        {
        }

        public RpcErrorCode? RpcErrorCode { get; set; }
    }
}
