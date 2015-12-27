// Copyright (c) 2015 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Configuration;
using System.Diagnostics;
using BitcoinLib.Auxiliary;
using BitcoinLib.Services.Coins.Base;
using BitcoinLib.Services.Coins.Bitcoin;
using BitcoinLib.Services.Coins.Cryptocoin;
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
                String daemonUrl,
                String rpcUsername,
                String rpcPassword,
                String walletPassword,
                Int16 rpcRequestTimeoutInSeconds)
            {

                if (!String.IsNullOrWhiteSpace(daemonUrl))
                {
                    DaemonUrl = daemonUrl;
                    UseTestnet = false; //  this will force the CoinParameters.SelectedDaemonUrl dynamic property to automatically pick the daemonUrl defined above
                    IgnoreConfigFiles = true;
                    RpcUsername = rpcUsername;
                    RpcPassword = rpcPassword;
                    WalletPassword = walletPassword;
                }

                if (rpcRequestTimeoutInSeconds > 0)
                {
                    RpcRequestTimeoutInSeconds = rpcRequestTimeoutInSeconds;
                }
                else
                {
                    Int16 rpcRequestTimeoutTryParse = 0;

                    if (Int16.TryParse(ConfigurationManager.AppSettings.Get("RpcRequestTimeoutInSeconds"), out rpcRequestTimeoutTryParse))
                    {
                        RpcRequestTimeoutInSeconds = rpcRequestTimeoutTryParse;
                    }
                }

                if (IgnoreConfigFiles && (String.IsNullOrWhiteSpace(DaemonUrl) || String.IsNullOrWhiteSpace(RpcUsername) || String.IsNullOrWhiteSpace(RpcPassword)))
                {
                    throw new Exception(String.Format("One or more required parameters, as defined in {0}, were not found in the configuration file!", GetType().Name));
                }

                if (IgnoreConfigFiles && Debugger.IsAttached && String.IsNullOrWhiteSpace(WalletPassword))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[WARNING] The wallet password is either null or empty");
                    Console.ResetColor();
                }

                #region Bitcoin

                if (coinService is BitcoinService)
                {
                    if (!IgnoreConfigFiles)
                    {
                        DaemonUrl = ConfigurationManager.AppSettings.Get("Bitcoin_DaemonUrl");
                        DaemonUrlTestnet = ConfigurationManager.AppSettings.Get("Bitcoin_DaemonUrl_Testnet");
                        RpcUsername = ConfigurationManager.AppSettings.Get("Bitcoin_RpcUsername");
                        RpcPassword = ConfigurationManager.AppSettings.Get("Bitcoin_RpcPassword");
                        WalletPassword = ConfigurationManager.AppSettings.Get("Bitcoin_WalletPassword");
                    }

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
                    if (!IgnoreConfigFiles)
                    {
                        DaemonUrl = ConfigurationManager.AppSettings.Get("Litecoin_DaemonUrl");
                        DaemonUrlTestnet = ConfigurationManager.AppSettings.Get("Litecoin_DaemonUrl_Testnet");
                        RpcUsername = ConfigurationManager.AppSettings.Get("Litecoin_RpcUsername");
                        RpcPassword = ConfigurationManager.AppSettings.Get("Litecoin_RpcPassword");
                        WalletPassword = ConfigurationManager.AppSettings.Get("Litecoin_WalletPassword");
                    }

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
                    if (!IgnoreConfigFiles)
                    {
                        DaemonUrl = ConfigurationManager.AppSettings.Get("Dogecoin_DaemonUrl");
                        DaemonUrlTestnet = ConfigurationManager.AppSettings.Get("Dogecoin_DaemonUrl_Testnet");
                        RpcUsername = ConfigurationManager.AppSettings.Get("Dogecoin_RpcUsername");
                        RpcPassword = ConfigurationManager.AppSettings.Get("Dogecoin_RpcPassword");
                        WalletPassword = ConfigurationManager.AppSettings.Get("Dogecoin_WalletPassword");
                    }

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
                    if (!IgnoreConfigFiles)
                    {
                        DaemonUrl = ConfigurationManager.AppSettings.Get("Sarcoin_DaemonUrl");
                        DaemonUrlTestnet = ConfigurationManager.AppSettings.Get("Sarcoin_DaemonUrl_Testnet");
                        RpcUsername = ConfigurationManager.AppSettings.Get("Sarcoin_RpcUsername");
                        RpcPassword = ConfigurationManager.AppSettings.Get("Sarcoin_RpcPassword");
                        WalletPassword = ConfigurationManager.AppSettings.Get("Sarcoin_WalletPassword");
                    }

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

                else if (coinService is CryptocoinService)
                {
                    CoinShortName = "XXX";
                    CoinLongName = "Generic Cryptocoin Template";
                    IsoCurrencyCode = "XXX";

                    //  Note: The rest of the parameters will have to be defined at run-time
                }

                #endregion

                #region Uknown coin exception

                else
                {
                    throw new Exception("Unknown coin!");
                }

                #endregion

                #region Invalid configuration / Missing parameters

                if (RpcRequestTimeoutInSeconds <= 0)
                {
                    throw new Exception("RpcRequestTimeoutInSeconds must be greater than zero");
                }

                if (   String.IsNullOrWhiteSpace(DaemonUrl)
                    || String.IsNullOrWhiteSpace(RpcUsername)
                    || String.IsNullOrWhiteSpace(RpcPassword))
                {
                    throw new Exception(String.Format("One or more required parameters, as defined in {0}, were not found in the configuration file!", GetType().Name));
                }

                #endregion
            }

            #endregion

            public String BaseUnitName { get; set; }
            public UInt32 BaseUnitsPerCoin { get; set; }
            public Int32 BlocksHighestPriorityTransactionsReservedSizeInBytes { get; set; }
            public Int32 BlockMaximumSizeInBytes { get; set; }
            public String CoinShortName { get; set; }
            public String CoinLongName { get; set; }
            public Decimal CoinsPerBaseUnit { get; set; }
            public String DaemonUrl { private get; set; }
            public String DaemonUrlTestnet { private get; set; }
            public Double EstimatedBlockGenerationTimeInMinutes { get; set; }
            public Int32 ExpectedNumberOfBlocksGeneratedPerDay => (Int32)EstimatedBlockGenerationTimeInMinutes * GlobalConstants.MinutesInADay;
            public Decimal FeePerThousandBytesInCoins { get; set; }
            public Int16 FreeTransactionMaximumSizeInBytes { get; set; }
            public Decimal FreeTransactionMinimumOutputAmountInCoins { get; set; }
            public Int32 FreeTransactionMinimumPriority { get; set; }
            public Boolean IgnoreConfigFiles { get; }
            public String IsoCurrencyCode { get; set; }
            public Decimal MinimumNonDustTransactionAmountInCoins { get; set; }
            public Decimal MinimumTransactionFeeInCoins { get; set; }
            public Decimal OneBaseUnitInCoins => CoinsPerBaseUnit;
            public UInt32 OneCoinInBaseUnits => BaseUnitsPerCoin;
            public String RpcPassword { get; set; }
            public Int16 RpcRequestTimeoutInSeconds { get; set; }
            public String RpcUsername { get; set; }
            public String SelectedDaemonUrl => !UseTestnet ? DaemonUrl : DaemonUrlTestnet;
            public UInt64 TotalCoinSupplyInCoins { get; set; }
            public Int32 TransactionSizeBytesContributedByEachInput { get; set; }
            public Int32 TransactionSizeBytesContributedByEachOutput { get; set; }
            public Int32 TransactionSizeFixedExtraSizeInBytes { get; set; }
            public Boolean UseTestnet { get; set; }
            public String WalletPassword { get; set; }
        }
    }
}