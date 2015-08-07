// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.ExceptionHandling.RawTransactions
{
    [Serializable]
    public class RawTransactionExcessiveFeeException : Exception
    {
        public RawTransactionExcessiveFeeException() : base("Fee in raw transaction is greater than specified amount.")
        {
        }

        public RawTransactionExcessiveFeeException(Decimal maxSpecifiedFee) : base(String.Format("Fee in raw transaction is greater than specified amount of {0}.", maxSpecifiedFee))
        {
        }

        public RawTransactionExcessiveFeeException(Decimal actualFee, Decimal maxSpecifiedFee) : base(String.Format("Fee of {0} in raw transaction is greater than specified amount of {1}.", actualFee, maxSpecifiedFee))
        {
        }

        public RawTransactionExcessiveFeeException(String message) : base(message)
        {
        }

        public RawTransactionExcessiveFeeException(String message, Exception innerException) : base(message, innerException)
        {
        }
    }
}