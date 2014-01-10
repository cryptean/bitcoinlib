using System;
using Newtonsoft.Json;

namespace BitcoinLib.Requests.CreateRawTransaction
{
    public class CreateRawTransactionInput
    {
        [JsonProperty("txid")]
        public String TransactionId { get; set; }

        [JsonProperty("vout")]
        public Int32 Output { get; set; }
    }
}