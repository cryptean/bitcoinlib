using System.Collections.Generic;

namespace BitcoinLib.Requests.FundRawTransaction
{
    /// <summary>
    /// 
    /// </summary>
    public class FundRawTransactionOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, object> Data = new Dictionary<string, object>();

        /// <summary>
        /// (string, optional, default=pool address) The Bitcoin Cash address to receive the change
        /// </summary>
        public string changeAddress { set => Data[nameof(changeAddress)] = value; }

        /// <summary>
        /// (numeric, optional, default=random) The index of the change output
        /// </summary>
        public double? changePosition { set => Data[nameof(changePosition)] = value; }

        /// <summary>
        /// (boolean, optional, default=false) Also select inputs which are watch only
        /// </summary>
        public bool? includeWatching { set => Data[nameof(includeWatching)] = value; }

        /// <summary>
        ///  (boolean, optional, default=false) Lock selected unspent outputs
        /// </summary>
        public bool? lockUnspents { set => Data[nameof(lockUnspents)] = value; }

        /// <summary>
        /// (numeric or string, optional, default=not set: makes wallet determine the fee) Set a specific fee rate in BCH/kB
        /// </summary>
        public double? feeRate { set => Data[nameof(feeRate)] = value; }

        /// <summary>
        /// [    (json array, optional) A json array of integers.
        ///        The fee will be equally deducted from the amount of each specified output.
        ///        The outputs are specified by their zero-based index, before any change output is added.
        ///        Those recipients will receive less bitcoins than you enter in their corresponding amount field.
        ///        If no outputs are specified here, the sender pays the fee.
        ///        vout_index,                  (numeric) The zero-based output index, before a change output is added.
        ///    ...
        ///        ]
        /// </summary>
        public List<int> subtractFeeFromOutputs { set => Data[nameof(subtractFeeFromOutputs)] = value; }
    }
}
