// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using System.Collections.Generic;
using System.Linq;
using BitcoinLib.RPC;
using BitcoinLib.RPC.Connector;
using BitcoinLib.Requests.AddNode;
using BitcoinLib.Requests.CreateRawTransaction;
using BitcoinLib.Requests.SignRawTransaction;
using BitcoinLib.Responses;
using BitcoinLib.Services.Coins.Base;
using Newtonsoft.Json.Linq;

namespace BitcoinLib.Services
{
    //   Implementation of API calls list, as found at: https://en.bitcoin.it/wiki/Original_Bitcoin_client/API_Calls_list (note: this list is often out-of-date so call "help" in your bitcoin-cli to get the latest signatures)
    public partial class CoinService : ICoinService
    {
        private readonly IRpcConnector _rpcConnector;

        public CoinService()
        {
            _rpcConnector = new RpcConnector(this);
            Parameters = new CoinParameters(this);
        }

        public CoinService(Boolean useTestnet) : this()
        {
            Parameters.UseTestnet = useTestnet;
        }

        public CoinService(String daemonUrl, String rpcUsername, String rpcPassword, String walletPassword = null) : this()
        {
            Parameters.DaemonUrl = daemonUrl;
            Parameters.UseTestnet = false; //  this will force the CoinParameters.SelectedDaemonUrl dynamic property to automatically pick the daemonUrl defined above
            Parameters.RpcUsername = rpcUsername;
            Parameters.RpcPassword = rpcPassword;

            if (!String.IsNullOrWhiteSpace(walletPassword))
            {
                Parameters.WalletPassword = walletPassword;
            }
        }

        public String AddMultiSigAddress(Int32 nRquired, List<String> publicKeys, String account)
        {
            return account != null
                       ? _rpcConnector.MakeRequest<String>(RpcMethods.addmultisigaddress, nRquired, publicKeys, account)
                       : _rpcConnector.MakeRequest<String>(RpcMethods.addmultisigaddress, nRquired, publicKeys);
        }

        public String AddNode(String node, NodeAction action)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.addnode, node, action.ToString());
        }

        public String BackupWallet(String destination)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.backupwallet, destination);
        }

        public CreateMultiSigResponse CreateMultiSig(Int32 nRquired, List<String> publicKeys)
        {
            return _rpcConnector.MakeRequest<CreateMultiSigResponse>(RpcMethods.createmultisig, nRquired, publicKeys);
        }

        public String CreateRawTransaction(CreateRawTransactionRequest rawTransaction)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.createrawtransaction, rawTransaction.Inputs, rawTransaction.Outputs);
        }

        public DecodeRawTransactionResponse DecodeRawTransaction(String rawTransactionHexString)
        {
            return _rpcConnector.MakeRequest<DecodeRawTransactionResponse>(RpcMethods.decoderawtransaction, rawTransactionHexString);
        }

        public DecodeScriptResponse DecodeScript(String hexString)
        {
            return _rpcConnector.MakeRequest<DecodeScriptResponse>(RpcMethods.decodescript, hexString);
        }

        public String DumpPrivKey(String bitcoinAddress)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.dumpprivkey, bitcoinAddress);
        }

        public void DumpWallet(String filename)
        {
            _rpcConnector.MakeRequest<String>(RpcMethods.dumpwallet, filename);
        }

        public String EncryptWallet(String passphrase)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.encryptwallet, passphrase);
        }

        public String GetAccount(String bitcoinAddress)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.getaccount, bitcoinAddress);
        }

        public String GetAccountAddress(String account)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.getaccountaddress, account);
        }

        public GetAddedNodeInfoResponse GetAddedNodeInfo(String dns, String node)
        {
            return String.IsNullOrWhiteSpace(node)
                       ? _rpcConnector.MakeRequest<GetAddedNodeInfoResponse>(RpcMethods.getaddednodeinfo, dns)
                       : _rpcConnector.MakeRequest<GetAddedNodeInfoResponse>(RpcMethods.getaddednodeinfo, dns, node);
        }

        public List<String> GetAddressesByAccount(String account)
        {
            return _rpcConnector.MakeRequest<List<String>>(RpcMethods.getaddressesbyaccount, account);
        }

        public Decimal GetBalance(String account, Int32 minConf)
        {
            return String.IsNullOrWhiteSpace(account)
                       ? _rpcConnector.MakeRequest<Decimal>(RpcMethods.getbalance)
                       : _rpcConnector.MakeRequest<Decimal>(RpcMethods.getbalance, account, minConf);
        }

        public String GetBestBlockHash()
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.getbestblockhash);
        }

        public GetBlockResponse GetBlock(String hash, Boolean verbose)
        {
            return _rpcConnector.MakeRequest<GetBlockResponse>(RpcMethods.getblock, hash, verbose);
        }

        public GetBlockchainInfoResponse GetBlockchainInfo()
        {
            return _rpcConnector.MakeRequest<GetBlockchainInfoResponse>(RpcMethods.getblockchaininfo);
        }

        public UInt32 GetBlockCount()
        {
            return _rpcConnector.MakeRequest<UInt32>(RpcMethods.getblockcount);
        }

        public String GetBlockHash(Int64 index)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.getblockhash, index);
        }

        public String GetBlockTemplate(params object[] parameters)
        {
            return parameters == null
                       ? _rpcConnector.MakeRequest<String>(RpcMethods.getblocktemplate)
                       : _rpcConnector.MakeRequest<String>(RpcMethods.getblocktemplate, parameters);
        }

        public Int32 GetConnectionCount()
        {
            return _rpcConnector.MakeRequest<Int32>(RpcMethods.getconnectioncount);
        }

        public Double GetDifficulty()
        {
            return _rpcConnector.MakeRequest<Double>(RpcMethods.getdifficulty);
        }

        public String GetGenerate()
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.getgenerate);
        }

        public Int32 GetHashesPerSec()
        {
            return _rpcConnector.MakeRequest<Int32>(RpcMethods.gethashespersec);
        }

        [Obsolete("Please use calls: GetWalletInfo(), GetBlockchainInfo() and GetNetworkInfo() instead")]
        public GetInfoResponse GetInfo()
        {
            return _rpcConnector.MakeRequest<GetInfoResponse>(RpcMethods.getinfo);
        }

        public GetMiningInfoResponse GetMiningInfo()
        {
            return _rpcConnector.MakeRequest<GetMiningInfoResponse>(RpcMethods.getmininginfo);
        }

        public GetNetTotalsResponse GetNetTotals()
        {
            return _rpcConnector.MakeRequest<GetNetTotalsResponse>(RpcMethods.getnettotals);
        }

        public UInt64 GetNetworkHashPs(UInt32 blocks, Int64 height)
        {
            return _rpcConnector.MakeRequest<UInt64>(RpcMethods.getnetworkhashps);
        }

        public GetNetworkInfoResponse GetNetworkInfo()
        {
            return _rpcConnector.MakeRequest<GetNetworkInfoResponse>(RpcMethods.getnetworkinfo);
        }

        public String GetNewAddress(String account)
        {
            return String.IsNullOrWhiteSpace(account)
                       ? _rpcConnector.MakeRequest<String>(RpcMethods.getnewaddress)
                       : _rpcConnector.MakeRequest<String>(RpcMethods.getnewaddress, account);
        }

        public List<GetPeerInfoResponse> GetPeerInfo()
        {
            return _rpcConnector.MakeRequest<List<GetPeerInfoResponse>>(RpcMethods.getpeerinfo);
        }

        public GetRawMemPoolResponse GetRawMemPool(Boolean verbose)
        {
            GetRawMemPoolResponse getRawMemPoolResponse = new GetRawMemPoolResponse
                {
                    IsVerbose = verbose
                };

            object rpcResponse = _rpcConnector.MakeRequest<object>(RpcMethods.getrawmempool, verbose);

            if (!verbose)
            {
                JArray rpcResponseAsArray = (JArray) rpcResponse;

                foreach (String txId in rpcResponseAsArray)
                {
                    getRawMemPoolResponse.TxIds.Add(txId);
                }

                return getRawMemPoolResponse;
            }

            IList<KeyValuePair<String, JToken>> rpcResponseAsKvp = (new EnumerableQuery<KeyValuePair<String, JToken>>(((JObject) (rpcResponse)))).ToList();
            IList<JToken> children = JObject.Parse(rpcResponse.ToString()).Children().ToList();

            for (Int32 i = 0; i < children.Count(); i++)
            {
                GetRawMemPoolVerboseResponse getRawMemPoolVerboseResponse = new GetRawMemPoolVerboseResponse
                    {
                        TxId = rpcResponseAsKvp[i].Key
                    };

                getRawMemPoolResponse.TxIds.Add(getRawMemPoolVerboseResponse.TxId);

                foreach (JProperty property in children[i].SelectMany(grandChild => grandChild.OfType<JProperty>()))
                {
                    switch (property.Name)
                    {
                        case "currentpriority":

                            Double currentPriority;

                            if (Double.TryParse(property.Value.ToString(), out currentPriority))
                            {
                                getRawMemPoolVerboseResponse.CurrentPriority = currentPriority;
                            }

                            break;

                        case "depends":

                            foreach (JToken jToken in property.Value)
                            {
                                getRawMemPoolVerboseResponse.Depends.Add(jToken.Value<String>());
                            }

                            break;

                        case "fee":

                            Decimal fee;

                            if (Decimal.TryParse(property.Value.ToString(), out fee))
                            {
                                getRawMemPoolVerboseResponse.Fee = fee;
                            }

                            break;

                        case "height":

                            Int32 height;

                            if (Int32.TryParse(property.Value.ToString(), out height))
                            {
                                getRawMemPoolVerboseResponse.Height = height;
                            }

                            break;

                        case "size":

                            Int32 size;

                            if (Int32.TryParse(property.Value.ToString(), out size))
                            {
                                getRawMemPoolVerboseResponse.Size = size;
                            }

                            break;

                        case "startingpriority":

                            Double startingPriority;

                            if (Double.TryParse(property.Value.ToString(), out startingPriority))
                            {
                                getRawMemPoolVerboseResponse.StartingPriority = startingPriority;
                            }

                            break;

                        case "time":

                            Int32 time;

                            if (Int32.TryParse(property.Value.ToString(), out time))
                            {
                                getRawMemPoolVerboseResponse.Time = time;
                            }

                            break;

                        default:

                            throw new Exception("Unkown property: " + property.Name + " in GetRawMemPool()");
                    }
                }
                getRawMemPoolResponse.VerboseResponses.Add(getRawMemPoolVerboseResponse);
            }
            return getRawMemPoolResponse;
        }

        public String GetRawChangeAddress()
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.getrawchangeaddress);
        }

        public String GetRawTransaction(String txId, Int32 verbose)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.getrawtransaction, txId, verbose);
        }

        public String GetReceivedByAccount(String account, Int32 minConf)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.getreceivedbyaccount, account, minConf);
        }

        public String GetReceivedByAddress(String bitcoinAddress, Int32 minConf)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.getreceivedbyaddress, bitcoinAddress, minConf);
        }

        public GetTransactionResponse GetTransaction(String txId)
        {
            return _rpcConnector.MakeRequest<GetTransactionResponse>(RpcMethods.gettransaction, txId);
        }

        public GetTransactionResponse GetTxOut(String txId, Int32 n, Boolean includeMemPool)
        {
            return _rpcConnector.MakeRequest<GetTransactionResponse>(RpcMethods.gettxout, txId, n, includeMemPool);
        }

        public GetTxOutSetInfoResponse GetTxOutSetInfo()
        {
            return _rpcConnector.MakeRequest<GetTxOutSetInfoResponse>(RpcMethods.gettxoutsetinfo);
        }

        public decimal GetUnconfirmedBalance()
        {
            return _rpcConnector.MakeRequest<Decimal>(RpcMethods.getunconfirmedbalance);
        }

        public GetWalletInfoResponse GetWalletInfo()
        {
            return _rpcConnector.MakeRequest<GetWalletInfoResponse>(RpcMethods.getwalletinfo);
        }

        public String GetWork(String data)
        {
            return String.IsNullOrWhiteSpace(data)
                       ? _rpcConnector.MakeRequest<String>(RpcMethods.getwork)
                       : _rpcConnector.MakeRequest<String>(RpcMethods.getwork, data);
        }

        public String Help(String command)
        {
            return String.IsNullOrWhiteSpace(command)
                       ? _rpcConnector.MakeRequest<String>(RpcMethods.help)
                       : _rpcConnector.MakeRequest<String>(RpcMethods.help, command);
        }

        public String ImportPrivKey(String privateKey, String label, Boolean rescan)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.importprivkey, privateKey, label, rescan);
        }

        public void ImportWallet(String filename)
        {
            _rpcConnector.MakeRequest<String>(RpcMethods.importwallet, filename);
        }

        public String KeyPoolRefill(UInt32 newSize)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.keypoolrefill, newSize);
        }

        public Dictionary<String, Decimal> ListAccounts(Int32 minConf)
        {
            return _rpcConnector.MakeRequest<Dictionary<String, Decimal>>(RpcMethods.listaccounts, minConf);
        }

        public List<List<ListAddressGroupingsResponse>> ListAddressGroupings()
        {
            List<List<List<object>>> unstructuredResponse = _rpcConnector.MakeRequest<List<List<List<object>>>>(RpcMethods.listaddressgroupings);
            List<List<ListAddressGroupingsResponse>> structuredResponse = new List<List<ListAddressGroupingsResponse>>(unstructuredResponse.Count);

            for (Int32 i = 0; i < unstructuredResponse.Count; i++)
            {
                for (Int32 j = 0; j < unstructuredResponse[i].Count; j++)
                {
                    if (unstructuredResponse[i][j].Count > 1)
                    {
                        ListAddressGroupingsResponse response = new ListAddressGroupingsResponse
                            {
                                Address = unstructuredResponse[i][j][0].ToString()
                            };

                        Decimal balance;
                        Decimal.TryParse(unstructuredResponse[i][j][1].ToString(), out balance);

                        if (unstructuredResponse[i][j].Count > 2)
                        {
                            response.Account = unstructuredResponse[i][j][2].ToString();
                        }

                        if (structuredResponse.Count < i + 1)
                        {
                            structuredResponse.Add(new List<ListAddressGroupingsResponse>());
                        }

                        structuredResponse[i].Add(response);
                    }
                }
            }
            return structuredResponse;
        }

        public String ListLockUnspent()
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.listlockunspent);
        }

        public List<ListReceivedByAccountResponse> ListReceivedByAccount(Int32 minConf, Boolean includeEmpty)
        {
            return _rpcConnector.MakeRequest<List<ListReceivedByAccountResponse>>(RpcMethods.listreceivedbyaccount, minConf, includeEmpty);
        }

        public List<ListReceivedByAddressResponse> ListReceivedByAddress(Int32 minConf, Boolean includeEmpty)
        {
            return _rpcConnector.MakeRequest<List<ListReceivedByAddressResponse>>(RpcMethods.listreceivedbyaddress, minConf, includeEmpty);
        }

        public ListSinceBlockResponse ListSinceBlock(String blockHash, Int32 targetConfirmations)
        {
            return String.IsNullOrWhiteSpace(blockHash)
                       ? _rpcConnector.MakeRequest<ListSinceBlockResponse>(RpcMethods.listsinceblock)
                       : _rpcConnector.MakeRequest<ListSinceBlockResponse>(RpcMethods.listsinceblock, blockHash, targetConfirmations);
        }

        public List<ListTransactionsResponse> ListTransactions(String account, Int32 count, Int32 from)
        {
            return (String.IsNullOrWhiteSpace(account))
                       ? _rpcConnector.MakeRequest<List<ListTransactionsResponse>>(RpcMethods.listtransactions)
                       : _rpcConnector.MakeRequest<List<ListTransactionsResponse>>(RpcMethods.listtransactions, account, count, from);
        }

        public List<ListUnspentResponse> ListUnspent(Int32 minConf, Int32 maxConf, List<String> addresses)
        {
            return _rpcConnector.MakeRequest<List<ListUnspentResponse>>(RpcMethods.listunspent, minConf, maxConf, (addresses ?? new List<String>()));
        }

        public Boolean Move(String fromAccount, String toAccount, Decimal amount, Int32 minConf, String comment)
        {
            return _rpcConnector.MakeRequest<Boolean>(RpcMethods.move, fromAccount, toAccount, amount, minConf, comment);
        }

        public void Ping()
        {
            _rpcConnector.MakeRequest<String>(RpcMethods.ping);
        }

        public String SendFrom(String fromAccount, String toBitcoinAddress, Decimal amount, Int32 minConf, String comment, String commentTo)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.sendfrom, fromAccount, toBitcoinAddress, amount, minConf, comment, commentTo);
        }

        public String SendMany(String fromAccount, Dictionary<String, Decimal> toBitcoinAddress, Int32 minConf, String comment)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.sendmany, fromAccount, toBitcoinAddress, minConf, comment);
        }

        public String SendRawTransaction(String rawTransactionHexString, Boolean? allowHighFees)
        {
            return allowHighFees == null
                       ? _rpcConnector.MakeRequest<String>(RpcMethods.sendrawtransaction, rawTransactionHexString)
                       : _rpcConnector.MakeRequest<String>(RpcMethods.sendrawtransaction, rawTransactionHexString, allowHighFees);
        }

        public String SendToAddress(String bitcoinAddress, Decimal amount, String comment, String commentTo)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.sendtoaddress, bitcoinAddress, amount, comment, commentTo);
        }

        public string SetAccount(String bitcoinAddress, String account)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.setaccount, bitcoinAddress, account);
        }

        public String SetGenerate(Boolean generate, Int16 generatingProcessorsLimit)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.setgenerate, generate, generatingProcessorsLimit);
        }

        public String SetTxFee(Decimal amount)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.settxfee, amount);
        }

        public String SignMessage(String bitcoinAddress, String message)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.signmessage, bitcoinAddress, message);
        }

        public SignRawTransactionResponse SignRawTransaction(SignRawTransactionRequest request)
        {
            #region default values

            if (request.Inputs.Count == 0)
            {
                request.Inputs = null;
            }

            if (String.IsNullOrWhiteSpace(request.SigHashType))
            {
                request.SigHashType = SigHashType.All;
            }

            if (request.PrivateKeys.Count == 0)
            {
                request.PrivateKeys = null;
            }

            #endregion

            return _rpcConnector.MakeRequest<SignRawTransactionResponse>(RpcMethods.signrawtransaction, request.RawTransactionHex, request.Inputs, request.PrivateKeys, request.SigHashType);
        }

        public String Stop()
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.stop);
        }

        public String SubmitBlock(String hexData, params object[] parameters)
        {
            return parameters == null
                       ? _rpcConnector.MakeRequest<String>(RpcMethods.submitblock, hexData)
                       : _rpcConnector.MakeRequest<String>(RpcMethods.submitblock, hexData, parameters);
        }

        public ValidateAddressResponse ValidateAddress(String bitcoinAddress)
        {
            return _rpcConnector.MakeRequest<ValidateAddressResponse>(RpcMethods.validateaddress, bitcoinAddress);
        }

        public bool VerifyChain(UInt16 checkLevel, UInt32 numBlocks)
        {
            return _rpcConnector.MakeRequest<Boolean>(RpcMethods.verifychain, checkLevel, numBlocks);
        }

        public String VerifyMessage(String bitcoinAddress, String signature, String message)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.verifymessage, bitcoinAddress, signature, message);
        }

        public String WalletLock()
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.walletlock);
        }

        public String WalletPassphrase(String passphrase, Int32 timeoutInSeconds)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.walletpassphrase, passphrase, timeoutInSeconds);
        }

        public String WalletPassphraseChange(String oldPassphrase, String newPassphrase)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.walletpassphrasechange, oldPassphrase, newPassphrase);
        }

        public override String ToString()
        {
            return Parameters.CoinLongName;
        }
    }
}