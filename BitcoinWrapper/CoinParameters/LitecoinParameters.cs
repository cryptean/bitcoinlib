using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinLib.CoinParameters
{
    public class LitecoinParameters : CoinParameters
    {
        //JDA Note: These parameters intentionally include a small safety zone of +/- 1 byte on both initial and incremental inputs.  Users may derive initialize CoinParameters directly if exact values are needed

        public LitecoinParameters() : 
            base("LTC","Litecoin",576,228,150,34,5000,0.001m,0.01m)
            //Litecoin client reports ~226 for single I/O, using 228 for safety in calculations (technically 226+-1)
            //Litecoin client reports ~374 for two input, one output, using 150 (rather than incremental 148+-1) for safety in calculations
            //Litecoin client reports ~260 for one input, two output, incremental is fixed at 34
            //Litecoin allows up to 5000 bytes for free transaction, see function CTransaction::GetMinFee in src/main.cpp
        { }
    }
}
