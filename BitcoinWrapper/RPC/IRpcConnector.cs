using BitcoinWrapper.Enums;

namespace BitcoinWrapper.RPC
{
    public interface IRpcConnector
    {
        T MakeRequest<T>(RpcMethods method, params object[] parameters);
    }
}