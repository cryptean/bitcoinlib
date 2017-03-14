// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement
using System;
using System.Diagnostics;
using BitcoinLib.Auxiliary;
using BitcoinLib.Services.Coins.Base;
using BitcoinLib.Services.Coins.Bitcoin;
using BitcoinLib.Services.Coins.Dogecoin;
using BitcoinLib.Services.Coins.Litecoin;
using BitcoinLib.Services.Coins.Sarcoin;

namespace BitcoinLib.Services
{
    public partial class CoinService
    {
        public CoinParameters Parameters { get; }

        public class CoinParameters
        {
            #region Constructor

            public CoinParameters(ICoinService coinService,
                string daemonUrl,
                string rpcUsername,
                string rpcPassword,
                string walletPassword,
                short rpcRequestTimeoutInSeconds = 10)
            {
                if (!string.IsNullOrWhiteSpace(daemonUrl))
                {
                    DaemonUrl = daemonUrl;
                    UseTestnet = false; //  this will force the CoinParameters.SelectedDaemonUrl dynamic property to automatically pick the daemonUrl defined above
                    RpcUsername = rpcUsername;
                    RpcPassword = rpcPassword;
                    WalletPassword = walletPassword;
                }

                if (rpcRequestTimeoutInSeconds > 0)
                {
                    RpcRequestTimeoutInSeconds = rpcRequestTimeoutInSeconds;
                }

                if ((string.IsNullOrWhiteSpace(DaemonUrl) || string.IsNullOrWhiteSpace(RpcUsername) || string.IsNullOrWhiteSpace(RpcPassword)))
                {
                    throw new Exception($"One or more required parameters, as defined in {GetType().Name}, were not found in the configuration file!");
                }

                if (Debugger.IsAttached && string.IsNullOrWhiteSpace(WalletPassword))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[WARNING] The wallet password is either null or empty");
                    Console.ResetColor();
                }

                #region Bitcoin

                if (coinService is BitcoinService)
                {
                    CoinShortName = "BTC";
                    CoinLongName = "Bitcoin";
                    IsoCurrencyCode = "XBT";

                    TransactionSizeBytesContributedByEachInput = 148;
                    TransactionSizeBytesContributedByEachOutput = 34;
                    TransactionSizeFixedExtraSizeInBytes = 10;

                    FreeTransactionMaximumSizeInBytes = 1000;
                    FreeTransactionMinimumOutputAmountInCoins = 0.01M;
                    FreeTransactionMinimumPriority = 57600000;
                    FeePerThousandBytesInCoins = 0.0001M;
                    MinimumTransactionFeeInCoins = 0.0001M;
                    MinimumNonDustTransactionAmountInCoins = 0.0000543M;

                    TotalCoinSupplyInCoins = 21000000;
                    EstimatedBlockGenerationTimeInMinutes = 10;
                    BlocksHighestPriorityTransactionsReservedSizeInBytes = 50000;

                    BaseUnitName = "Satoshi";
                    BaseUnitsPerCoin = 100000000;
                    CoinsPerBaseUnit = 0.00000001M;
                }

                #endregion

                #region Litecoin

                else if (coinService is LitecoinService)
                {
                    CoinShortName = "LTC";
                    CoinLongName = "Litecoin";
                    IsoCurrencyCode = "XLT";

                    TransactionSizeBytesContributedByEachInput = 148;
                    TransactionSizeBytesContributedByEachOutput = 34;
                    TransactionSizeFixedExtraSizeInBytes = 10;

                    FreeTransactionMaximumSizeInBytes = 5000;
                    FreeTransactionMinimumOutputAmountInCoins = 0.001M;
                    FreeTransactionMinimumPriority = 230400000;
                    FeePerThousandBytesInCoins = 0.001M;
                    MinimumTransactionFeeInCoins = 0.001M;
                    MinimumNonDustTransactionAmountInCoins = 0.001M;

                    TotalCoinSupplyInCoins = 84000000;
                    EstimatedBlockGenerationTimeInMinutes = 2.5;
                    BlocksHighestPriorityTransactionsReservedSizeInBytes = 16000;
                    BlockMaximumSizeInBytes = 250000;

                    BaseUnitName = "Litetoshi";
                    BaseUnitsPerCoin = 100000000;
                    CoinsPerBaseUnit = 0.00000001M;
                }

                #endregion

                #region Dogecoin

                else if (coinService is DogecoinService)
                {
                    CoinShortName = "Doge";
                    CoinLongName = "Dogecoin";
                    IsoCurrencyCode = "XDG";
                    TransactionSizeBytesContributedByEachInput = 148;
                    TransactionSizeBytesContributedByEachOutput = 34;
                    TransactionSizeFixedExtraSizeInBytes = 10;
                    FreeTransactionMaximumSizeInBytes = 1; // free txs are not supported from v.1.8+
                    FreeTransactionMinimumOutputAmountInCoins = 1;
                    FreeTransactionMinimumPriority = 230400000;
                    FeePerThousandBytesInCoins = 1;
                    MinimumTransactionFeeInCoins = 1;
                    MinimumNonDustTransactionAmountInCoins = 0.1M;
                    TotalCoinSupplyInCoins = 100000000000;
                    EstimatedBlockGenerationTimeInMinutes = 1;
                    BlocksHighestPriorityTransactionsReservedSizeInBytes = 16000;
                    BlockMaximumSizeInBytes = 500000;
                    BaseUnitName = "Koinu";
                    BaseUnitsPerCoin = 100000000;
                    CoinsPerBaseUnit = 0.00000001M;
                }

                #endregion

                #region Sarcoin

                else if (coinService is SarcoinService)
                {
                    CoinShortName = "SAR";
                    CoinLongName = "Sarcoin";
                    IsoCurrencyCode = "SAR";

                    TransactionSizeBytesContributedByEachInput = 148;
                    TransactionSizeBytesContributedByEachOutput = 34;
                    TransactionSizeFixedExtraSizeInBytes = 10;

                    FreeTransactionMaximumSizeInBytes = 0;
                    FreeTransactionMinimumOutputAmountInCoins = 0;
                    FreeTransactionMinimumPriority = 0;
                    FeePerThousandBytesInCoins = 0.00001M;
                    MinimumTransactionFeeInCoins = 0.00001M;
                    MinimumNonDustTransactionAmountInCoins = 0.00001M;

                    TotalCoinSupplyInCoins = 2000000000;
                    EstimatedBlockGenerationTimeInMinutes = 1.5;
                    BlocksHighestPriorityTransactionsReservedSizeInBytes = 50000;

                    BaseUnitName = "Satoshi";
                    BaseUnitsPerCoin = 100000000;
                    CoinsPerBaseUnit = 0.00000001M;
                }

                #endregion

                #region Agnostic coin (cryptocoin)

                else  
                {
                    CoinShortName = "XXX";
                    CoinLongName = "Generic Cryptocoin Template";
                    IsoCurrencyCode = "XXX";

                    //  Note: The rest of the parameters will have to be defined at run-time
                }

                #endregion 
            }

            #endregion

            public string BaseUnitName { get; set; }
            public uint BaseUnitsPerCoin { get; set; }
            public int BlocksHighestPriorityTransactionsReservedSizeInBytes { get; set; }
            public int BlockMaximumSizeInBytes { get; set; }
            public string CoinShortName { get; set; }
            public string CoinLongName { get; set; }
            public decimal CoinsPerBaseUnit { get; set; }
            public string DaemonUrl { private get; set; }
            public string DaemonUrlTestnet { private get; set; }
            public double EstimatedBlockGenerationTimeInMinutes { get; set; }
            public int ExpectedNumberOfBlocksGeneratedPerDay => (int)EstimatedBlockGenerationTimeInMinutes * GlobalConstants.MinutesInADay;
            public decimal FeePerThousandBytesInCoins { get; set; }
            public short FreeTransactionMaximumSizeInBytes { get; set; }
            public decimal FreeTransactionMinimumOutputAmountInCoins { get; set; }
            public int FreeTransactionMinimumPriority { get; set; }
            public string IsoCurrencyCode { get; set; }
            public decimal MinimumNonDustTransactionAmountInCoins { get; set; }
            public decimal MinimumTransactionFeeInCoins { get; set; }
            public decimal OneBaseUnitInCoins => CoinsPerBaseUnit;
            public uint OneCoinInBaseUnits => BaseUnitsPerCoin;
            public string RpcPassword { get; set; }
            public short RpcRequestTimeoutInSeconds { get; set; }
            public string RpcUsername { get; set; }
            public string SelectedDaemonUrl => !UseTestnet ? DaemonUrl : DaemonUrlTestnet;
            public ulong TotalCoinSupplyInCoins { get; set; }
            public int TransactionSizeBytesContributedByEachInput { get; set; }
            public int TransactionSizeBytesContributedByEachOutput { get; set; }
            public int TransactionSizeFixedExtraSizeInBytes { get; set; }
            public bool UseTestnet { get; set; }
            public string WalletPassword { get; set; }
        }
    }
}