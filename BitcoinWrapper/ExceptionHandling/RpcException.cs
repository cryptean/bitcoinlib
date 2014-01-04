using System;

namespace BitcoinWrapper.ExceptionHandling
{
    [Serializable]
    public class RpcException : Exception
    {
        public RpcException()
        {
        }

        public RpcException(String customMessage) : base(customMessage)
        {
        }

        public RpcException(String customMessage, Exception exception) : base(customMessage, exception)
        {
        }
    }
}
