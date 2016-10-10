// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System;

namespace BitcoinLib.ExceptionHandling.RawTransactions
{
    [Serializable]
    public class RawTransactionInvalidAmountException : Exception
    {
        public RawTransactionInvalidAmountException() : base("Raw Transaction amount is invalid.")
        {
        }

        public RawTransactionInvalidAmountException(string message) : base(message)
        {
        }

        public RawTransactionInvalidAmountException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}