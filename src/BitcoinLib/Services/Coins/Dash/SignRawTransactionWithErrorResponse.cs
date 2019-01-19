using System.Collections.Generic;
using BitcoinLib.Responses;

namespace BitcoinLib.Services.Coins.Dash
{
	public class SignRawTransactionWithErrorResponse : SignRawTransactionResponse
	{
		public List<SignRawTransactionError> Errors { get; set; }
	}
}