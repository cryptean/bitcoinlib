using BitcoinLib.CoinParameters.Dash;
using BitcoinLib.RPC.Specifications;

namespace BitcoinLib.Services.Coins.Dash
{
    public class DashService : CoinService, IDashService
    {
        public DashService(bool useTestnet = false) : base(useTestnet)
        {
        }

        public DashService(string daemonUrl, string rpcUsername, string rpcPassword, string walletPassword)
            : base(daemonUrl, rpcUsername, rpcPassword, walletPassword)
        {
        }

        public DashService(string daemonUrl, string rpcUsername, string rpcPassword, string walletPassword,
            short rpcRequestTimeoutInSeconds)
            : base(daemonUrl, rpcUsername, rpcPassword, walletPassword, rpcRequestTimeoutInSeconds)
        {
        }

        /// <inheritdoc />
        public string SendToAddress(string dashAddress, decimal amount, string comment = null, string commentTo = null,
            bool subtractFeeFromAmount = false, bool useInstantSend = false, bool usePrivateSend = false)
        {
            return _rpcConnector.MakeRequest<string>(RpcMethods.sendtoaddress, dashAddress, amount, comment, commentTo,
                subtractFeeFromAmount, useInstantSend, usePrivateSend);
        }

        public DashConstants.Constants Constants => DashConstants.Constants.Instance;
    }
}