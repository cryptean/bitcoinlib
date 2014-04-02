using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinLib.ExceptionHandling
{
    //This file contains various specific exceptions to give specific meaningful exception capability
    [Serializable]
    public class RawTransactionAmountInvalidException : Exception
    {
        public RawTransactionAmountInvalidException()
            : base("Raw Transaction amount is invalid.")
        {

        }

        public RawTransactionAmountInvalidException(string message)
            : base(message)
        {

        }

        public RawTransactionAmountInvalidException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }

    [Serializable]
    public class RawTransactionExcessiveFeeException : Exception
    {
        public RawTransactionExcessiveFeeException()
            : base("Fee in raw transaction is greater than specified amount.")
        {

        }

        public RawTransactionExcessiveFeeException(decimal maxSpecifiedFee)
            : base(String.Format("Fee in raw transaction is greater than specified amount of {0}.",maxSpecifiedFee))
        {

        }

        public RawTransactionExcessiveFeeException(decimal actualFee, decimal maxSpecifiedFee)
            : base(String.Format("Fee of {0} in raw transaction is greater than specified amount of {1}.", actualFee, maxSpecifiedFee))
        {

        }

        public RawTransactionExcessiveFeeException(string message)
            : base(message)
        {

        }

        public RawTransactionExcessiveFeeException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }

}
