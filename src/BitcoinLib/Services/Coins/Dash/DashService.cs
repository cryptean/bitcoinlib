using System.Collections.Generic;
using System.Linq;
using BitcoinLib.CoinParameters.Dash;
using BitcoinLib.Requests.SignRawTransaction;
using BitcoinLib.RPC.Specifications;
using BitcoinLib.Services.Coins.Bitcoin;
using Newtonsoft.Json.Linq;

namespace BitcoinLib.Services.Coins.Dash
{
	/// <summary>
	/// Mostly the same functionality as <see cref="BitcoinService"/>, just adds a bunch more features
	/// for handling InstantSend and PrivateSend, plus better raw tx generation support.
	/// </summary>
	public class DashService : CoinService, IDashService
	{
		public DashService(bool useTestnet = false) : base(useTestnet) { }

		public DashService(string daemonUrl, string rpcUsername, string rpcPassword,
			string walletPassword) : base(daemonUrl, rpcUsername, rpcPassword, walletPassword) { }

		public DashService(string daemonUrl, string rpcUsername, string rpcPassword,
			string walletPassword, short rpcRequestTimeoutInSeconds) : base(daemonUrl, rpcUsername,
			rpcPassword, walletPassword, rpcRequestTimeoutInSeconds) { }
		
		/// <summary>
		/// Adds InstantSend and PrivateSend to SendToAddress from our wallet.
		/// </summary>
		/// <inheritdoc />
		public string SendToAddress(string dashAddress, decimal amount, string comment = null,
			string commentTo = null, bool subtractFeeFromAmount = false, bool useInstantSend = false,
			bool usePrivateSend = false)
			=> _rpcConnector.MakeRequest<string>(RpcMethods.sendtoaddress, dashAddress, amount,
				comment, commentTo, subtractFeeFromAmount, useInstantSend, usePrivateSend);

		/// <summary>
		/// Adds InstantSend support to SendRawTransaction
		/// </summary>
		public string SendRawTransaction(string rawTransactionHexString, bool allowHighFees,
			bool useInstantSend)
			=> _rpcConnector.MakeRequest<string>(RpcMethods.sendrawtransaction, rawTransactionHexString,
				allowHighFees, useInstantSend);
		
		public SignRawTransactionWithErrorResponse SignRawTransactionWithErrorSupport(
			SignRawTransactionRequest request)
		{
			if (request.Inputs.Count == 0)
				request.Inputs = null;
			if (string.IsNullOrWhiteSpace(request.SigHashType))
				request.SigHashType = "ALL";
			if (request.PrivateKeys.Count == 0)
				request.PrivateKeys = null;
			return _rpcConnector.MakeRequest<SignRawTransactionWithErrorResponse>(
				RpcMethods.signrawtransaction, request.RawTransactionHex, request.Inputs,
				request.PrivateKeys, request.SigHashType);
		}

		/// <summary>
		/// privatesend "command"
		/// Arguments:
		/// 1. "command"        (string or set of strings, required) The command to execute
		/// Available commands:
		/// start       - Start mixing
		/// stop        - Stop mixing
		/// reset       - Reset mixing
		/// </summary>
		public string SendPrivateSendCommand(string command)
			=> _rpcConnector.MakeRequest<string>(RpcMethods.privatesend, command);

		public AddressBalanceResponse GetAddressBalance(AddressBalanceRequest addresses)
			=> _rpcConnector.MakeRequest<AddressBalanceResponse>(RpcMethods.getaddressbalance, addresses);

		/// <summary>
		/// Extends unspend result to show ps_rounds to check for available mixed PrivateSend amount.
		/// </summary>
		public List<ListUnspentDashResponse> ListUnspentPrivateSend()
			=> _rpcConnector.MakeRequest<List<ListUnspentDashResponse>>(RpcMethods.listunspent,
				1, 9999999, new List<string>());

		public DashConstants.Constants Constants => DashConstants.Constants.Instance;

		public List<MasternodeResponse> MasternodeList()
		{
			var response = _rpcConnector.MakeRequest<JObject>(RpcMethods.masternode, "list");
			var result = new List<MasternodeResponse>();
			foreach (var data in response)
				result.Add(data.Value.ToObject<MasternodeResponse>());
			return result;
		}
	}
}