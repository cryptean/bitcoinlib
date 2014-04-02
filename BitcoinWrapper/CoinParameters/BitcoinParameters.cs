using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinLib.CoinParameters
{
    public class BitcoinParameters : CoinParameters
    {
        //JDA Note: These parameters intentionally include a small safety zone of +/- 1 byte on both initial and incremental inputs.  Users may derive initialize CoinParameters directly if exact values are needed
        
        public BitcoinParameters() : 
            base("BTC","Bitcoin",144,228,150,34,1000, 0.0001m,0.01m)
            //Bitcoin client reports ~226 for single I/O, , using 228 for safety in calculations (technically 226+-1)
            //Bitcoin client reports ~374 for two input, one output, using 150 (rather than incremental 148+-1) for safety in calculations
            //Bitcoin client reports ~260 for one input, two output, incremental is fixed at 34
            //Bitcoin allows up to 1000 bytes for free transaction, see function CTransaction::GetMinFee in src/main.cpp
        { }
    }
}
