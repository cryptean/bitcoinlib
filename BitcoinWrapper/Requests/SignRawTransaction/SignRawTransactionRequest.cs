using System;
using System.Collections.Generic;

namespace BitcoinWrapper.Requests.SignRawTransaction
{
    public class SignRawTransactionRequest
    {
        public String RawTransactionHex { get; set; }
        public List<SignRawTransactionInput> Inputs { get; set; }
        public List<String> PrivateKeys { get; set; }
        public String SigHashType { get; set; }

        public SignRawTransactionRequest(String rawTransactionHex)
        {
            RawTransactionHex = rawTransactionHex;
            Inputs = new List<SignRawTransactionInput>();
            PrivateKeys = new List<String>();
            SigHashType = SignRawTransaction.SigHashType.All;
        }

        public void AddInput(String transactionId, Int32 output, String scriptPubKey, String redeemScript)
        {
            Inputs.Add(new SignRawTransactionInput
                {
                    TransactionId = transactionId,
                    Output = output,
                    ScriptPubKey = scriptPubKey,
                    RedeemScript = redeemScript
                });
        }

        public void AddKey(String privateKey)
        {
            PrivateKeys.Add(privateKey);
        }
    }
}
