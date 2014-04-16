// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;
using System.Linq;
using BitcoinLib.RPC;
using BitcoinLib.Requests.CreateRawTransaction;
using BitcoinLib.Responses;
using BitcoinLib.Services.Coins.Base;

namespace BitcoinLib.Services
{
    public partial class CoinService
    {
        //  Note: This will return funky results if the address in question along with its private key have been used to create a multisig address with unspent funds
        public Decimal GetAddressBalance(String inWalletAddress, Int32 minConf, Boolean validateAddressBeforeProcessing)
        {
            if (validateAddressBeforeProcessing)
            {
                ValidateAddressResponse validateAddressResponse = ValidateAddress(inWalletAddress);

                if (!validateAddressResponse.IsValid || !validateAddressResponse.IsMine)
                {
                    //  Implicit exception: Address is invalid or not an in-wallet address
                    return -1;
                }
            }

            List<ListUnspentResponse> listUnspentResponses = ListUnspent(minConf, 9999999, new List<String>
                {
                    inWalletAddress
                });

            return listUnspentResponses.Any() ? listUnspentResponses.Sum(x => x.Amount) : 0;
        }

        public Dictionary<String, String> GetMyPublicAndPrivateKeyPairs()
        {
            const Int16 secondsToUnlockTheWallet = 30;
            Dictionary<String, String> keyPairs = new Dictionary<String, String>();
            WalletPassphrase(Parameters.WalletPassword, secondsToUnlockTheWallet);
            List<ListReceivedByAddressResponse> myAddresses = ListReceivedByAddress(0, true);

            foreach (ListReceivedByAddressResponse listReceivedByAddressResponse in myAddresses)
            {
                ValidateAddressResponse validateAddressResponse = ValidateAddress(listReceivedByAddressResponse.Address);

                if (validateAddressResponse.IsMine && validateAddressResponse.IsValid && !validateAddressResponse.IsScript)
                {
                    String privateKey = DumpPrivKey(listReceivedByAddressResponse.Address);
                    keyPairs.Add(validateAddressResponse.PubKey, privateKey);
                }
            }

            WalletLock();
            return keyPairs;
        }

        //  Note: As RPC's gettransaction works only for in-wallet transactions this had to be extended so it will work for every single transaction.
        public DecodeRawTransactionResponse GetPublicTransaction(String txId)
        {
            String rawTransaction = GetRawTransaction(txId, 0);
            return DecodeRawTransaction(rawTransaction);
        }

        public Decimal GetTransactionFee(CreateRawTransactionRequest transaction, Boolean checkIfTransactionQualifiesForFreeRelay)
        {
            if (checkIfTransactionQualifiesForFreeRelay && TransactionQualifiesForFreeRelay(transaction))
            {
                return 0;
            }

            Decimal transactionSizeInBytes = GetTransactionSizeInBytes(transaction);
            return (transactionSizeInBytes / Parameters.FreeTransactionMaximumSizeInBytes) + (transactionSizeInBytes % Parameters.FreeTransactionMaximumSizeInBytes == 0 ? 0 : 1) * Parameters.FeePerThousandBytesInCoins;
        }

        public Decimal GetTransactionPriority(CreateRawTransactionRequest transaction)
        {
            if (transaction.Inputs.Count == 0)
            {
                return 0;
            }

            List<ListUnspentResponse> unspentInputs = (this as ICoinService).ListUnspent().ToList();
            Decimal sumOfInputsValueInBaseUnitsMultipliedByTheirAge = transaction.Inputs.Select(input => unspentInputs.First(x => x.TxId == input.TxId)).Select(unspentResponse => (unspentResponse.Amount * Parameters.OneCoinInBaseUnits) * unspentResponse.Confirmations).Sum();
            return sumOfInputsValueInBaseUnitsMultipliedByTheirAge / GetTransactionSizeInBytes(transaction);
        }

        //  Note: Be careful when using GetTransactionSenderAddress(es) as it just gives you an address owned by someone who previously controlled the transaction's outputs
        //  which might not actually be the sender (e.g. for e-wallets) and who may not intend to receive anything there in the first place. 
        public String GetTransactionSenderAddress(String txId)
        {
            String rawTransaction = GetRawTransaction(txId, 0);
            DecodeRawTransactionResponse decodedRawTransaction = DecodeRawTransaction(rawTransaction);
            List<Vin> transactionInputs = decodedRawTransaction.Vin;
            String rawTransactionHex = GetRawTransaction(transactionInputs[0].TxId, 0);
            DecodeRawTransactionResponse inputDecodedRawTransaction = DecodeRawTransaction(rawTransactionHex);
            List<Vout> vouts = inputDecodedRawTransaction.Vout;
            return vouts[0].ScriptPubKey.Addresses[0];
        }

        public Int32 GetTransactionSizeInBytes(CreateRawTransactionRequest transaction)
        {
            return (transaction.Inputs.Count * Parameters.TransactionSizeBytesContributedByEachInput)
                   + (transaction.Outputs.Count * Parameters.TransactionSizeBytesContributedByEachOutput)
                   + Parameters.TransactionSizeFixedExtraSizeInBytes
                   + transaction.Inputs.Count;
        }

        public Boolean IsInWalletTransaction(String txId)
        {
            //  Note: This might not be efficient if iterated, consider caching ListTransactions' results.
            return ListTransactions(null, Int32.MaxValue, 0).Any(listTransactionsResponse => listTransactionsResponse.TxId == txId);
        }

        public Boolean IsWalletEncrypted()
        {
            return !Help(RpcMethods.walletlock.ToString()).Contains("unknown command");
        }

        public Boolean TransactionQualifiesForFreeRelay(CreateRawTransactionRequest transaction)
        {
            return transaction.Outputs.Any(x => x.Amount < Parameters.FreeTransactionMinimumOutputAmountInCoins)
                   && GetTransactionSizeInBytes(transaction) < Parameters.FreeTransactionMaximumSizeInBytes
                   && GetTransactionPriority(transaction) > Parameters.FreeTransactionMinimumPriority;
        }
    }
}