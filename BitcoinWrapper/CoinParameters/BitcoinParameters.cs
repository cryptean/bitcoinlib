using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinLib.CoinParameters
{
    public class BitcoinParameters : CoinParameters
    {
        public BitcoinParameters() : 
            base("BTC","Bitcoin",144,250,150,35,1000)
            //Bitcoin client reports ~226 for single I/O, using 230 for safety in calculations
            //Bitcoin client reports ~374 for two input, one output, using 150 (rather than incremental 148) for safety in calculations
            //Bitcoin client reports ~260 for one input, two output, using 35 (rather than incremental 34) for safety in calculations
            //Bitcoin allows up to 1000 bytes for free transaction, see function CTransaction::GetMinFee in src/main.cpp
        { }
    }
}
