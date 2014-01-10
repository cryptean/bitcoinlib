using System;
using Newtonsoft.Json;

namespace BitcoinWrapper.Requests.SignRawTransaction
{
    public class SignRawTransactionInput
    {
        [JsonProperty(PropertyName = "txid", Order = 0)]
        public String TransactionId { get; set; }

        [JsonProperty(PropertyName = "vout", Order = 1)]
        public Int32 Output { get; set; }

        [JsonProperty(PropertyName = "scriptPubKey", Order = 2)]
        public String ScriptPubKey { get; set; }

        [JsonProperty(PropertyName = "redeemScript", Order = 3)]
        public String RedeemScript { get; set; }
    }
}
