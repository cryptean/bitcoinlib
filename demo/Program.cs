// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using BitcoinLib.Auxiliary;
using BitcoinLib.ExceptionHandling.Rpc;
using BitcoinLib.Responses;
using BitcoinLib.Services.Coins.Base;
using BitcoinLib.Services.Coins.Bitcoin;

namespace ConsoleClient
{
    internal sealed class Program
    {
        private static readonly ICoinService CoinService = new BitcoinService(useTestnet: true);

        private static void Main()
        {
            try
            {
                Console.Write("\n\nConnecting to {0} {1}Net via RPC at {2}...", CoinService.Parameters.CoinLongName, (CoinService.Parameters.UseTestnet ? "Test" : "Main"), CoinService.Parameters.SelectedDaemonUrl);

                //  Network difficulty
                var networkDifficulty = CoinService.GetDifficulty();
                Console.WriteLine("[OK]\n\n{0} Network Difficulty: {1}", CoinService.Parameters.CoinLongName, networkDifficulty.ToString("#,###", CultureInfo.InvariantCulture));

                //  My balance
                var myBalance = CoinService.GetBalance();
                Console.WriteLine("\nMy balance: {0} {1}", myBalance, CoinService.Parameters.CoinShortName);

                //  Current block
                Console.WriteLine("Current block: {0}",
                    CoinService.GetBlockCount().ToString("#,#", CultureInfo.InvariantCulture));

                //  Wallet state
                Console.WriteLine("Wallet state: {0}", CoinService.IsWalletEncrypted() ? "Encrypted" : "Unencrypted");

                //  Keys and addresses
                if (myBalance > 0)
                {
                    //  My non-empty addresses
                    Console.WriteLine("\n\nMy non-empty addresses:");

                    var myNonEmptyAddresses = CoinService.ListReceivedByAddress();

                    foreach (var address in myNonEmptyAddresses)
                    {
                        Console.WriteLine("\n--------------------------------------------------");
                        Console.WriteLine("Account: " + (string.IsNullOrWhiteSpace(address.Account) ? "(no label)" : address.Account));
                        Console.WriteLine("Address: " + address.Address);
                        Console.WriteLine("Amount: " + address.Amount);
                        Console.WriteLine("Confirmations: " + address.Confirmations);
                        Console.WriteLine("--------------------------------------------------");
                    }

                    //  My private keys
                    if (bool.Parse(ConfigurationManager.AppSettings["ExtractMyPrivateKeys"]) && myNonEmptyAddresses.Count > 0 && CoinService.IsWalletEncrypted())
                    {
                        const short secondsToUnlockTheWallet = 30;

                        Console.Write("\nWill now unlock the wallet for " + secondsToUnlockTheWallet + ((secondsToUnlockTheWallet > 1) ? " seconds" : " second") + "...");
                        CoinService.WalletPassphrase(CoinService.Parameters.WalletPassword, secondsToUnlockTheWallet);
                        Console.WriteLine("[OK]\n\nMy private keys for non-empty addresses:\n");

                        foreach (var address in myNonEmptyAddresses)
                        {
                            Console.WriteLine("Private Key for address " + address.Address + ": " + CoinService.DumpPrivKey(address.Address));
                        }

                        Console.Write("\nLocking wallet...");
                        CoinService.WalletLock();
                        Console.WriteLine("[OK]");
                    }

                    //  My transactions 
                    Console.WriteLine("\n\nMy transactions: ");
                    var myTransactions = CoinService.ListTransactions(null, int.MaxValue, 0);

                    foreach (var transaction in myTransactions)
                    {
                        Console.WriteLine("\n---------------------------------------------------------------------------");
                        Console.WriteLine("Account: " + (string.IsNullOrWhiteSpace(transaction.Account) ? "(no label)" : transaction.Account));
                        Console.WriteLine("Address: " + transaction.Address);
                        Console.WriteLine("Category: " + transaction.Category);
                        Console.WriteLine("Amount: " + transaction.Amount);
                        Console.WriteLine("Fee: " + transaction.Fee);
                        Console.WriteLine("Confirmations: " + transaction.Confirmations);
                        Console.WriteLine("BlockHash: " + transaction.BlockHash);
                        Console.WriteLine("BlockIndex: " + transaction.BlockIndex);
                        Console.WriteLine("BlockTime: " + transaction.BlockTime + " - " + UnixTime.UnixTimeToDateTime(transaction.BlockTime));
                        Console.WriteLine("TxId: " + transaction.TxId);
                        Console.WriteLine("Time: " + transaction.Time + " - " + UnixTime.UnixTimeToDateTime(transaction.Time));
                        Console.WriteLine("TimeReceived: " + transaction.TimeReceived + " - " + UnixTime.UnixTimeToDateTime(transaction.TimeReceived));

                        if (!string.IsNullOrWhiteSpace(transaction.Comment))
                        {
                            Console.WriteLine("Comment: " + transaction.Comment);
                        }

                        if (!string.IsNullOrWhiteSpace(transaction.OtherAccount))
                        {
                            Console.WriteLine("Other Account: " + transaction.OtherAccount);
                        }

                        if (transaction.WalletConflicts.Any())
                        {
                            Console.Write("Conflicted Transactions: ");

                            foreach (var conflictedTxId in transaction.WalletConflicts)
                            {
                                Console.Write(conflictedTxId + " ");
                            }

                            Console.WriteLine();
                        }

                        Console.WriteLine("---------------------------------------------------------------------------");
                    }

                    //  Transaction Details
                    Console.WriteLine("\n\nMy transactions' details:");
                    foreach (var transaction in myTransactions)
                    {
                        var localWalletTransaction = CoinService.GetTransaction(transaction.TxId);
                        IEnumerable<PropertyInfo> localWalletTrasactionProperties = localWalletTransaction.GetType().GetProperties();
                        IList<GetTransactionResponseDetails> localWalletTransactionDetailsList = localWalletTransaction.Details.ToList();

                        Console.WriteLine("\nTransaction\n-----------");

                        foreach (var propertyInfo in localWalletTrasactionProperties)
                        {
                            var propertyInfoName = propertyInfo.Name;

                            if (propertyInfoName != "Details" && propertyInfoName != "WalletConflicts")
                            {
                                Console.WriteLine(propertyInfoName + ": " + propertyInfo.GetValue(localWalletTransaction, null));
                            }
                        }

                        foreach (var details in localWalletTransactionDetailsList)
                        {
                            IEnumerable<PropertyInfo> detailsProperties = details.GetType().GetProperties();
                            Console.WriteLine("\nTransaction details " + (localWalletTransactionDetailsList.IndexOf(details) + 1) + " of total " + localWalletTransactionDetailsList.Count + "\n--------------------------------");

                            foreach (var propertyInfo in detailsProperties)
                            {
                                Console.WriteLine(propertyInfo.Name + ": " + propertyInfo.GetValue(details, null));
                            }
                        }
                    }

                    //  Unspent transactions
                    Console.WriteLine("\nMy unspent transactions:");
                    var unspentList = CoinService.ListUnspent();

                    foreach (var unspentResponse in unspentList)
                    {
                        IEnumerable<PropertyInfo> detailsProperties = unspentResponse.GetType().GetProperties();

                        Console.WriteLine("\nUnspent transaction " + (unspentList.IndexOf(unspentResponse) + 1) + " of " + unspentList.Count + "\n--------------------------------");

                        foreach (var propertyInfo in detailsProperties)
                        {
                            Console.WriteLine(propertyInfo.Name + " : " + propertyInfo.GetValue(unspentResponse, null));
                        }
                    }
                }

                Console.ReadLine();
            }
            catch (RpcInternalServerErrorException exception)
            {
                var errorCode = 0;
                var errorMessage = string.Empty;

                if (exception.RpcErrorCode.GetHashCode() != 0)
                {
                    errorCode = exception.RpcErrorCode.GetHashCode();
                    errorMessage = exception.RpcErrorCode.ToString();
                }

                Console.WriteLine("[Failed] {0} {1} {2}", exception.Message, errorCode != 0 ? "Error code: " + errorCode : string.Empty, !string.IsNullOrWhiteSpace(errorMessage) ? errorMessage : string.Empty);
            }
            catch (Exception exception)
            {
                Console.WriteLine("[Failed]\n\nPlease check your configuration and make sure that the daemon is up and running and that it is synchronized. \n\nException: " + exception);
            }
        }
    }
}