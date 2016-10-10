// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System;

namespace BitcoinLib.ExceptionHandling.RawTransactions
{
    [Serializable]
    public class RawTransactionExcessiveFeeException : Exception
    {
        public RawTransactionExcessiveFeeException() : base("Fee in raw transaction is greater than specified amount.")
        {
        }

        public RawTransactionExcessiveFeeException(decimal maxSpecifiedFee) : base($"Fee in raw transaction is greater than specified amount of {maxSpecifiedFee}.")
        {
        }

        public RawTransactionExcessiveFeeException(decimal actualFee, decimal maxSpecifiedFee) : base($"Fee of {actualFee} in raw transaction is greater than specified amount of {maxSpecifiedFee}.")
        {
        }

        public RawTransactionExcessiveFeeException(string message) : base(message)
        {
        }

        public RawTransactionExcessiveFeeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}