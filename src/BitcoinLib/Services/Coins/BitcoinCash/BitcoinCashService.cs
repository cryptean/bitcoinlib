using BitcoinLib.CoinParameters.Bitcoin;
using BitcoinLib.Services.Coins.Bitcoin;

namespace BitcoinLib.Services.Coins.BitcoinCash
{
	public class BitcoinCashService : CoinService, IBitcoinService
	{
		public BitcoinCashService(bool useTestnet = false) : base(useTestnet)
		{
		}

		public BitcoinCashService(string daemonUrl, string rpcUsername, string rpcPassword, string walletPassword, short rpcRequestTimeoutInSeconds) : base(daemonUrl, rpcUsername, rpcPassword, walletPassword, rpcRequestTimeoutInSeconds)
		{
		}

		public BitcoinConstants.Constants Constants => BitcoinConstants.Constants.Instance;
	}
}