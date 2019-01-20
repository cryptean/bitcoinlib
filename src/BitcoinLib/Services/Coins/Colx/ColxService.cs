using BitcoinLib.CoinParameters.Colx;
using BitcoinLib.Services.Coins.Bitcoin;
using BitcoinLib.Services.Coins.Dash;

namespace BitcoinLib.Services.Coins.Colx
{
	/// <summary>
	/// Mostly the same functionality as <see cref="BitcoinService"/>.
	/// </summary>
	public class ColxService : DashService, IColxService
	{
		public ColxService(bool useTestnet = false) : base(useTestnet) { }

		public ColxService(string daemonUrl, string rpcUsername, string rpcPassword,
			string walletPassword) : base(daemonUrl, rpcUsername, rpcPassword, walletPassword) { }

		public ColxService(string daemonUrl, string rpcUsername, string rpcPassword,
			string walletPassword, short rpcRequestTimeoutInSeconds) : base(daemonUrl, rpcUsername,
			rpcPassword, walletPassword, rpcRequestTimeoutInSeconds) { }
		
		public new ColxConstants.Constants Constants => ColxConstants.Constants.Instance;
	}
}