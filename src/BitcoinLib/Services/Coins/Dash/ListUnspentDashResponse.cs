using BitcoinLib.Responses;

namespace BitcoinLib.Services.Coins.Dash
{
	public class ListUnspentDashResponse : ListUnspentResponse
	{
		public decimal Ps_Rounds { get; set; }
	}
}