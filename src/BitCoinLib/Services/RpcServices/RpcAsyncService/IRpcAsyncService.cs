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
    public interface IRpcAsyncService
    {
        #region Blockchain

        Task<string> GetBestBlockHashAsync();
        Task<GetBlockResponse> GetBlockAsync(string hash, bool verbose = true);
        Task<GetBlockchainInfoResponse> GetBlockchainInfoAsync();
        Task<uint> GetBlockCountAsync();
        Task<string> GetBlockHashAsync(long index);
        //  getblockheader
        //  getchaintips
        Task<double> GetDifficultyAsync();
        Task<List<GetChainTipsResponse>> GetChainTipsAsync();
        Task<GetMemPoolInfoResponse> GetMemPoolInfoAsync();
        Task<GetRawMemPoolResponse> GetRawMemPoolAsync(bool verbose = false);
        Task<GetTransactionResponse> GetTxOutAsync(string txId, int n, bool includeMemPool = true);
        //  gettxoutproof["txid",...] ( blockhash )
        Task<GetTxOutSetInfoResponse> GetTxOutSetInfoAsync();
        Task<bool> VerifyChainAsync(ushort checkLevel = 3, uint numBlocks = 288); //  Note: numBlocks: 0 => ALL

        #endregion

        #region Control

        Task<GetInfoResponse> GetInfoAsync();
        Task<string> HelpAsync(string command = null);
        Task<string> StopAsync();

        #endregion

        #region Generating

        //  generate numblocks
        Task<bool> GetGenerateAsync();
        Task<string> SetGenerateAsync(bool generate, short generatingProcessorsLimit);

        #endregion

        #region Mining

        Task<GetBlockTemplateResponse> GetBlockTemplateAsync(params object[] parameters);
        Task<GetMiningInfoResponse> GetMiningInfoAsync();
        Task<ulong> GetNetworkHashPsAsync(uint blocks = 120, long height = -1);
        Task<bool> PrioritiseTransactionAsync(string txId, decimal priorityDelta, decimal feeDelta);
        Task<string> SubmitBlockAsync(string hexData, params object[] parameters);

        #endregion

        #region Network

        Task AddNodeAsync(string node, NodeAction action);
        //  clearbanned
        //  disconnectnode
        Task<GetAddedNodeInfoResponse> GetAddedNodeInfoAsync(string dns, string node = null);
        Task<int> GetConnectionCountAsync();
        Task<GetNetTotalsResponse> GetNetTotalsAsync();
        Task<GetNetworkInfoResponse> GetNetworkInfoAsync();
        Task<List<GetPeerInfoResponse>> GetPeerInfoAsync();
        //  listbanned
        Task PingAsync();
        //  setban

        #endregion

        #region Rawtransactions

        Task<string> CreateRawTransactionAsync(CreateRawTransactionRequest rawTransaction);
        Task<DecodeRawTransactionResponse> DecodeRawTransactionAsync(string rawTransactionHexString);
        Task<DecodeScriptResponse> DecodeScriptAsync(string hexString);
        //  fundrawtransaction
        Task<GetRawTransactionResponse> GetRawTransactionAsync(string txId, int verbose = 0);
        Task<string> SendRawTransactionAsync(string rawTransactionHexString, bool? allowHighFees = false);
        Task<SignRawTransactionResponse> SignRawTransactionAsync(SignRawTransactionRequest signRawTransactionRequest);

        #endregion

        #region Util

        Task<CreateMultiSigResponse> CreateMultiSigAsync(int nRquired, List<string> publicKeys);
        Task<decimal> EstimateFeeAsync(ushort nBlocks);
        Task<decimal> EstimatePriorityAsync(ushort nBlocks);
        //  estimatesmartfee
        //  estimatesmartpriority
        Task<ValidateAddressResponse> ValidateAddressAsync(string bitcoinAddress);
        Task<bool> VerifyMessageAsync(string bitcoinAddress, string signature, string message);

        #endregion

        #region Wallet

        //  abandontransaction
        Task<string> AddMultiSigAddressAsync(int nRquired, List<string> publicKeys, string account = null);
        Task BackupWalletAsync(string destination);
        Task<string> DumpPrivKeyAsync(string bitcoinAddress);
        Task DumpWalletAsync(string filename);
        Task<string> GetAccountAsync(string bitcoinAddress);
        Task<string> GetAccountAddressAsync(string account);
        Task<List<string>> GetAddressesByAccountAsync(string account);
        Task<decimal> GetBalanceAsync(string account = null, int minConf = 1, bool? includeWatchonly = null);
        Task<string> GetNewAddressAsync(string account = "");
        Task<string> GetRawChangeAddressAsync();
        Task<decimal> GetReceivedByAccountAsync(string account, int minConf = 1);
        Task<decimal> GetReceivedByAddressAsync(string bitcoinAddress, int minConf = 1);
        Task<GetTransactionResponse> GetTransactionAsync(string txId, bool? includeWatchonly = null);
        Task<decimal> GetUnconfirmedBalanceAsync();
        Task<GetWalletInfoResponse> GetWalletInfoAsync();
        Task ImportAddressAsync(string address, string label = null, bool rescan = true);
        Task<string> ImportPrivKeyAsync(string privateKey, string label = null, bool rescan = true);
        //  importpubkey
        Task ImportWalletAsync(string filename);
        Task<string> KeyPoolRefillAsync(uint newSize = 100);
        Task<Dictionary<string, decimal>> ListAccountsAsync(int minConf = 1, bool? includeWatchonly = null);
        Task<List<List<ListAddressGroupingsResponse>>> ListAddressGroupingsAsync();
        Task<string> ListLockUnspentAsync();
        Task<List<ListReceivedByAccountResponse>> ListReceivedByAccountAsync(int minConf = 1, bool includeEmpty = false, bool? includeWatchonly = null);
        Task<List<ListReceivedByAddressResponse>> ListReceivedByAddressAsync(int minConf = 1, bool includeEmpty = false, bool? includeWatchonly = null);
        Task<ListSinceBlockResponse> ListSinceBlockAsync(string blockHash = null, int targetConfirmations = 1, bool? includeWatchonly = null);
        Task<List<ListTransactionsResponse>> ListTransactionsAsync(string account = null, int count = 10, int from = 0, bool? includeWatchonly = null);
        Task<List<ListUnspentResponse>> ListUnspentAsync(int minConf = 1, int maxConf = 9999999, List<string> addresses = null);
        Task<bool> LockUnspentAsync(bool unlock, IList<ListUnspentResponse> listUnspentResponses);
        Task<bool> MoveAsync(string fromAccount, string toAccount, decimal amount, int minConf = 1, string comment = "");
        Task<string> SendFromAsync(string fromAccount, string toBitcoinAddress, decimal amount, int minConf = 1, string comment = null, string commentTo = null);
        Task<string> SendManyAsync(string fromAccount, Dictionary<string, decimal> toBitcoinAddress, int minConf = 1, string comment = null);
        Task<string> SendToAddressAsync(string bitcoinAddress, decimal amount, string comment = null, string commentTo = null);
        Task<string> SetAccountAsync(string bitcoinAddress, string account);
        Task<string> SetTxFeeAsync(decimal amount);
        Task<string> SignMessageAsync(string bitcoinAddress, string message);
        Task<string> WalletLockAsync();
        Task<string> WalletPassphraseAsync(string passphrase, int timeoutInSeconds);
        Task<string> WalletPassphraseChangeAsync(string oldPassphrase, string newPassphrase);

        #endregion

        #region Extender
        Task<decimal> GetAddressBalanceAsync(string inWalletAddress, int minConf = 0, bool validateAddressBeforeProcessing = true);
        Task<decimal> GetMinimumNonZeroTransactionFeeEstimateAsync(short numberOfInputs = 1, short numberOfOutputs = 1);
        Task<Dictionary<string, string>> GetMyPublicAndPrivateKeyPairsAsync();
        Task<DecodeRawTransactionResponse> GetPublicTransactionAsync(string txId);
        Task<decimal> GetTransactionFeeAsync(CreateRawTransactionRequest createRawTransactionRequest, bool checkIfTransactionQualifiesForFreeRelay = true, bool enforceMinimumTransactionFeePolicy = true);
        Task<decimal> GetTransactionPriorityAsync(CreateRawTransactionRequest createRawTransactionRequest);
        decimal GetTransactionPriorityAsync(IList<ListUnspentResponse> transactionInputs, int numberOfOutputs);
        Task<string> GetTransactionSenderAddressAsync(string txId);
        int GetTransactionSizeInBytesAsync(CreateRawTransactionRequest createRawTransactionRequest);
        int GetTransactionSizeInBytesAsync(int numberOfInputs, int numberOfOutputs);
        Task<GetRawTransactionResponse> GetRawTxFromImmutableTxIdAsync(string rigidTxId, int listTransactionsCount = int.MaxValue, int listTransactionsFrom = 0, bool getRawTransactionVersbose = true, bool rigidTxIdIsSha256 = false);
        Task<string> GetImmutableTxIdAsync(string txId, bool getSha256Hash = false);
        Task<bool> IsInWalletTransactionAsync(string txId);
        Task<bool> IsTransactionFreeAsync(CreateRawTransactionRequest createRawTransactionRequest);
        bool IsTransactionFreeAsync(IList<ListUnspentResponse> transactionInputs, int numberOfOutputs, decimal minimumAmountAmongOutputs);
        Task<bool> IsWalletEncryptedAsync(); 
        #endregion
    }
}