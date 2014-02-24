// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;
using BitcoinLib.Requests.AddNode;
using BitcoinLib.Requests.CreateRawTransaction;
using BitcoinLib.Requests.SignRawTransaction;
using BitcoinLib.Responses;

namespace BitcoinLib.Services
{
    public interface IRpcService
    {
        String AddMultiSigAddress(Int32 nRquired, List<String> publicKeys, String account = null);                                                          //  <nrequired> <'["key","key"]'> [account]
        String AddNode(String node, NodeAction action);                                                                                                     //  <node> <add|remove|onetry>
        String BackupWallet(String destination);                                                                                                            //  <destination>
        CreateMultiSigResponse CreateMultiSig(Int32 nRquired, List<String> publicKeys);                                                                     //  <nrequired> <'["key","key"]'>
        String CreateRawTransaction(CreateRawTransactionRequest rawTransaction);                                                                            //  [{"txid":txid,"vout":n},...] {address:amount,...}
        DecodeRawTransactionResponse DecodeRawTransaction(String rawTransactionHexString);                                                                  //  <hex string>
        String DumpPrivKey(String bitcoinAddress);                                                                                                          //  <bitcoinaddress>
        String EncryptWallet(String passphrame);                                                                                                            //  <passphrase>                        //  Note: a new backup is needed after encrypting the wallet!
        String GetAccount(String bitcoinAddress);                                                                                                           //  <bitcoinaddress>
        String GetAccountAddress(String account);                                                                                                           //  <account>
        GetAddedNodeInfoResponse GetAddedNodeInfo(String dns, String node = null);                                                                          //  <dns> [node]
        List<String> GetAddressesByAccount(String account);                                                                                                 //  <account>
        Decimal GetBalance(String account = null, Int32 minConf = 1);                                                                                       //  [account] [minconf=1]
        GetBlockResponse GetBlock(String hash);                                                                                                             //  <hash>
        Int32 GetBlockCount();                                                                                                                              //  -
        String GetBlockHash(Int32 index);                                                                                                                   //  <index>
        String GetBlockTemplate(params object[] parameters);                                                                                                //  [params]
        Int32 GetConnectionCount();                                                                                                                         //  -
        Double GetDifficulty();                                                                                                                             //  -
        String GetGenerate();                                                                                                                               //  -
        Int32 GetHashesPerSec();                                                                                                                            //  -
        GetInfoResponse GetInfo();                                                                                                                          //  -
        GetMiningInfoResponse GetMiningInfo();                                                                                                              //  -
        String GetNewAddress(String account = null);                                                                                                        //  [account]
        List<GetPeerInfoResponse> GetPeerInfo();                                                                                                            //  -
        String GetRawMemPool();                                                                                                                             //  -
        String GetRawTransaction(String txId, Int32 verbose = 0);                                                                                           //  <txid> [verbose=0]
        String GetReceivedByAccount(String account, Int32 minConf = 1);                                                                                     //  <account> [minconf=1]
        String GetReceivedByAddress(String bitcoinAddress, Int32 minConf = 1);                                                                              //  <bitcoinaddress> [minconf=1]
        GetTransactionResponse GetTransaction(String txId);                                                                                                 //  <txid>                                  //  Note: For local wallet transactions only. To find "all transactions", use GetRawTransaction and decode with DecodeRawTransaction
        GetTransactionResponse GetTxOut(String txId, Int32 n, Boolean includeMemPool = true);                                                               //  <txid> <n> [includemempool=true]
        GetTxOutSetInfoResponse GetTxOutSetInfo();                                                                                                          //  -
        String GetWork(String data = null);                                                                                                                 //  [data]
        String Help(String command = null);                                                                                                                 //  [command]
        String ImportPrivKey(String privateKey, String label = null, Boolean rescan = true);                                                                //  [label] [rescan=true]
        String KeyPoolRefill();                                                                                                                             //  -
        Dictionary<String, Decimal> ListAccounts(Int32 minConf = 1);                                                                                        //  [minconf=1]
        List<List<ListAddressGroupingsResponse>> ListAddressGroupings();                                                                                    //  -
        String ListLockUnspent();                                                                                                                           //  -
        List<ListReceivedByAccountResponse> ListReceivedByAccount(Int32 minConf = 1, Boolean includeEmpty = false);                                         //  [minconf=1] [includeempty=false]
        List<ListReceivedByAddressResponse> ListReceivedByAddress(Int32 minConf = 1, Boolean includeEmpty = false);                                         //  [minconf=1] [includeempty=false]
        ListSinceBlockResponse ListSinceBlock(String blockHash = null, Int32 targetConfirmations = 0);                                                      //  [blockhash] [target-confirmations]      //  Note: [target-confirmations] is being defaulted at 1 here so the optional parameters signature will not break, plus in most cases it won't affect the results as minConf (for source txs) is almost always initialized at the value of 1
        List<ListTransactionsResponse> ListTransactions(String account = null, Int32 count = 10, Int32 from = 0);                                           //  [account] [count=10] [from=0]
        List<ListUnspentResponse> ListUnspent(Int32 minConf = 1, Int32 maxConf = 9999999, List<String> addreses = null);                                    //  [minconf=1] [maxconf=9999999] ["address",...]                                                                                       //  [minconf=1] [maxconf=9999999] ["address",...]
        //  todo: implement: lockunspent unlock? [array-of-Objects]                                                                                         //  unlock? [array-of-Objects]
        String Move(String fromAccount, String toAccount, Decimal amount, Int32 minConf = 1, String comment = null);                                        //  <fromaccount> <toaccount> <amount> [minconf=1] [comment]
        String SendFrom(String fromAccount, String toBitcoinAddress, Decimal amount, Int32 minConf = 1, String comment = null, String commentTo = null);    //  <fromaccount> <tobitcoinaddress> <amount> [minconf=1] [comment] [comment-to]
        String SendMany(String fromAccount, Dictionary<String, Decimal> toBitcoinAddress, Int32 minConf = 1, String comment = null);                        //  <fromaccount> {address:amount,...} [minconf=1] [comment]                                                                               //  <fromaccount> {address:amount,...} [minconf=1] [comment]
        String SendRawTransaction(String rawTransactionHexString);                                                                                          //  <hex string>
        String SendToAddress(String bitcoinAddress, Decimal amount, String comment = null, String commentTo = null);                                        //  <bitcoinaddress> <amount> [comment] [comment-to]
        String SetAccount(String bitcoinAddress, String account);                                                                                           //  <bitcoinaddress> <account>                                                                                                           
        String SetGenerate(Boolean generate, Int16 generatingProcessorsLimit);                                                                              //  <generate> [genproclimit]   
        String SetTxFee(Decimal amount);                                                                                                                    //  <amount>
        String SignMessage(String bitcoinAddress, String message);                                                                                          //  <bitcoinaddress> <message>
        SignRawTransactionResponse SignRawTransaction(SignRawTransactionRequest signRawTransactionRequest);                                                 //  signrawtransaction <hex string> [{"txid":txid,"vout":n,"scriptPubKey":hex,"redeemScript":hex},...] [<privatekey1>,...] [sighashtype="ALL"]
        String Stop();                                                                                                                                      //  -
        String SubmitBlock(String hexData, params object[] parameters);                                                                                     //  <hex data> [optional-params-obj]
        ValidateAddressResponse ValidateAddress(String bitcoinAddress);                                                                                     //  <bitcoinaddress>
        String VerifyMessage(String bitcoinAddress, String signature, String message);                                                                      //  <bitcoinaddress> <signature> <message>
        String WalletLock();                                                                                                                                //  -
        String WalletPassphrase(String passphrase, Int32 timeoutInSeconds);                                                                                 //  <passphrase> <timeout>
        String WalletPassphraseChange(String oldPassphrase, String newPassphrase);                                                                          //  <oldpassphrase> <newpassphrase>
    }
}