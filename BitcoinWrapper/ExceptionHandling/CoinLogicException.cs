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

}
