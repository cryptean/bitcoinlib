// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Configuration;
using BitcoinLib.Auxiliary;
using BitcoinLib.Services.Coins.Base;
using BitcoinLib.Services.Coins.Bitcoin;
using BitcoinLib.Services.Coins.Cryptocoin;
using BitcoinLib.Services.Coins.Litecoin;

namespace BitcoinLib.Services
{
    public partial class CoinService
    {
        public CoinParameters Parameters { get; private set; }

        public class CoinParameters
        {
            private CoinParameters()
            {
                try
                {
                    RpcDelayResendingTimedOutRequests = Boolean.Parse(ConfigurationManager.AppSettings.Get("RpcDelayResendingTimedOutRequests"));
                    RpcRequestTimeoutInSeconds = Int16.Parse(ConfigurationManager.AppSettings.Get("RpcRequestTimeoutInSeconds"));
                    RpcResendTimedOutRequests = Boolean.Parse(ConfigurationManager.AppSettings.Get("RpcResendTimedOutRequests"));
                    RpcTimedOutRequestsResendAttempts = Int16.Parse(ConfigurationManager.AppSettings.Get("RpcTimedOutRequestsResendAttempts"));
                    RpcUnlockWalletImplicitlyWhenRequired = Boolean.Parse(ConfigurationManager.AppSettings.Get("RpcUnlockWalletImplicitlyWhenRequired"));
                    RpcUseBase2ExponentialDelaysWhenResendingTimedOutRequests = Boolean.Parse(ConfigurationManager.AppSettings.Get("RpcUseBase2ExponentialDelaysWhenResendingTimedOutRequests"));
                }
                catch (Exception)
                {
                    throw new Exception(String.Format("One or more required parameters, as defined in {0}, were not found in the configuration file!", GetType().Name));
                }
            }

            public CoinParameters(ICoinService coinService) : this()
            {
                if (coinService is BitcoinService)
                {
                    DaemonUrl = ConfigurationManager.AppSettings.Get("Bitcoin_DaemonUrl");
                    DaemonUrlTestnet = ConfigurationManager.AppSettings.Get("Bitcoin_DaemonUrl_Testnet");
                    RpcUsername = ConfigurationManager.AppSettings.Get("Bitcoin_RpcUsername");
                    RpcPassword = ConfigurationManager.AppSettings.Get("Bitcoin_RpcPassword");
                    WalletPassword = ConfigurationManager.AppSettings.Get("Bitcoin_WalletPassword");

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
                else if (coinService is LitecoinService)
                {
                    DaemonUrl = ConfigurationManager.AppSettings.Get("Litecoin_DaemonUrl");
                    DaemonUrlTestnet = ConfigurationManager.AppSettings.Get("Litecoin_DaemonUrl_Testnet");
                    RpcUsername = ConfigurationManager.AppSettings.Get("Litecoin_RpcUsername");
                    RpcPassword = ConfigurationManager.AppSettings.Get("Litecoin_RpcPassword");
                    WalletPassword = ConfigurationManager.AppSettings.Get("Litecoin_WalletPassword");

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
                else if (coinService is CryptocoinService)
                {
                    CoinShortName = "XXX";
                    CoinLongName = "Generic Cryptocoin Template";
                    IsoCurrencyCode = "XXX";

                    //  Note: The rest of the parameters will have to be defined at run-time
                }
                else
                {
                    throw new Exception("Unknown coin!");
                }
            }

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

            public Int32 ExpectedNumberOfBlocksGeneratedPerDay
            {
                get
                {
                    return (Int32) EstimatedBlockGenerationTimeInMinutes * GlobalConstants.MinutesInADay;
                }
            }

            public Decimal FeePerThousandBytesInCoins { get; set; }
            public Int16 FreeTransactionMaximumSizeInBytes { get; set; }
            public Decimal FreeTransactionMinimumOutputAmountInCoins { get; set; }
            public Int32 FreeTransactionMinimumPriority { get; set; }
            public String IsoCurrencyCode { get; set; }
            public Decimal MinimumNonDustTransactionAmountInCoins { get; set; }
            public Decimal MinimumTransactionFeeInCoins { get; set; }

            public Decimal OneBaseUnitInCoins
            {
                get
                {
                    return CoinsPerBaseUnit;
                }
            }

            public UInt32 OneCoinInBaseUnits
            {
                get
                {
                    return BaseUnitsPerCoin;
                }
            }

            public Boolean RpcDelayResendingTimedOutRequests { get; set; }
            public String RpcPassword { get; set; }
            public Int16 RpcRequestTimeoutInSeconds { get; set; }
            public Boolean RpcResendTimedOutRequests { get; set; }
            public Int16 RpcTimedOutRequestsResendAttempts { get; set; }
            public Boolean RpcUnlockWalletImplicitlyWhenRequired { get; set; }
            public Boolean RpcUseBase2ExponentialDelaysWhenResendingTimedOutRequests { get; set; }
            public String RpcUsername { get; set; }

            public String SelectedDaemonUrl
            {
                get
                {
                    return !UseTestnet ? DaemonUrl : DaemonUrlTestnet;
                }
            }

            public UInt32 TotalCoinSupplyInCoins { get; set; }
            public Int32 TransactionSizeBytesContributedByEachInput { get; set; }
            public Int32 TransactionSizeBytesContributedByEachOutput { get; set; }
            public Double? TransactionSizeCalculationIntenionalOverheadPercentage { get; set; } //  apply an overhead to make sure numbers will always be a bit bigger than what is considered safe for the tx to be broadcasted by the miners
            public Int32 TransactionSizeFixedExtraSizeInBytes { get; set; }
            public Boolean UseTestnet { get; set; }
            public String WalletPassword { get; set; }
        }
    }
}