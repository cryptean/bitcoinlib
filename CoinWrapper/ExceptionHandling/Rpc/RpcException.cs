// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.ExceptionHandling.Rpc
{
    [Serializable]
    public class RpcException : Exception
    {
        public RpcException()
        {
        }

        public RpcException(String customMessage) : base(customMessage)
        {
        }

        public RpcException(String customMessage, Exception exception) : base(customMessage, exception)
        {
        }
    }
}