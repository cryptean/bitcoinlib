// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System.Collections.Generic;
using BitcoinLib.Requests.AddNode;
using BitcoinLib.Requests.CreateRawTransaction;
using BitcoinLib.Requests.SignRawTransaction;
using BitcoinLib.Responses;

namespace BitcoinLib.Services.RpcServices.RpcService
{
    public interface IRpcService
    {
        #region Blockchain

        string GetBestBlockHash();
        GetBlockResponse GetBlock(string hash, bool verbose = true);
        GetBlockchainInfoResponse GetBlockchainInfo();
        uint GetBlockCount();
        string GetBlockHash(long index);
        //  getblockheader
        //  getchaintips
        double GetDifficulty();
        List<GetChainTipsResponse> GetChainTips();
        GetMemPoolInfoResponse GetMemPoolInfo();
        GetRawMemPoolResponse GetRawMemPool(bool verbose = false);
        GetTransactionResponse GetTxOut(string txId, int n, bool includeMemPool = true);
        //  gettxoutproof["txid",...] ( blockhash )
        GetTxOutSetInfoResponse GetTxOutSetInfo();
        bool VerifyChain(ushort checkLevel = 3, uint numBlocks = 288); //  Note: numBlocks: 0 => ALL

        #endregion

        #region Control

        GetInfoResponse GetInfo();
        string Help(string command = null);
        string Stop();

        #endregion

        #region Generating

        //  generate numblocks
        bool GetGenerate();
        string SetGenerate(bool generate, short generatingProcessorsLimit);

        #endregion

        #region Mining

        GetBlockTemplateResponse GetBlockTemplate(params object[] parameters);
        GetMiningInfoResponse GetMiningInfo();
        ulong GetNetworkHashPs(uint blocks = 120, long height = -1);
        bool PrioritiseTransaction(string txId, decimal priorityDelta, decimal feeDelta);
        string SubmitBlock(string hexData, params object[] parameters);

        #endregion

        #region Network

        void AddNode(string node, NodeAction action);
        //  clearbanned
        //  disconnectnode
        GetAddedNodeInfoResponse GetAddedNodeInfo(string dns, string node = null);
        int GetConnectionCount();
        GetNetTotalsResponse GetNetTotals();
        GetNetworkInfoResponse GetNetworkInfo();
        List<GetPeerInfoResponse> GetPeerInfo();
        //  listbanned
        void Ping();
        //  setban

        #endregion

        #region Rawtransactions

        string CreateRawTransaction(CreateRawTransactionRequest rawTransaction);
        DecodeRawTransactionResponse DecodeRawTransaction(string rawTransactionHexString);
        DecodeScriptResponse DecodeScript(string hexString);
        //  fundrawtransaction
        GetRawTransactionResponse GetRawTransaction(string txId, int verbose = 0);
        string SendRawTransaction(string rawTransactionHexString, bool? allowHighFees = false);
        SignRawTransactionResponse SignRawTransaction(SignRawTransactionRequest signRawTransactionRequest);

        #endregion

        #region Util

        CreateMultiSigResponse CreateMultiSig(int nRquired, List<string> publicKeys);
        decimal EstimateFee(ushort nBlocks);
        decimal EstimatePriority(ushort nBlocks);
        //  estimatesmartfee
        //  estimatesmartpriority
        ValidateAddressResponse ValidateAddress(string bitcoinAddress);
        bool VerifyMessage(string bitcoinAddress, string signature, string message);

        #endregion

        #region Wallet

        //  abandontransaction
        string AddMultiSigAddress(int nRquired, List<string> publicKeys, string account = null);
        string AddWitnessAddress(string address);
        void BackupWallet(string destination);
        string DumpPrivKey(string bitcoinAddress);
        void DumpWallet(string filename);
        string GetAccount(string bitcoinAddress);
        string GetAccountAddress(string account);
        List<string> GetAddressesByAccount(string account);
        decimal GetBalance(string account = null, int minConf = 1, bool? includeWatchonly = null);
        string GetNewAddress(string account = "");
        string GetRawChangeAddress();
        decimal GetReceivedByAccount(string account, int minConf = 1);
        decimal GetReceivedByAddress(string bitcoinAddress, int minConf = 1);
        GetTransactionResponse GetTransaction(string txId, bool? includeWatchonly = null);
        decimal GetUnconfirmedBalance();
        GetWalletInfoResponse GetWalletInfo();
        void ImportAddress(string address, string label = null, bool rescan = true);
        string ImportPrivKey(string privateKey, string label = null, bool rescan = true);
        //  importpubkey
        void ImportWallet(string filename);
        string KeyPoolRefill(uint newSize = 100);
        Dictionary<string, decimal> ListAccounts(int minConf = 1, bool? includeWatchonly = null);
        List<List<ListAddressGroupingsResponse>> ListAddressGroupings();
        string ListLockUnspent();
        List<ListReceivedByAccountResponse> ListReceivedByAccount(int minConf = 1, bool includeEmpty = false, bool? includeWatchonly = null);
        List<ListReceivedByAddressResponse> ListReceivedByAddress(int minConf = 1, bool includeEmpty = false, bool? includeWatchonly = null);
        ListSinceBlockResponse ListSinceBlock(string blockHash = null, int targetConfirmations = 1, bool? includeWatchonly = null);
        List<ListTransactionsResponse> ListTransactions(string account = null, int count = 10, int from = 0, bool? includeWatchonly = null);
        List<ListUnspentResponse> ListUnspent(int minConf = 1, int maxConf = 9999999, List<string> addresses = null);
        bool LockUnspent(bool unlock, IList<ListUnspentResponse> listUnspentResponses);
        bool Move(string fromAccount, string toAccount, decimal amount, int minConf = 1, string comment = "");
        string SendFrom(string fromAccount, string toBitcoinAddress, decimal amount, int minConf = 1, string comment = null, string commentTo = null);
        string SendMany(string fromAccount, Dictionary<string, decimal> toBitcoinAddress, int minConf = 1, string comment = null);
        string SendToAddress(string bitcoinAddress, decimal amount, string comment = null, string commentTo = null, bool subtractFeeFromAmount = false);
        string SetAccount(string bitcoinAddress, string account);
        string SetTxFee(decimal amount);
        string SignMessage(string bitcoinAddress, string message);
        string WalletLock();
        string WalletPassphrase(string passphrase, int timeoutInSeconds);
        string WalletPassphraseChange(string oldPassphrase, string newPassphrase);

        #endregion
    }
}