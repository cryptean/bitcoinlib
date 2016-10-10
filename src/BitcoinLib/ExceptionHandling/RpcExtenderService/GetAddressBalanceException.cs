// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System;

namespace BitcoinLib.ExceptionHandling.RpcExtenderService
{
    [Serializable]
    public class GetAddressBalanceException : Exception
    {
        public GetAddressBalanceException()
        {
        }

        public GetAddressBalanceException(string customMessage) : base(customMessage)
        {
        }

        public GetAddressBalanceException(string customMessage, Exception exception) : base(customMessage, exception)
        {
        }
    }
}