// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BitcoinLib.Auxiliary;
using BitcoinLib.ExceptionHandling.RpcExtenderService;
using BitcoinLib.ExtensionMethods;
using BitcoinLib.Requests.CreateRawTransaction;
using BitcoinLib.Responses;
using BitcoinLib.RPC.Specifications;
using BitcoinLib.Services.Coins.Base;

namespace BitcoinLib.Services
{
    public partial class CoinService
    {
        //  Note: This will return funky results if the address in question along with its private key have been used to create a multisig address with unspent funds
        public async Task<decimal> GetAddressBalance(string inWalletAddress, int minConf, bool validateAddressBeforeProcessing)
        {
            if (validateAddressBeforeProcessing)
            {
                var validateAddressResponse = await ValidateAddress(inWalletAddress);

                if (!validateAddressResponse.IsValid)
                {
                    throw new GetAddressBalanceException($"Address {inWalletAddress} is invalid!");
                }

                if (!validateAddressResponse.IsMine)
                {
                    throw new GetAddressBalanceException($"Address {inWalletAddress} is not an in-wallet address!");
                }
            }

            var listUnspentResponses = await ListUnspent(minConf, 9999999, new List<string>
            {
                inWalletAddress
            });

            return listUnspentResponses.Any() ? listUnspentResponses.Sum(x => x.Amount) : 0;
        }

        public async Task<string> GetImmutableTxId(string txId, bool getSha256Hash)
        {
            var response = await GetRawTransaction(txId, 1);
            var text = response.Vin.First().TxId + "|" + response.Vin.First().Vout + "|" + response.Vout.First().Value;
            return getSha256Hash ? Hashing.GetSha256(text) : text;
        }

        //  Get a rough estimate on fees for non-free txs, depending on the total number of tx inputs and outputs
        [Obsolete("Please don't use this method to calculate tx fees, its purpose is to provide a rough estimate only")]
        public async Task<decimal> GetMinimumNonZeroTransactionFeeEstimate(short numberOfInputs = 1, short numberOfOutputs = 1)
        {
            var rawTransactionRequest = new CreateRawTransactionRequest(new List<CreateRawTransactionInput>(numberOfInputs), new Dictionary<string, decimal>(numberOfOutputs));

            for (short i = 0; i < numberOfInputs; i++)
            {
                rawTransactionRequest.AddInput(new CreateRawTransactionInput
                {
                    TxId = "dummyTxId" + i.ToString(CultureInfo.InvariantCulture),
                    Vout = i
                });
            }

            for (short i = 0; i < numberOfOutputs; i++)
            {
                rawTransactionRequest.AddOutput(new CreateRawTransactionOutput
                {
                    Address = "dummyAddress" + i.ToString(CultureInfo.InvariantCulture),
                    Amount = i + 1
                });
            }

            return await GetTransactionFee(rawTransactionRequest, false, true);
        }

        public async Task<Dictionary<string, string>> GetMyPublicAndPrivateKeyPairs()
        {
            const short secondsToUnlockTheWallet = 30;
            var keyPairs = new Dictionary<string, string>();
            await WalletPassphrase(Parameters.WalletPassword, secondsToUnlockTheWallet);
            var myAddresses = await (this as ICoinService).ListReceivedByAddress(0, true);

            foreach (var listReceivedByAddressResponse in myAddresses)
            {
                var validateAddressResponse = await ValidateAddress(listReceivedByAddressResponse.Address);

                if (validateAddressResponse.IsMine && validateAddressResponse.IsValid && !validateAddressResponse.IsScript)
                {
                    var privateKey = await DumpPrivKey(listReceivedByAddressResponse.Address);
                    keyPairs.Add(validateAddressResponse.PubKey, privateKey);
                }
            }

            await WalletLock();
            return keyPairs;
        }

        //  Note: As RPC's gettransaction works only for in-wallet transactions this had to be extended so it will work for every single transaction.
        public async Task<DecodeRawTransactionResponse> GetPublicTransaction(string txId)
        {
            var rawTransaction = (await GetRawTransaction(txId, 0)).Hex;
            return await DecodeRawTransaction(rawTransaction);
        }

        [Obsolete("Please use EstimateFee() instead. You can however keep on using this method until the network fully adjusts to the new rules on fee calculation")]
        public async Task<decimal> GetTransactionFee(CreateRawTransactionRequest transaction, bool checkIfTransactionQualifiesForFreeRelay, bool enforceMinimumTransactionFeePolicy)
        {
            if (checkIfTransactionQualifiesForFreeRelay && await IsTransactionFree(transaction))
            {
                return 0;
            }

            decimal transactionSizeInBytes = GetTransactionSizeInBytes(transaction);
            var transactionFee = ((transactionSizeInBytes / Parameters.FreeTransactionMaximumSizeInBytes) + (transactionSizeInBytes % Parameters.FreeTransactionMaximumSizeInBytes == 0 ? 0 : 1)) * Parameters.FeePerThousandBytesInCoins;

            if (transactionFee.GetNumberOfDecimalPlaces() > Parameters.CoinsPerBaseUnit.GetNumberOfDecimalPlaces())
            {
                transactionFee = Math.Round(transactionFee, Parameters.CoinsPerBaseUnit.GetNumberOfDecimalPlaces(), MidpointRounding.AwayFromZero);
            }

            if (enforceMinimumTransactionFeePolicy && Parameters.MinimumTransactionFeeInCoins != 0 && transactionFee < Parameters.MinimumTransactionFeeInCoins)
            {
                transactionFee = Parameters.MinimumTransactionFeeInCoins;
            }

            return transactionFee;
        }

        public async Task<GetRawTransactionResponse> GetRawTxFromImmutableTxId(string rigidTxId, int listTransactionsCount = int.MaxValue, int listTransactionsFrom = 0, bool getRawTransactionVersbose = true, bool rigidTxIdIsSha256 = false)
        {
            var allTransactions = await (this as ICoinService).ListTransactions("*", listTransactionsCount, listTransactionsFrom);

            foreach (var listTransactionsResponse in allTransactions)
            {
                if (rigidTxId == await GetImmutableTxId(listTransactionsResponse.TxId, rigidTxIdIsSha256))
                    return await GetRawTransaction(listTransactionsResponse.TxId, getRawTransactionVersbose ? 1 : 0)
                ;
            }
            return null;
        }

        public async Task<decimal> GetTransactionPriority(CreateRawTransactionRequest transaction)
        {
            if (transaction.Inputs.Count == 0)
            {
                return 0;
            }

            var unspentInputs = (await (this as ICoinService).ListUnspent(0)).ToList();
            var sumOfInputsValueInBaseUnitsMultipliedByTheirAge = transaction.Inputs.Select(input => unspentInputs.First(x => x.TxId == input.TxId)).Select(unspentResponse => (unspentResponse.Amount * Parameters.OneCoinInBaseUnits) * unspentResponse.Confirmations).Sum();
            return sumOfInputsValueInBaseUnitsMultipliedByTheirAge / GetTransactionSizeInBytes(transaction);
        }

        public decimal GetTransactionPriority(IList<ListUnspentResponse> transactionInputs, int numberOfOutputs)
        {
            if (transactionInputs.Count == 0)
            {
                return 0;
            }

            return transactionInputs.Sum(input => input.Amount * Parameters.OneCoinInBaseUnits * input.Confirmations) / GetTransactionSizeInBytes(transactionInputs.Count, numberOfOutputs);
        }

        //  Note: Be careful when using GetTransactionSenderAddress() as it just gives you an address owned by someone who previously controlled the transaction's outputs
        //  which might not actually be the sender (e.g. for e-wallets) and who may not intend to receive anything there in the first place. 
        [Obsolete("Please don't use this method in production enviroment, it's for testing purposes only")]
        public async Task<string> GetTransactionSenderAddress(string txId)
        {
            var rawTransaction = (await GetRawTransaction(txId, 0)).Hex;
            var decodedRawTransaction = await DecodeRawTransaction(rawTransaction);
            var transactionInputs = decodedRawTransaction.Vin;
            var rawTransactionHex = (await GetRawTransaction(transactionInputs[0].TxId, 0)).Hex;
            var inputDecodedRawTransaction = await DecodeRawTransaction(rawTransactionHex);
            var vouts = inputDecodedRawTransaction.Vout;
            return vouts[0].ScriptPubKey.Addresses[0];
        }

        public int GetTransactionSizeInBytes(CreateRawTransactionRequest transaction)
        {
            return GetTransactionSizeInBytes(transaction.Inputs.Count, transaction.Outputs.Count);
        }

        public int GetTransactionSizeInBytes(int numberOfInputs, int numberOfOutputs)
        {
            return numberOfInputs * Parameters.TransactionSizeBytesContributedByEachInput
                   + numberOfOutputs * Parameters.TransactionSizeBytesContributedByEachOutput
                   + Parameters.TransactionSizeFixedExtraSizeInBytes
                   + numberOfInputs;
        }

        public async Task<bool> IsInWalletTransaction(string txId)
        {
            //  Note: This might not be efficient if iterated, consider caching ListTransactions' results.
            return (await (this as ICoinService).ListTransactions(null, int.MaxValue)).Any(listTransactionsResponse => listTransactionsResponse.TxId == txId);
        }

        public async Task<bool> IsTransactionFree(CreateRawTransactionRequest transaction)
        {
            return transaction.Outputs.Any(x => x.Value < Parameters.FreeTransactionMinimumOutputAmountInCoins)
                   && GetTransactionSizeInBytes(transaction) < Parameters.FreeTransactionMaximumSizeInBytes
                   && (await GetTransactionPriority(transaction)) > Parameters.FreeTransactionMinimumPriority;
        }

        public bool IsTransactionFree(IList<ListUnspentResponse> transactionInputs, int numberOfOutputs, decimal minimumAmountAmongOutputs)
        {
            return minimumAmountAmongOutputs < Parameters.FreeTransactionMinimumOutputAmountInCoins
                   && GetTransactionSizeInBytes(transactionInputs.Count, numberOfOutputs) < Parameters.FreeTransactionMaximumSizeInBytes
                   && GetTransactionPriority(transactionInputs, numberOfOutputs) > Parameters.FreeTransactionMinimumPriority;
        }

        public async Task<bool> IsWalletEncrypted()
        {
            return !(await Help(RpcMethods.walletlock.ToString())).Contains("unknown command");
        }
    }
}