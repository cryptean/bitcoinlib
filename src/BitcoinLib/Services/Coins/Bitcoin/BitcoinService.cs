// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using BitcoinLib.CoinParameters.Bitcoin;
using BitcoinLib.RPC.Specifications;

namespace BitcoinLib.Services.Coins.Bitcoin
{
    public class BitcoinService : CoinService, IBitcoinService
    {
        public BitcoinService(bool useTestnet = false) : base(useTestnet)
        {
        }

        public BitcoinService(string daemonUrl, string rpcUsername, string rpcPassword, string walletPassword)
            : base(daemonUrl, rpcUsername, rpcPassword, walletPassword)
        {
        }

        public BitcoinService(string daemonUrl, string rpcUsername, string rpcPassword, string walletPassword, short rpcRequestTimeoutInSeconds)
            : base(daemonUrl, rpcUsername, rpcPassword, walletPassword, rpcRequestTimeoutInSeconds)
        {
        }

        public BitcoinConstants.Constants Constants => BitcoinConstants.Constants.Instance;
				
				public string SendToAddress(string bitcoinAddress, decimal amount, string comment, string commentTo, bool subtractFeeFromAmount, bool allowReplaceByFee)
				{
					return _rpcConnector.MakeRequest<string>(RpcMethods.sendtoaddress, bitcoinAddress, amount, comment, commentTo, subtractFeeFromAmount, allowReplaceByFee);
				}

				public string GetNewAddress(string account = "", string addressType = "")
				{
					return string.IsNullOrWhiteSpace(account)
						? _rpcConnector.MakeRequest<string>(RpcMethods.getnewaddress)
						: _rpcConnector.MakeRequest<string>(RpcMethods.getnewaddress, account, addressType);
				}
    }
}