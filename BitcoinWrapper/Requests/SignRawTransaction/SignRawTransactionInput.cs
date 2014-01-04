using System;
using Newtonsoft.Json;

namespace BitcoinWrapper.Requests.SignRawTransaction
{
    public class SignRawTransactionInput
    {
        [JsonProperty("txid")]
        public String TransactionId { get; set; }

        [JsonProperty("vout")]
        public Int32 Output { get; set; }

        [JsonProperty("scriptPubKey")]
        public String ScriptPubKey { get; set; }
    }
}
