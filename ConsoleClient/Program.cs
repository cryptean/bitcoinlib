// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using BitcoinLib.Auxiliary;
using BitcoinLib.Responses;
using BitcoinLib.Services.Coins.Base;
using BitcoinLib.Services.Coins.Bitcoin;

namespace ConsoleClient
{
    internal sealed class Program
    {
        private static readonly ICoinService CoinService = new BitcoinService(useTestnet:true);

        private static void Main()
        {
            Console.Write("\n\nConnecting to {0} {1}Net via RPC at {2}...", CoinService.Parameters.CoinLongName, (CoinService.Parameters.UseTestnet ? "Test" : "Main"), CoinService.Parameters.SelectedDaemonUrl);

            //  Network difficulty
            try
            {
                Double networkDifficulty = CoinService.GetDifficulty();
                Console.WriteLine("[OK]\n\n{0} Network Difficulty: {1}", CoinService.Parameters.CoinLongName, networkDifficulty.ToString("#,###", CultureInfo.InvariantCulture));
            }
            catch (Exception exception)
            {
                Console.WriteLine("[Failed]\n\nPlease check your configuration and make sure that the daemon is up and running and that it is synchronized. \n\nException: " + exception);
                return;
            }

            //  My balance
            Decimal myBalance = CoinService.GetBalance();
            Console.WriteLine("\nMy balance: {0} {1}", myBalance, CoinService.Parameters.CoinShortName);

            //  Current block
            Console.WriteLine("Current block: {0}", CoinService.GetBlockCount().ToString("#,#", CultureInfo.InvariantCulture));

            //  Wallet state
            Console.WriteLine("Wallet state: {0}", CoinService.IsWalletEncrypted() ? "Encrypted" : "Unencrypted");

            //  Keys and addresses
            if (myBalance > 0)
            {
                //  My non-empty addresses
                Console.WriteLine("\n\nMy non-empty addresses:");

                List<ListReceivedByAddressResponse> myNonEmptyAddresses = CoinService.ListReceivedByAddress();

                foreach (ListReceivedByAddressResponse address in myNonEmptyAddresses)
                {
                    Console.WriteLine("\n--------------------------------------------------");
                    Console.WriteLine("Account: " + (String.IsNullOrWhiteSpace(address.Account) ? "(no label)" : address.Account));
                    Console.WriteLine("Address: " + address.Address);
                    Console.WriteLine("Amount: " + address.Amount);
                    Console.WriteLine("Confirmations: " + address.Confirmations);
                    Console.WriteLine("--------------------------------------------------");
                }

                //  My private keys
                if (Boolean.Parse(ConfigurationManager.AppSettings["ExtractMyPrivateKeys"]) && myNonEmptyAddresses.Count > 0 && CoinService.IsWalletEncrypted())
                {
                    const Int16 secondsToUnlockTheWallet = 3;

                    try
                    {
                        Console.Write("\nWill now unlock the wallet for " + secondsToUnlockTheWallet + ((secondsToUnlockTheWallet > 1) ? " seconds" : " second") + "...");
                        CoinService.WalletPassphrase(CoinService.Parameters.WalletPassword, secondsToUnlockTheWallet);
                        Console.WriteLine("[OK]\n\nMy private keys for non-empty addresses:\n");

                        foreach (ListReceivedByAddressResponse address in myNonEmptyAddresses)
                        {
                            Console.WriteLine("Private Key for address " + address.Address + ": " + CoinService.DumpPrivKey(address.Address));
                        }
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("[Failed]. The wallet could not be unlocked, did you use the correct password?\n");
                        throw;
                    }
                    finally
                    {
                        Console.Write("\nLocking wallet...");
                        CoinService.WalletLock();
                        Console.WriteLine("[OK]");
                    }
                }

                //  My transactions 
                Console.WriteLine("\n\nMy transactions: ");
                List<ListTransactionsResponse> myTransactions = CoinService.ListTransactions(null, Int32.MaxValue, 0);

                foreach (ListTransactionsResponse transaction in myTransactions)
                {
                    Console.WriteLine("\n---------------------------------------------------------------------------");
                    Console.WriteLine("Account: " + (String.IsNullOrWhiteSpace(transaction.Account) ? "(no label)" : transaction.Account));
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

                    if (!String.IsNullOrWhiteSpace(transaction.Comment))
                    {
                        Console.WriteLine("Comment: " + transaction.Comment);
                    }

                    if (!String.IsNullOrWhiteSpace(transaction.OtherAccount))
                    {
                        Console.WriteLine("Other Account: " + transaction.OtherAccount);
                    }

                    if (transaction.WalletConflicts.Any())
                    {
                        Console.Write("Conflicted Transactions: ");

                        foreach (String conflictedTxId in transaction.WalletConflicts)
                        {
                            Console.Write(conflictedTxId + " ");
                        }

                        Console.WriteLine();
                    }

                    Console.WriteLine("---------------------------------------------------------------------------");
                }

                //  Transaction Details
                Console.WriteLine("\n\nMy transactions' details:");
                foreach (ListTransactionsResponse transaction in myTransactions)
                {
                    GetTransactionResponse localWalletTransaction = CoinService.GetTransaction(transaction.TxId);
                    IEnumerable<PropertyInfo> localWalletTrasactionProperties = localWalletTransaction.GetType().GetProperties();
                    IList<GetTransactionResponseDetails> localWalletTransactionDetailsList = localWalletTransaction.Details.ToList();

                    Console.WriteLine("\nTransaction\n-----------");
                    foreach (PropertyInfo propertyInfo in localWalletTrasactionProperties)
                    {
                        String propertyInfoName = propertyInfo.Name;

                        if (propertyInfoName != "Details" && propertyInfoName != "WalletConflicts")
                        {
                            Console.WriteLine(propertyInfoName + ": " + propertyInfo.GetValue(localWalletTransaction, null));
                        }
                    }

                    foreach (GetTransactionResponseDetails details in localWalletTransactionDetailsList)
                    {
                        IEnumerable<PropertyInfo> detailsProperties = details.GetType().GetProperties();
                        Console.WriteLine("\nTransaction details " + (localWalletTransactionDetailsList.IndexOf(details) + 1) + " of total " + localWalletTransactionDetailsList.Count + "\n--------------------------------");

                        foreach (PropertyInfo propertyInfo in detailsProperties)
                        {
                            Console.WriteLine(propertyInfo.Name + ": " + propertyInfo.GetValue(details, null));
                        }
                    }
                }

                //  Unspent transactions
                Console.WriteLine("My unspent transactions:");
                List<ListUnspentResponse> unspentList = CoinService.ListUnspent();

                foreach (ListUnspentResponse unspentResponse in unspentList)
                {
                    IEnumerable<PropertyInfo> detailsProperties = unspentResponse.GetType().GetProperties();

                    Console.WriteLine("\nUnspent transaction " + (unspentList.IndexOf(unspentResponse) + 1) + " of " + unspentList.Count + "\n--------------------------------");

                    foreach (PropertyInfo propertyInfo in detailsProperties)
                    {
                        Console.WriteLine(propertyInfo.Name + " : " + propertyInfo.GetValue(unspentResponse, null));
                    }
                }
            }
            Console.ReadLine();
        }
    }
}