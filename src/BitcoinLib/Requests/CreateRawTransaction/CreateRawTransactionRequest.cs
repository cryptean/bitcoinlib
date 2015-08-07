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
            Outputs = new Dictionary<String, Decimal>();
        }

        public CreateRawTransactionRequest(IList<CreateRawTransactionInput> inputs, IDictionary<String, Decimal> outputs) : this()
        {
            Inputs = inputs;
            Outputs = outputs;
        }

        public IList<CreateRawTransactionInput> Inputs { get; private set; }
        public IDictionary<String, Decimal> Outputs { get; private set; }

        public void AddInput(CreateRawTransactionInput input)
        {
            Inputs.Add(input);
        }

        public void AddOutput(CreateRawTransactionOutput output)
        {
            Outputs.Add(output.Address, output.Amount);
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
            Outputs.Add(address, amount);
        }

        public Boolean RemoveInput(CreateRawTransactionInput input)
        {
            return Inputs.Contains(input) && Inputs.Remove(input);
        }

        public Boolean RemoveOutput(CreateRawTransactionOutput output)
        {
            return RemoveOutput(output.Address, output.Amount);
        }

        public Boolean RemoveInput(String txId, Int32 vout)
        {
            CreateRawTransactionInput input = Inputs.FirstOrDefault(x => x.TxId == txId && x.Vout == vout);
            return input != null && Inputs.Remove(input);
        }

        public Boolean RemoveOutput(String address, Decimal amount)
        {
            KeyValuePair<String, Decimal> outputToBeRemoved = new KeyValuePair<String, Decimal>(address, amount);
            return Outputs.Contains<KeyValuePair<String, Decimal>>(outputToBeRemoved) && Outputs.Remove(outputToBeRemoved);
        }
    }
}