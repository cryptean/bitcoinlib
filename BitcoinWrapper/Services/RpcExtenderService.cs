// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using BitcoinLib.Responses;


namespace BitcoinLib.Services
{
    public sealed partial class BitcoinService : IBitcoinService
    {
        public Dictionary<String, String> GetMyPublicAndPrivateKeyPairs()
        {
            const Int16 secondsToUnlockTheWallet = 30;
            Dictionary<String, String> keyPairs = new Dictionary<String, String>();
            WalletPassphrase(ConfigurationManager.AppSettings.Get("WalletPassword"), secondsToUnlockTheWallet);
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

        //  Note: As RPC's gettransaction works only for in-wallet transaction we had to extend this with the use of raw transactions so it will work for every single transaction.
        public DecodeRawTransactionResponse GetPublicTransaction(String txId)
        {
            String rawTransaction = GetRawTransaction(txId, 0);
            return DecodeRawTransaction(rawTransaction);
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

        public IEnumerable<String> GetTransactionSenderAddresses(String txId)
        {
            List<String> senderAddresses = new List<String>();
            String rawTransaction = GetRawTransaction(txId, 0);
            DecodeRawTransactionResponse decodedRawTransaction = DecodeRawTransaction(rawTransaction);
            List<Vin> transactionInputs = decodedRawTransaction.Vin;
            Console.WriteLine("Found {0} inputs", transactionInputs.Count);

            for (Int32 i = 0; i < transactionInputs.Count; i++)
            {
                Console.WriteLine("\nInput {0}: txId = {1}", i + 1, transactionInputs[i].TxId);
                String rawTransactionHex = GetRawTransaction(transactionInputs[i].TxId, 0);
                DecodeRawTransactionResponse inputDecodedRawTransaction = DecodeRawTransaction(rawTransactionHex);
                List<Vout> vouts = inputDecodedRawTransaction.Vout;

                foreach (String sendersAddress in vouts.Select(vout => vout.ScriptPubKey.Addresses[0]))
                {
                    Console.Write("Sender's address: {0}...", sendersAddress);

                    if (!senderAddresses.Contains(sendersAddress))
                    {
                        senderAddresses.Add(sendersAddress);
                        Console.WriteLine("Added");
                    }
                    else
                    {
                        Console.WriteLine("Already exists");
                    }
                }
            }
            return senderAddresses;
        }

        public Boolean IsInWalletTransaction(String txId)
        {
            //  Note: This might not be efficient if iterated, consider caching ListTransactions' results.
            return ListTransactions(null, Int32.MaxValue, 0).Any(listTransactionsResponse => listTransactionsResponse.TxId == txId);
        }

        public double GetTransactionPriority(CoinParameters.CoinParameters coinParams, BitcoinLib.Requests.CreateRawTransaction.CreateRawTransactionRequest rawTx)
        {
            int numOfRawInputs = rawTx.Inputs.Count;
            int numOfRawOutputs = rawTx.Outputs.Count;

            IRpcService svcInterface = ((IRpcService)this);

            if (svcInterface == null)
                throw new InvalidCastException("Implementations of IRpcExtender Service must also implement IRpcService to get priority");

            var listOfUnspent = svcInterface.ListUnspent();

            decimal coinAgeValue = 0m;

            foreach (var input in rawTx.Inputs)
            {
                foreach (var unspent in listOfUnspent)
                {
                    if (input.TransactionId == unspent.TxId && input.Output == unspent.VOut)
                    {
                        coinAgeValue += unspent.Amount * unspent.Confirmations;
                        //No reason to continue iterating on this unspent, won't find any more of them
                        break;
                    }
                }
            }

            int size = coinParams.TransactionBaseSizeSingleIO + (numOfRawInputs - 1) * coinParams.TransactionIncrementalInputSize + (numOfRawOutputs - 1) * coinParams.TransactionIncrementalOutputSize;

            double priority = (double)(coinAgeValue / size);

            return priority;

        }

        public bool IsTransactionFree(CoinParameters.CoinParameters coinParams, BitcoinLib.Requests.CreateRawTransaction.CreateRawTransactionRequest rawTx)
        {
            int numOfRawInputs = rawTx.Inputs.Count;
            int numOfRawOutputs = rawTx.Outputs.Count;

            int size = coinParams.TransactionBaseSizeSingleIO + (numOfRawInputs - 1) * coinParams.TransactionIncrementalInputSize + (numOfRawOutputs - 1) * coinParams.TransactionIncrementalOutputSize;

            if (size > coinParams.AllowedFreeSize)
                return false;

            if (rawTx.Outputs.Any(t => t.Value < coinParams.DustThreshold))
                return false;

            double freeThreshold = 1 * coinParams.ConfirmationsForMediumAtOneCoin / coinParams.TransactionBaseSizeSingleIO;
            double priority = GetTransactionPriority(coinParams, rawTx);

            if (priority > freeThreshold)
                return true;

            return false;
        }


    }
}