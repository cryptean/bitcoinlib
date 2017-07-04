using BitcoinLib.CoinParameters.Dash;
using BitcoinLib.Services.Coins.Base;

namespace BitcoinLib.Services.Coins.Dash
{
    public interface IDashService : ICoinService, IDashConstants
    {
        /// <summary>
        ///     Send an amount to a given address.
        /// </summary>
        /// <param name="dashAddress">The dash address to send to.</param>
        /// <param name="amount">The amount in DASH to send. eg 0.1.</param>
        /// <param name="comment">
        ///     A comment used to store what the transaction is for. This is not part of the transaction,
        ///     just kept in your wallet.
        /// </param>
        /// <param name="commentTo">
        ///     A comment to store the name of the person or organization to which you're sending the
        ///     transaction. This is not part of the transaction, just kept in your wallet.
        /// </param>
        /// <param name="subtractFeeFromAmount">
        ///     The fee will be deducted from the amount being sent. The recipient will receive
        ///     less amount of Dash than you enter in the amount field.
        /// </param>
        /// <param name="useInstantSend">Send this transaction as InstantSend.</param>
        /// <param name="usePrivateSend">Use anonymized funds only.</param>
        /// <returns>The transaction id.</returns>
        string SendToAddress(string dashAddress, decimal amount, string comment = null,
            string commentTo = null, bool subtractFeeFromAmount = false, bool useInstantSend = false,
            bool usePrivateSend = false);
    }
}