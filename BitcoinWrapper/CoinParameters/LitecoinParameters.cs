using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinLib.CoinParameters
{
    public class LitecoinParameters : CoinParameters
    {
        public LitecoinParameters() : 
            base("BTC","Bitcoin",144,230,150,35,5000)
            //Litecoin client reports ~226 for single I/O, using 230 for safety in calculations
            //Litecoin client reports ~374 for two input, one output, using 150 (rather than incremental 148) for safety in calculations
            //Litecoin client reports ~260 for one input, two output, using 35 (rather than incremental 34) for safety in calculations
            //Litecoin allows up to 5000 bytes for free transaction, see function CTransaction::GetMinFee in src/main.cpp
        { }
    }
}
