// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;

namespace BitcoinLib.Requests.CreateRawTransaction
{
    public class CreateRawTransactionRequest
    {
        public CreateRawTransactionRequest()
        {
            Inputs = new List<CreateRawTransactionInput>();
            Outputs = new Dictionary<String, Decimal>();
        }

        public CreateRawTransactionRequest(List<CreateRawTransactionInput> inputs, Dictionary<String, Decimal> outputs)
        {
            Inputs = inputs;
            Outputs = outputs;
        }

        public List<CreateRawTransactionInput> Inputs { get; private set; }
        public Dictionary<String, Decimal> Outputs { get; private set; }

        public void AddInput(CreateRawTransactionInput input)
        {
            Inputs.Add(input);
        }

        public void AddOutput(CreateRawTransactionOutput output)
        {
            Outputs.Add(output.Address, output.Amount);
        }

        public void AddInput(String transactionId, Int32 output)
        {
            Inputs.Add(new CreateRawTransactionInput {TransactionId = transactionId, Output = output});
        }

        public void AddOutput(String address, Decimal amount)
        {
            Outputs.Add(address, amount);
        }
    }
}