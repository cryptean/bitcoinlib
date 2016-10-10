// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System.Collections.Generic;
using System.Linq;

namespace BitcoinLib.Requests.CreateRawTransaction
{
    public class CreateRawTransactionRequest
    {
        public CreateRawTransactionRequest()
        {
            Inputs = new List<CreateRawTransactionInput>();
            Outputs = new Dictionary<string, decimal>();
        }

        public CreateRawTransactionRequest(IList<CreateRawTransactionInput> inputs, IDictionary<string, decimal> outputs) : this()
        {
            Inputs = inputs;
            Outputs = outputs;
        }

        public IList<CreateRawTransactionInput> Inputs { get; }
        public IDictionary<string, decimal> Outputs { get; }

        public void AddInput(CreateRawTransactionInput input)
        {
            Inputs.Add(input);
        }

        public void AddOutput(CreateRawTransactionOutput output)
        {
            Outputs.Add(output.Address, output.Amount);
        }

        public void AddInput(string txId, int vout)
        {
            Inputs.Add(new CreateRawTransactionInput
            {
                TxId = txId,
                Vout = vout
            });
        }

        public void AddOutput(string address, decimal amount)
        {
            Outputs.Add(address, amount);
        }

        public bool RemoveInput(CreateRawTransactionInput input)
        {
            return Inputs.Contains(input) && Inputs.Remove(input);
        }

        public bool RemoveOutput(CreateRawTransactionOutput output)
        {
            return RemoveOutput(output.Address, output.Amount);
        }

        public bool RemoveInput(string txId, int vout)
        {
            var input = Inputs.FirstOrDefault(x => x.TxId == txId && x.Vout == vout);
            return input != null && Inputs.Remove(input);
        }

        public bool RemoveOutput(string address, decimal amount)
        {
            var outputToBeRemoved = new KeyValuePair<string, decimal>(address, amount);
            return Outputs.Contains<KeyValuePair<string, decimal>>(outputToBeRemoved) && Outputs.Remove(outputToBeRemoved);
        }
    }
}