using System.Collections.Generic;

namespace BitcoinLib.Responses
{
    public class GetAddressInfoResponse
    {
        public string Address { get; set; }
        public string ScriptPubKey { get; set; }
        public bool IsMine { get; set; }
        public bool IsWatchOnly { get; set; }
        public bool IsScript { get; set; }
        public bool IsWitness { get; set; }
        public List<LabelDetails> Labels { get; set; }
    }

    public class LabelDetails
    {
        public string Name { get; set; }
        public string Purpose { get; set; }
    }
}
