using System.Collections.Generic;
using Newtonsoft.Json;

namespace BitcoinLib.Services.Coins.Dash
{
	public class AddressBalanceRequest
	{
		[JsonProperty("addresses")]
		public List<string> Addresses { get; set; }
	}
}