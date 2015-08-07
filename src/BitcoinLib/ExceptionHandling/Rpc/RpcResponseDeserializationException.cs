// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.ExceptionHandling.Rpc
{
    [Serializable]
    public class RpcResponseDeserializationException : Exception
    {
        public RpcResponseDeserializationException()
        {
        }

        public RpcResponseDeserializationException(String customMessage) : base(customMessage)
        {
        }

        public RpcResponseDeserializationException(String customMessage, Exception exception) : base(customMessage, exception)
        {
        }
    }
}