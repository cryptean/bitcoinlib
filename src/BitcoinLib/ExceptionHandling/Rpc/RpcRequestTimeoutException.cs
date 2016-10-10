// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System;

namespace BitcoinLib.ExceptionHandling.Rpc
{
    [Serializable]
    public class RpcRequestTimeoutException : Exception
    {
        public RpcRequestTimeoutException()
        {
        }

        public RpcRequestTimeoutException(string customMessage) : base(customMessage)
        {
        }

        public RpcRequestTimeoutException(string customMessage, Exception exception) : base(customMessage, exception)
        {
        }
    }
}