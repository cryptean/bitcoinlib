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
        String GetAccount(String bitcoinAddress);
        String GetAccountAddress(String account);
        GetAddedNodeInfoResponse GetAddedNodeInfo(String dns, String node = null);
        List<String> GetAddressesByAccount(String account);
        Decimal GetBalance(String account = null, Int32 minConf = 1);
        String GetBestBlockHash();
        GetBlockResponse GetBlock(String hash, Boolean verbose = true);
        GetBlockchainInfoResponse GetBlockchainInfo();
        UInt32 GetBlockCount();
        String GetBlockHash(Int64 index);
        String GetBlockTemplate(params object[] parameters);
        Int32 GetConnectionCount();
        Double GetDifficulty();
        String GetGenerate();
        Int32 GetHashesPerSec();
        GetInfoResponse GetInfo();
        GetMiningInfoResponse GetMiningInfo();
        GetNetTotalsResponse GetNetTotals();
        UInt64 GetNetworkHashPs(UInt32 blocks = 120, Int64 height = -1);
        GetNetworkInfoResponse GetNetworkInfo();
        String GetNewAddress(String account = "");
        List<GetPeerInfoResponse> GetPeerInfo();
        String GetRawChangeAddress();
        GetRawMemPoolResponse GetRawMemPool(Boolean verbose = false);
        String GetRawTransaction(String txId, Int32 verbose = 0);
        String GetReceivedByAccount(String account, Int32 minConf = 1);
        String GetReceivedByAddress(String bitcoinAddress, Int32 minConf = 1);
        GetTransactionResponse GetTransaction(String txId);
        GetTransactionResponse GetTxOut(String txId, Int32 n, Boolean includeMemPool = true);
        GetTxOutSetInfoResponse GetTxOutSetInfo();
        Decimal GetUnconfirmedBalance();
        GetWalletInfoResponse GetWalletInfo();
        String GetWork(String data = null);
        String Help(String command = null);
        String ImportPrivKey(String privateKey, String label = null, Boolean rescan = true);
        void ImportWallet(String filename);
        String KeyPoolRefill(UInt32 newSize = 100);
        Dictionary<String, Decimal> ListAccounts(Int32 minConf = 1);
        List<List<ListAddressGroupingsResponse>> ListAddressGroupings();
        String ListLockUnspent();
        List<ListReceivedByAccountResponse> ListReceivedByAccount(Int32 minConf = 1, Boolean includeEmpty = false);
        List<ListReceivedByAddressResponse> ListReceivedByAddress(Int32 minConf = 1, Boolean includeEmpty = false);
        ListSinceBlockResponse ListSinceBlock(String blockHash = null, Int32 targetConfirmations = 0); //  Note: [target-confirmations] is set default to 1 so the optional parameters signature will not break, plus in most cases it won't affect the results as minConf (for source txs) is almost always initialized at the value of 1
        List<ListTransactionsResponse> ListTransactions(String account = null, Int32 count = 10, Int32 from = 0);
        List<ListUnspentResponse> ListUnspent(Int32 minConf = 1, Int32 maxConf = 9999999, List<String> addresses = null);
        //  todo: implement: lockunspent unlock [{"txid":"txid","vout":n},...]
        Boolean Move(String fromAccount, String toAccount, Decimal amount, Int32 minConf = 1, String comment = "");
        void Ping();
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
        String SubmitBlock(String hexData, params object[] parameters);
        ValidateAddressResponse ValidateAddress(String bitcoinAddress);
        Boolean VerifyChain(UInt16 checkLevel = 3, UInt32 numBlocks = 288); //  Note: numBlocks: 0 => ALL
        String VerifyMessage(String bitcoinAddress, String signature, String message);
        String WalletLock();
        String WalletPassphrase(String passphrase, Int32 timeoutInSeconds);
        String WalletPassphraseChange(String oldPassphrase, String newPassphrase);
    }
}