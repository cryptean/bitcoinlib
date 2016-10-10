// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System;

namespace BitcoinLib.ExceptionHandling.Rpc
{
    [Serializable]
    public class RpcResponseDeserializationException : Exception
    {
        public RpcResponseDeserializationException()
        {
        }

        public RpcResponseDeserializationException(string customMessage) : base(customMessage)
        {
        }

        public RpcResponseDeserializationException(string customMessage, Exception exception) : base(customMessage, exception)
        {
        }
    }
}