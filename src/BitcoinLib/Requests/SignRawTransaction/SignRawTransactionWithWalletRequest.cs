// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System.Collections.Generic;

namespace BitcoinLib.Requests.SignRawTransaction
{
    public class SignRawTransactionWithWalletRequest
    {
        public SignRawTransactionWithWalletRequest(string rawTransactionHex, string sigHashType = "ALL")
        {
            RawTransactionHex = rawTransactionHex;
            Inputs = new List<SignRawTransactionWithWalletInput>();
            SigHashType = sigHashType;
        }

        public string RawTransactionHex { get; set; }
        public List<SignRawTransactionWithWalletInput> Inputs { get; set; }
        public string SigHashType { get; set; }

        public void AddInput(string txId, int vout, string scriptPubKey, string redeemScript)
        {
            Inputs.Add(new SignRawTransactionWithWalletInput
            {
                TxId = txId,
                Vout = vout,
                ScriptPubKey = scriptPubKey,
                RedeemScript = redeemScript
            });
        }

        public void AddInput(SignRawTransactionWithWalletInput input)
        {
            Inputs.Add(input);
        }
    }
}