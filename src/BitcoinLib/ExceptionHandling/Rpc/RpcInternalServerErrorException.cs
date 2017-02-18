// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System;
using BitcoinLib.RPC.Specifications;

namespace BitcoinLib.ExceptionHandling.Rpc
{
    [Serializable]
    public class RpcInternalServerErrorException : Exception
    {
        public RpcInternalServerErrorException()
        {
        }

        public RpcInternalServerErrorException(string customMessage) : base(customMessage)
        {
        }

        public RpcInternalServerErrorException(string customMessage, Exception exception) : base(customMessage, exception)
        {
        }

        public RpcErrorCode? RpcErrorCode { get; set; } 
    }
}