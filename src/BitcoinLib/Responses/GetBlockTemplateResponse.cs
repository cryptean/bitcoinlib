// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement


namespace BitcoinLib.Responses
{
    public class GetBlockTemplateResponse
    {
        public int Version { get; set; }
        public string PreviousBlockHash { get; set; }
        public GetBlockTemplateTransaction[] Transactions { get; set; }
        public GetBlockTemplateCoinbaseAux CoinbaseAux { get; set; }
        public long CoinbaseValue { get; set; }
        public string Target { get; set; }
        public int MinTime { get; set; }
        public string[] Mutable { get; set; }
        public string NonceRange { get; set; }
        public int SigopLimit { get; set; }
        public int SizeLimit { get; set; }
        public uint CurTime { get; set; }
        public string Bits { get; set; }
        public int Height { get; set; }
    }

    public class GetBlockTemplateCoinbaseAux
    {
        public string Flags { get; set; }
    }

    public class GetBlockTemplateTransaction
    {
        public string Data { get; set; }
        public string Hash { get; set; }
        public string[] Depends { get; set; }
        public int Fee { get; set; }
        public int Sigops { get; set; }
    }
}