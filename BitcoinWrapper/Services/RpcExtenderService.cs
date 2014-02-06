// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using BitcoinLib.Responses;

namespace BitcoinLib.Services
{
    public sealed partial class BitcoinService
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

                if (validateAddressResponse.IsMine && validateAddressResponse.IsValid)
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
    }
}