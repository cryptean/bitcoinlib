// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System.Threading.Tasks;
using BitcoinLib.RPC.Specifications;

namespace BitcoinLib.RPC.Connector
{
    public interface IRpcConnector
    {
        Task<T> MakeRequestAsync<T>(RpcMethods method, params object[] parameters);
    }
}