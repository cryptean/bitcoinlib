// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;
using BitcoinLib.Requests.AddNode;
using BitcoinLib.Requests.CreateRawTransaction;
using BitcoinLib.Requests.SignRawTransaction;
using BitcoinLib.Responses;

namespace BitcoinLib.Services.RpcServices.RpcService
{
    public interface IRpcService
    {
        String AddMultiSigAddress(Int32 nRquired, List<String> publicKeys, String account = null);
        String AddNode(String node, NodeAction action);
        String BackupWallet(String destination);
        CreateMultiSigResponse CreateMultiSig(Int32 nRquired, List<String> publicKeys);
        String CreateRawTransaction(CreateRawTransactionRequest rawTransaction);
        DecodeRawTransactionResponse DecodeRawTransaction(String rawTransactionHexString);
        DecodeScriptResponse DecodeScript(String hexString);
        String DumpPrivKey(String bitcoinAddress);
        void DumpWallet(String filename);
        String EncryptWallet(String passphrame);
        Decimal EstimateFee(UInt16 nBlocks);
        Decimal EstimatePriority(UInt16 nBlocks);
        String GetAccount(String bitcoinAddress);
        String GetAccountAddress(String account);
        GetAddedNodeInfoResponse GetAddedNodeInfo(String dns, String node = null);
        List<String> GetAddressesByAccount(String account);
        Decimal GetBalance(String account = null, Int32 minConf = 1, Boolean? includeWatchonly = false);
        String GetBestBlockHash();
        GetBlockResponse GetBlock(String hash, Boolean verbose = true);
        GetBlockchainInfoResponse GetBlockchainInfo();
        UInt32 GetBlockCount();
        String GetBlockHash(Int64 index);
        GetBlockTemplateResponse GetBlockTemplate(params object[] parameters);
        List<GetChainTipsResponse> GetChainTips();
        Int32 GetConnectionCount();
        Double GetDifficulty();
        String GetGenerate();
        Int32 GetHashesPerSec();
        GetInfoResponse GetInfo();
        GetMemPoolInfoResponse GetMemPoolInfo();
        GetMiningInfoResponse GetMiningInfo();
        GetNetTotalsResponse GetNetTotals();
        UInt64 GetNetworkHashPs(UInt32 blocks = 120, Int64 height = -1);
        GetNetworkInfoResponse GetNetworkInfo();
        String GetNewAddress(String account = "");
        List<GetPeerInfoResponse> GetPeerInfo();
        String GetRawChangeAddress();
        GetRawMemPoolResponse GetRawMemPool(Boolean verbose = false);
        GetRawTransactionResponse GetRawTransaction(String txId, Int32 verbose = 0);
        String GetReceivedByAccount(String account, Int32 minConf = 1);
        String GetReceivedByAddress(String bitcoinAddress, Int32 minConf = 1);
        GetTransactionResponse GetTransaction(String txId, Boolean? includeWatchonly = false);
        GetTransactionResponse GetTxOut(String txId, Int32 n, Boolean includeMemPool = true);
        GetTxOutSetInfoResponse GetTxOutSetInfo();
        Decimal GetUnconfirmedBalance();
        GetWalletInfoResponse GetWalletInfo();
        String Help(String command = null);
        void ImportAddress(String address, String label = null, Boolean rescan = true);
        String ImportPrivKey(String privateKey, String label = null, Boolean rescan = true);
        void ImportWallet(String filename);
        String KeyPoolRefill(UInt32 newSize = 100);
        Dictionary<String, Decimal> ListAccounts(Int32 minConf = 1, Boolean? includeWatchonly = false);
        List<List<ListAddressGroupingsResponse>> ListAddressGroupings();
        String ListLockUnspent();
        List<ListReceivedByAccountResponse> ListReceivedByAccount(Int32 minConf = 1, Boolean includeEmpty = false, Boolean? includeWatchonly = false);
        List<ListReceivedByAddressResponse> ListReceivedByAddress(Int32 minConf = 1, Boolean includeEmpty = false, Boolean? includeWatchonly = false);
        ListSinceBlockResponse ListSinceBlock(String blockHash = null, Int32 targetConfirmations = 1, Boolean? includeWatchonly = false);
        List<ListTransactionsResponse> ListTransactions(String account = null, Int32 count = 10, Int32 from = 0, Boolean? includeWatchonly = false);
        List<ListUnspentResponse> ListUnspent(Int32 minConf = 1, Int32 maxConf = 9999999, List<String> addresses = null);
        Boolean LockUnspent(Boolean unlock, IList<ListUnspentResponse> listUnspentResponses);
        Boolean Move(String fromAccount, String toAccount, Decimal amount, Int32 minConf = 1, String comment = "");
        void Ping();
        Boolean PrioritiseTransaction(String txId, Decimal priorityDelta, Decimal feeDelta);
        String SendFrom(String fromAccount, String toBitcoinAddress, Decimal amount, Int32 minConf = 1, String comment = null, String commentTo = null);
        String SendMany(String fromAccount, Dictionary<String, Decimal> toBitcoinAddress, Int32 minConf = 1, String comment = null);
        String SendRawTransaction(String rawTransactionHexString, Boolean? allowHighFees = false);
        String SendToAddress(String bitcoinAddress, Decimal amount, String comment = null, String commentTo = null);
        String SetAccount(String bitcoinAddress, String account);
        String SetGenerate(Boolean generate, Int16 generatingProcessorsLimit);
        String SetTxFee(Decimal amount);
        String SignMessage(String bitcoinAddress, String message);
        SignRawTransactionResponse SignRawTransaction(SignRawTransactionRequest signRawTransactionRequest);
        String Stop();
        String SubmitBlock(String hexData, params Object[] parameters);
        ValidateAddressResponse ValidateAddress(String bitcoinAddress);
        Boolean VerifyChain(UInt16 checkLevel = 3, UInt32 numBlocks = 288); //  Note: numBlocks: 0 => ALL
        String VerifyMessage(String bitcoinAddress, String signature, String message);
        String WalletLock();
        String WalletPassphrase(String passphrase, Int32 timeoutInSeconds);
        String WalletPassphraseChange(String oldPassphrase, String newPassphrase);
    }
}