using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using BitcoinWrapper.Auxiliary;
using BitcoinWrapper.Responses;
using BitcoinWrapper.Services;

namespace ConsoleClient
{
    internal sealed class Program
    {
        private static readonly IRpcService RpcService = new RpcService();
        private static readonly Boolean UseTestNet = Boolean.Parse(ConfigurationManager.AppSettings.Get("UseTestNet"));
        private static readonly String DaemonUrl = !UseTestNet ? ConfigurationManager.AppSettings.Get("DaemonUrl") : ConfigurationManager.AppSettings.Get("TestNetDaemonUrl");

        static void Main()
        {
            Console.Write("\n\nConnecting to Bitcoin {0}Net via QT RPC API at {1}...", (UseTestNet ? "Test" : "Main"), DaemonUrl);

            //  Network difficulty
            try
            {
                Double networkDifficulty = RpcService.GetDifficulty();
                Console.WriteLine("[OK]\n\nBTC Network Difficulty: " + networkDifficulty.ToString("#,#", CultureInfo.InvariantCulture));
            }
            catch (Exception exception)
            {
                Console.WriteLine("[Failed]\n\nPlease check your configuration and make sure that the deamon is up and running and that it is synchronized. \n\nException: " + exception);
                return;
            }

            //  My balance
            Decimal myBalance = RpcService.GetBalance();
            Console.WriteLine("\nMy balance: " + myBalance + " BTC");

            //  Current block
            Int32 blockCount = RpcService.GetBlockCount();
            Console.WriteLine("\nCurrent block: {0} Hash: {1}", blockCount.ToString("#,#", CultureInfo.InvariantCulture), RpcService.GetBlockHash(blockCount));

            //  Keys and addresses
            if (myBalance > 0)
            {
                //  My non-empty addresses
                Console.WriteLine("\n\nMy non-empty addresses:");

                List<ListReceivedByAddressResponse> myNonEmptyAddresses = RpcService.ListReceivedByAddress();

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
                if (Boolean.Parse(ConfigurationManager.AppSettings["ExtractMyPrivateKeys"]) && myNonEmptyAddresses.Count > 0)
                {
                    String walletPassword = ConfigurationManager.AppSettings.Get("WalletPassword");   //  be careful with this one - please do not store your password here //
                    const Int16 secondsToUnlockTheWallet = 3;

                    try
                    {
                        Console.Write("\nWill now unlock the wallet for " + secondsToUnlockTheWallet + ((secondsToUnlockTheWallet > 1) ? " seconds" : " second") + "...");
                        RpcService.WalletPassphrase(walletPassword, secondsToUnlockTheWallet);
                        Console.WriteLine("[OK]");
                        Console.WriteLine("\nMy private keys for non-empty addresses:\n");

                        foreach (ListReceivedByAddressResponse address in myNonEmptyAddresses)
                        {
                            Console.WriteLine("Private Key for " + address.Address + ": " + RpcService.DumpPrivKey(address.Address));
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
                        RpcService.WalletLock();
                        Console.WriteLine("[OK]");
                    }
                }

                //  My transactions 
                Console.WriteLine("\n\nMy transactions: ");
                List<ListTransactionsResponse> myTransactions = RpcService.ListTransactions();

                foreach (ListTransactionsResponse transaction in myTransactions)
                {
                    Console.WriteLine("\n---------------------------------------------------------------------------");
                    Console.WriteLine("Account: " + (String.IsNullOrWhiteSpace(transaction.Account) ? "(no label)" : transaction.Account));
                    Console.WriteLine("Address: " + transaction.Address);
                    Console.WriteLine("Category: " + transaction.Category);
                    Console.WriteLine("Amount: " + transaction.Amount);
                    Console.WriteLine("Confirmations: " + transaction.Confirmations);
                    Console.WriteLine("BlockHash: " + transaction.BlockHash);
                    Console.WriteLine("BlockIndex: " + transaction.BlockIndex);
                    Console.WriteLine("BlockTime: " + transaction.BlockTime + " - " + UnixTime.UnixTimeToDateTime(transaction.BlockTime));
                    Console.WriteLine("TxId: " + transaction.TxId);
                    Console.WriteLine("Time: " + transaction.Time + " - " + UnixTime.UnixTimeToDateTime(transaction.Time));
                    Console.WriteLine("TimeReceived: " + transaction.TimeReceived + " - " + UnixTime.UnixTimeToDateTime(transaction.TimeReceived));
                    Console.WriteLine("---------------------------------------------------------------------------");
                }

                //  Transaction Details
                Console.WriteLine("\n\nMy transactions' details:");
                foreach (ListTransactionsResponse transaction in myTransactions)
                {
                    //Console.WriteLine("\nRetrieving information for transaction with id: " + transaction.TxId);
                    GetTransactionResponse localWalletTransaction = RpcService.GetTransaction(transaction.TxId);
                    IEnumerable<PropertyInfo> localWalletTrasactionProperties = localWalletTransaction.GetType().GetProperties();
                    IList<GetTransactionResponseDetails> localWalletTransactionDetailsList = localWalletTransaction.Details.ToList();

                    Console.WriteLine("\nTransaction\n-----------");
                    foreach (PropertyInfo propertyInfo in localWalletTrasactionProperties)
                    {
                        String propertyInfoName = propertyInfo.Name;

                        if (propertyInfoName != "Details")
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
                Console.WriteLine("My unspect transactions:");
                List<ListUnspentResponse> unspentList = RpcService.ListUnspent();

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
