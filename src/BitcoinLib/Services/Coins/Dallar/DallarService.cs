// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using BitcoinLib.CoinParameters.Dallar;
using BitcoinLib.Requests.CreateRawTransaction;
using BitcoinLib.Responses;
using BitcoinLib.RPC.Specifications;

namespace BitcoinLib.Services.Coins.Dallar
{
    public class DallarService : CoinService, IDallarService
    {
        public DallarService(bool useTestnet = false) : base(useTestnet)
        {
        }

        public DallarService(string daemonUrl, string rpcUsername, string rpcPassword, string walletPassword = null)
            : base(daemonUrl, rpcUsername, rpcPassword, walletPassword)
        {
        }

        public DallarService(string daemonUrl, string rpcUsername, string rpcPassword, string walletPassword, short rpcRequestTimeoutInSeconds)
            : base(daemonUrl, rpcUsername, rpcPassword, walletPassword, rpcRequestTimeoutInSeconds)
        {
        }

        public DallarConstants.Constants Constants => DallarConstants.Constants.Instance;

        public GetFundRawTransactionResponse GetFundRawTransaction(string rawTransactionHex)
        {
            return _rpcConnector.MakeRequest<GetFundRawTransactionResponse>(RpcMethods.fundrawtransaction, rawTransactionHex);
        }

        public decimal GetEstimateFeeForSendToAddress(string Address, decimal Amount)
        {
            var txRequest = new CreateRawTransactionRequest();
            txRequest.AddOutput(Address, Amount);
            return GetFundRawTransaction(CreateRawTransaction(txRequest)).Fee;
        }
    }
}