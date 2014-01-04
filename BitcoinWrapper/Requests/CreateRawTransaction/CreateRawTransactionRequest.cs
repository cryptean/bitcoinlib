using System;
using System.Collections.Generic;

namespace BitcoinWrapper.Requests.CreateRawTransaction
{
    public class CreateRawTransactionRequest
    {
        public List<CreateRawTransactionInput> Inputs { get; private set; }
        public Dictionary<String, Decimal> Outputs { get; private set; }
        
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
            Inputs.Add(new CreateRawTransactionInput { TransactionId = transactionId, Output = output });
        }

        public void AddOutput(String address, Decimal amount)
        {
            Outputs.Add(address, amount);
        }
    }
}
