// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;

namespace BitcoinLib.Requests.SignRawTransaction
{
    public class SignRawTransactionRequest
    {
        public SignRawTransactionRequest(String rawTransactionHex)
        {
            RawTransactionHex = rawTransactionHex;
            Inputs = new List<SignRawTransactionInput>();
            PrivateKeys = new List<String>();
            SigHashType = SignRawTransaction.SigHashType.All;
        }

        public String RawTransactionHex { get; set; }
        public List<SignRawTransactionInput> Inputs { get; set; }
        public List<String> PrivateKeys { get; set; }
        public String SigHashType { get; set; }

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

        public void AddInput(SignRawTransactionInput signRawTransactionInput)
        {
            Inputs.Add(signRawTransactionInput);
        }

        public void AddKey(String privateKey)
        {
            PrivateKeys.Add(privateKey);
        }
    }
}