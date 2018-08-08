using BitcoinLib.CoinParameters.Mogwaicoin;
using BitcoinLib.Services.Coins.Base;
using System.Collections.Generic;

namespace BitcoinLib.Services.Coins.Mogwaicoin
{
    public interface IMogwaicoinService : ICoinService, IMogwaicoinConstants
    {
        /// <summary>
        ///     Send an amount to a given address.
        /// </summary>
        /// <param name="mogwaiAddress">The mogwai address to send to.</param>
        /// <param name="amount">The amount in MOG to send. eg 0.1.</param>
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

        /// <summary>
        ///     Get MirrorAddress
        /// </summary>
        /// <param name="mogwaiAddress">The mogwai address to send to.</param>
        /// <returns></returns>
        MirrorAddressResponse MirrorAddress(string mogwaiAddress);

        /// <summary>
        ///     Get transactions sent to MirrorAddress 
        /// </summary>
        /// <param name="mogwaiAddress">The mogwai address to send to.</param>
        /// <returns></returns>
        List<ListMirrorTransactionsResponse> ListMirrorTransactions(string mogwaiAddress);
    }
}