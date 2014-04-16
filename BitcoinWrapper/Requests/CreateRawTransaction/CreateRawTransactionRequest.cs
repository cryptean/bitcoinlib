// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;
using System.Linq;

namespace BitcoinLib.Requests.CreateRawTransaction
{
    public class CreateRawTransactionRequest
    {
        public CreateRawTransactionRequest()
        {
            Inputs = new List<CreateRawTransactionInput>();
            Outputs = new List<CreateRawTransactionOutput>();
        }

        public CreateRawTransactionRequest(IList<CreateRawTransactionInput> inputs, IList<CreateRawTransactionOutput> outputs)
        {
            Inputs = inputs;
            Outputs = outputs;
        }

        public CreateRawTransactionRequest(IList<CreateRawTransactionInput> inputs, Dictionary<String, Decimal> outputs) : this()
        {
            Inputs = inputs;

            foreach (KeyValuePair<String, Decimal> keyValuePair in outputs)
            {
                Outputs.Add(new CreateRawTransactionOutput
                    {
                        Address = keyValuePair.Key,
                        Amount = keyValuePair.Value
                    });
            }
        }

        public IList<CreateRawTransactionInput> Inputs { get; private set; }
        public IList<CreateRawTransactionOutput> Outputs { get; private set; }

        public void AddInput(CreateRawTransactionInput input)
        {
            Inputs.Add(input);
        }

        public void AddOutput(CreateRawTransactionOutput output)
        {
            Outputs.Add(output);
        }

        public void AddInput(String txId, Int32 vout)
        {
            Inputs.Add(new CreateRawTransactionInput
                {
                    TxId = txId,
                    Vout = vout
                });
        }

        public void AddOutput(String address, Decimal amount)
        {
            Outputs.Add(new CreateRawTransactionOutput
                {
                    Address = address,
                    Amount = amount
                });
        }

        public Boolean RemoveInput(CreateRawTransactionInput input)
        {
            return Inputs.Contains(input) && Inputs.Remove(input);
        }

        public Boolean RemoveOutput(CreateRawTransactionOutput output)
        {
            return Outputs.Contains(output) && Outputs.Remove(output);
        }

        public Boolean RemoveInput(String txId, Int32 vout)
        {
            CreateRawTransactionInput input = Inputs.FirstOrDefault(x => x.TxId == txId && x.Vout == vout);
            return input != null && Inputs.Remove(input);
        }

        public Boolean RemoveOutput(String address, Decimal amount)
        {
            CreateRawTransactionOutput output = Outputs.FirstOrDefault(x => x.Address == address && x.Amount == amount);
            return output != null && Outputs.Remove(output);
        }
    }
}