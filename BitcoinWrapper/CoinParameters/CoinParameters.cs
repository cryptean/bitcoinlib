using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinLib.CoinParameters
{
    public class CoinParameters
    {
        int transactionBaseSizeSingleInputOutput;

        public int TransactionBaseSizeSingleIO
        {
            get { return transactionBaseSizeSingleInputOutput; }
        }

        int transactionIncrementalInputSize;
        public int TransactionIncrementalInputSize
        {
            get { return transactionIncrementalInputSize; }
        }

        int transactionIncrementalOutputSize;

        public int TransactionIncrementalOutputSize
        {
            get { return transactionIncrementalOutputSize; }
        }
        
        int allowedFreeSize;

        public int AllowedFreeSize
        {
            get { return allowedFreeSize; }
        }

        string coinShortName;

        public string CoinShortName
        {
            get { return coinShortName; }
        }
        
        string coinLongName;

        public string CoinLongName
        {
            get { return coinLongName; }
        }
        
        int confirmationsForMediumAtOneCoin;

        public int ConfirmationsForMediumAtOneCoin
        {
            get { return confirmationsForMediumAtOneCoin; }
        }

        decimal feePerKB;

        public decimal FeePerKB
        {
            get { return feePerKB; }
            set { feePerKB = value; }
        }

        decimal dustThreshold;

        public decimal DustThreshold
        {
          get { return dustThreshold; }
          set { dustThreshold = value; }
        }
        
        public CoinParameters(string shortName, string longName, int confsForMedium, int baseTXSize, int incrInputSize, int incrOutputSize, int allowFreeSize, decimal feePerKilobyte, decimal dustThresholdIn)
        {
            confirmationsForMediumAtOneCoin = confsForMedium;
            coinShortName = shortName;
            coinLongName = longName;
            transactionBaseSizeSingleInputOutput = baseTXSize;
            transactionIncrementalInputSize = incrInputSize;
            transactionIncrementalOutputSize = incrOutputSize;
            allowedFreeSize = allowFreeSize;
            feePerKB = feePerKilobyte;
            dustThreshold = dustThresholdIn;
        }
    }
}
