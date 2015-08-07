// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.ExceptionHandling.RawTransactions
{
    [Serializable]
    public class RawTransactionInvalidAmountException : Exception
    {
        public RawTransactionInvalidAmountException() : base("Raw Transaction amount is invalid.")
        {
        }

        public RawTransactionInvalidAmountException(String message) : base(message)
        {
        }

        public RawTransactionInvalidAmountException(String message, Exception innerException) : base(message, innerException)
        {
        }
    }
}