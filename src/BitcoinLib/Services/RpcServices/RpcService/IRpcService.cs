// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System.Collections.Generic;
using System.Threading.Tasks;
using BitcoinLib.Requests.AddNode;
using BitcoinLib.Requests.CreateRawTransaction;
using BitcoinLib.Requests.SignRawTransaction;
using BitcoinLib.Responses;

namespace BitcoinLib.Services.RpcServices.RpcService
{
    public interface IRpcService
    {
        #region Blockchain

        Task<string> GetBestBlockHash();
        Task<GetBlockResponse> GetBlock(string hash, bool verbose = true);
        Task<GetBlockchainInfoResponse> GetBlockchainInfo();
        Task<uint> GetBlockCount();
        Task<string> GetBlockHash(long index);
        //  getblockheader
        //  getchaintips
        Task<double> GetDifficulty();
        Task<List<GetChainTipsResponse>> GetChainTips();
        Task<GetMemPoolInfoResponse> GetMemPoolInfo();
        Task<GetRawMemPoolResponse> GetRawMemPool(bool verbose = false);
        Task<GetTransactionResponse> GetTxOut(string txId, int n, bool includeMemPool = true);
        //  gettxoutproof["txid",...] ( blockhash )
        Task<GetTxOutSetInfoResponse> GetTxOutSetInfo();
        Task<bool> VerifyChain(ushort checkLevel = 3, uint numBlocks = 288); //  Note: numBlocks: 0 => ALL

        #endregion

        #region Control

        Task<GetInfoResponse> GetInfo();
        Task<string> Help(string command = null);
        Task<string> Stop();

        #endregion

        #region Generating

        //  generate numblocks
        Task<bool> GetGenerate();
        Task<string> SetGenerate(bool generate, short generatingProcessorsLimit);

        #endregion

        #region Mining

        Task<GetBlockTemplateResponse> GetBlockTemplate(params object[] parameters);
        Task<GetMiningInfoResponse> GetMiningInfo();
        Task<ulong> GetNetworkHashPs(uint blocks = 120, long height = -1);
        Task<bool> PrioritiseTransaction(string txId, decimal priorityDelta, decimal feeDelta);
        Task<string> SubmitBlock(string hexData, params object[] parameters);

        #endregion

        #region Network

        Task AddNode(string node, NodeAction action);
        //  clearbanned
        //  disconnectnode
        Task<GetAddedNodeInfoResponse> GetAddedNodeInfo(string dns, string node = null);
        Task<int> GetConnectionCount();
        Task<GetNetTotalsResponse> GetNetTotals();
        Task<GetNetworkInfoResponse> GetNetworkInfo();
        Task<List<GetPeerInfoResponse>> GetPeerInfo();
        //  listbanned
        Task Ping();
        //  setban

        #endregion

        #region Rawtransactions

        Task<string> CreateRawTransaction(CreateRawTransactionRequest rawTransaction);
        Task<DecodeRawTransactionResponse> DecodeRawTransaction(string rawTransactionHexString);
        Task<DecodeScriptResponse> DecodeScript(string hexString);
        //  fundrawtransaction
        Task<GetRawTransactionResponse> GetRawTransaction(string txId, int verbose = 0);
        Task<string> SendRawTransaction(string rawTransactionHexString, bool? allowHighFees = false);
        Task<SignRawTransactionResponse> SignRawTransaction(SignRawTransactionRequest signRawTransactionRequest);

        #endregion

        #region Util

        Task<CreateMultiSigResponse> CreateMultiSig(int nRquired, List<string> publicKeys);
        Task<decimal> EstimateFee(ushort nBlocks);
        Task<decimal> EstimatePriority(ushort nBlocks);
        //  estimatesmartfee
        //  estimatesmartpriority
        Task<ValidateAddressResponse> ValidateAddress(string bitcoinAddress);
        Task<bool> VerifyMessage(string bitcoinAddress, string signature, string message);

        #endregion

        #region Wallet

        //  abandontransaction
        Task<string> AddMultiSigAddress(int nRquired, List<string> publicKeys, string account = null);
        Task BackupWallet(string destination);
        Task<string> DumpPrivKey(string bitcoinAddress);
        Task DumpWallet(string filename);
        Task<string> GetAccount(string bitcoinAddress);
        Task<string> GetAccountAddress(string account);
        Task<List<string>> GetAddressesByAccount(string account);
        Task<decimal> GetBalance(string account = null, int minConf = 1, bool? includeWatchonly = null);
        Task<string> GetNewAddress(string account = "");
        Task<string> GetRawChangeAddress();
        Task<decimal> GetReceivedByAccount(string account, int minConf = 1);
        Task<decimal> GetReceivedByAddress(string bitcoinAddress, int minConf = 1);
        Task<GetTransactionResponse> GetTransaction(string txId, bool? includeWatchonly = null);
        Task<decimal> GetUnconfirmedBalance();
        Task<GetWalletInfoResponse> GetWalletInfo();
        Task ImportAddress(string address, string label = null, bool rescan = true);
        Task<string> ImportPrivKey(string privateKey, string label = null, bool rescan = true);
        //  importpubkey
        Task ImportWallet(string filename);
        Task<string> KeyPoolRefill(uint newSize = 100);
        Task<Dictionary<string, decimal>> ListAccounts(int minConf = 1, bool? includeWatchonly = null);
        Task<List<List<ListAddressGroupingsResponse>>> ListAddressGroupings();
        Task<string> ListLockUnspent();
        Task<List<ListReceivedByAccountResponse>> ListReceivedByAccount(int minConf = 1, bool includeEmpty = false, bool? includeWatchonly = null);
        Task<List<ListReceivedByAddressResponse>> ListReceivedByAddress(int minConf = 1, bool includeEmpty = false, bool? includeWatchonly = null);
        Task<ListSinceBlockResponse> ListSinceBlock(string blockHash = null, int targetConfirmations = 1, bool? includeWatchonly = null);
        Task<List<ListTransactionsResponse>> ListTransactions(string account = null, int count = 10, int from = 0, bool? includeWatchonly = null);
        Task<List<ListUnspentResponse>> ListUnspent(int minConf = 1, int maxConf = 9999999, List<string> addresses = null);
        Task<bool> LockUnspent(bool unlock, IList<ListUnspentResponse> listUnspentResponses);
        Task<bool> Move(string fromAccount, string toAccount, decimal amount, int minConf = 1, string comment = "");
        Task<string> SendFrom(string fromAccount, string toBitcoinAddress, decimal amount, int minConf = 1, string comment = null, string commentTo = null);
        Task<string> SendMany(string fromAccount, Dictionary<string, decimal> toBitcoinAddress, int minConf = 1, string comment = null);
        Task<string> SendToAddress(string bitcoinAddress, decimal amount, string comment = null, string commentTo = null);
        Task<string> SetAccount(string bitcoinAddress, string account);
        Task<string> SetTxFee(decimal amount);
        Task<string> SignMessage(string bitcoinAddress, string message);
        Task<string> WalletLock();
        Task<string> WalletPassphrase(string passphrase, int timeoutInSeconds);
        Task<string> WalletPassphraseChange(string oldPassphrase, string newPassphrase);

        #endregion
    }
}