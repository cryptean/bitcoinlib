using System;
using System.Collections.Generic;

namespace BitcoinWrapper.Requests.SignRawTransaction
{
    //  todo: add SigHashType 
    //  todo: check consistency with ==>  signrawtransaction <hex string> [{"txid":txid,"vout":n,"scriptPubKey":hex,"redeemScript":hex},...] [<privatekey1>,...] [sighashtype="ALL"]      
    public class SignRawTransactionRequest
    {
        public SignRawTransactionRequest(String rawTransactionHex)
        {
            RawTransactionHex = rawTransactionHex;
            Inputs = new List<SignRawTransactionInput>();
            PrivateKeys = new List<String>();
        }

        public String RawTransactionHex { get; set; }
        public List<SignRawTransactionInput> Inputs { get; set; }
        public List<String> PrivateKeys { get; set; }
        public String SigHashType { get; set; }
        
        public void AddInput(String transactionId, Int32 output, String scriptPubKey)
        {
            Inputs.Add(new SignRawTransactionInput
                {
                    TransactionId = transactionId,
                    Output = output,
                    ScriptPubKey = scriptPubKey
                });
        }

        public void AddKey(String privateKey)
        {
            PrivateKeys.Add(privateKey);
        }
    }
}
