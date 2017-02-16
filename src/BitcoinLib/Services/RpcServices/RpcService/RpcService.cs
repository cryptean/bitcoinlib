// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitcoinLib.Requests.AddNode;
using BitcoinLib.Requests.CreateRawTransaction;
using BitcoinLib.Requests.SignRawTransaction;
using BitcoinLib.Responses;
using BitcoinLib.RPC.Connector;
using BitcoinLib.RPC.Specifications;
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
            Parameters = new CoinParameters(this, null, null, null, null, 0);
        }

        public CoinService(bool useTestnet) : this()
        {
            Parameters.UseTestnet = useTestnet;
        }

        public CoinService(string daemonUrl, string rpcUsername, string rpcPassword, string walletPassword)
        {
            _rpcConnector = new RpcConnector(this);
            Parameters = new CoinParameters(this, daemonUrl, rpcUsername, rpcPassword, walletPassword, 0);
        }

        //  this provides support for cases where *.config files are not an option
        public CoinService(string daemonUrl, string rpcUsername, string rpcPassword, string walletPassword, short rpcRequestTimeoutInSeconds)
        {
            _rpcConnector = new RpcConnector(this);
            Parameters = new CoinParameters(this, daemonUrl, rpcUsername, rpcPassword, walletPassword, rpcRequestTimeoutInSeconds);
        }

        public Task<string> AddMultiSigAddress(int nRquired, List<string> publicKeys, string account)
        {
            return account != null
                ? _rpcConnector.MakeRequestAsync<string>(RpcMethods.addmultisigaddress, nRquired, publicKeys, account)
                : _rpcConnector.MakeRequestAsync<string>(RpcMethods.addmultisigaddress, nRquired, publicKeys);
        }

        public Task AddNode(string node, NodeAction action)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.addnode, node, action.ToString());
        }

        public Task BackupWallet(string destination)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.backupwallet, destination);
        }

        public Task<CreateMultiSigResponse> CreateMultiSig(int nRquired, List<string> publicKeys)
        {
            return _rpcConnector.MakeRequestAsync<CreateMultiSigResponse>(RpcMethods.createmultisig, nRquired, publicKeys);
        }

        public Task<string> CreateRawTransaction(CreateRawTransactionRequest rawTransaction)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.createrawtransaction, rawTransaction.Inputs, rawTransaction.Outputs);
        }

        public Task<DecodeRawTransactionResponse> DecodeRawTransaction(string rawTransactionHexString)
        {
            return _rpcConnector.MakeRequestAsync<DecodeRawTransactionResponse>(RpcMethods.decoderawtransaction, rawTransactionHexString);
        }

        public Task<DecodeScriptResponse> DecodeScript(string hexString)
        {
            return _rpcConnector.MakeRequestAsync<DecodeScriptResponse>(RpcMethods.decodescript, hexString);
        }

        public Task<string> DumpPrivKey(string bitcoinAddress)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.dumpprivkey, bitcoinAddress);
        }

        public Task DumpWallet(string filename)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.dumpwallet, filename);
        }

        public Task<decimal> EstimateFee(ushort nBlocks)
        {
            return _rpcConnector.MakeRequestAsync<decimal>(RpcMethods.estimatefee, nBlocks);
        }

        public Task<decimal> EstimatePriority(ushort nBlocks)
        {
            return _rpcConnector.MakeRequestAsync<decimal>(RpcMethods.estimatepriority, nBlocks);
        }

        public Task<string> GetAccount(string bitcoinAddress)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.getaccount, bitcoinAddress);
        }

        public Task<string> GetAccountAddress(string account)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.getaccountaddress, account);
        }

        public Task<GetAddedNodeInfoResponse> GetAddedNodeInfo(string dns, string node)
        {
            return string.IsNullOrWhiteSpace(node)
                ? _rpcConnector.MakeRequestAsync<GetAddedNodeInfoResponse>(RpcMethods.getaddednodeinfo, dns)
                : _rpcConnector.MakeRequestAsync<GetAddedNodeInfoResponse>(RpcMethods.getaddednodeinfo, dns, node);
        }

        public Task<List<string>> GetAddressesByAccount(string account)
        {
            return _rpcConnector.MakeRequestAsync<List<string>>(RpcMethods.getaddressesbyaccount, account);
        }

        public Task<decimal> GetBalance(string account, int minConf, bool? includeWatchonly)
        {
            return includeWatchonly == null
                ? _rpcConnector.MakeRequestAsync<decimal>(RpcMethods.getbalance, (string.IsNullOrWhiteSpace(account) ? "*" : account), minConf)
                : _rpcConnector.MakeRequestAsync<decimal>(RpcMethods.getbalance, (string.IsNullOrWhiteSpace(account) ? "*" : account), minConf, includeWatchonly);
        }

        public Task<string> GetBestBlockHash()
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.getbestblockhash);
        }

        public Task<GetBlockResponse> GetBlock(string hash, bool verbose)
        {
            return _rpcConnector.MakeRequestAsync<GetBlockResponse>(RpcMethods.getblock, hash, verbose);
        }

        public Task<GetBlockchainInfoResponse> GetBlockchainInfo()
        {
            return _rpcConnector.MakeRequestAsync<GetBlockchainInfoResponse>(RpcMethods.getblockchaininfo);
        }

        public Task<uint> GetBlockCount()
        {
            return _rpcConnector.MakeRequestAsync<uint>(RpcMethods.getblockcount);
        }

        public Task<string> GetBlockHash(long index)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.getblockhash, index);
        }

        public Task<GetBlockTemplateResponse> GetBlockTemplate(params object[] parameters)
        {
            return parameters == null
                ? _rpcConnector.MakeRequestAsync<GetBlockTemplateResponse>(RpcMethods.getblocktemplate)
                : _rpcConnector.MakeRequestAsync<GetBlockTemplateResponse>(RpcMethods.getblocktemplate, parameters);
        }

        public Task<List<GetChainTipsResponse>> GetChainTips()
        {
            return _rpcConnector.MakeRequestAsync<List<GetChainTipsResponse>>(RpcMethods.getchaintips);
        }

        public Task<int> GetConnectionCount()
        {
            return _rpcConnector.MakeRequestAsync<int>(RpcMethods.getconnectioncount);
        }

        public Task<double> GetDifficulty()
        {
            return _rpcConnector.MakeRequestAsync<double>(RpcMethods.getdifficulty);
        }

        public Task<bool> GetGenerate()
        {
            return _rpcConnector.MakeRequestAsync<bool>(RpcMethods.getgenerate);
        }

        [Obsolete("Please use calls: GetWalletInfo(), GetBlockchainInfo() and GetNetworkInfo() instead")]
        public Task<GetInfoResponse> GetInfo()
        {
            return _rpcConnector.MakeRequestAsync<GetInfoResponse>(RpcMethods.getinfo);
        }

        public Task<GetMemPoolInfoResponse> GetMemPoolInfo()
        {
            return _rpcConnector.MakeRequestAsync<GetMemPoolInfoResponse>(RpcMethods.getmempoolinfo);
        }

        public Task<GetMiningInfoResponse> GetMiningInfo()
        {
            return _rpcConnector.MakeRequestAsync<GetMiningInfoResponse>(RpcMethods.getmininginfo);
        }

        public Task<GetNetTotalsResponse> GetNetTotals()
        {
            return _rpcConnector.MakeRequestAsync<GetNetTotalsResponse>(RpcMethods.getnettotals);
        }

        public Task<ulong> GetNetworkHashPs(uint blocks, long height)
        {
            return _rpcConnector.MakeRequestAsync<ulong>(RpcMethods.getnetworkhashps);
        }

        public Task<GetNetworkInfoResponse> GetNetworkInfo()
        {
            return _rpcConnector.MakeRequestAsync<GetNetworkInfoResponse>(RpcMethods.getnetworkinfo);
        }

        public Task<string> GetNewAddress(string account)
        {
            return string.IsNullOrWhiteSpace(account)
                ? _rpcConnector.MakeRequestAsync<string>(RpcMethods.getnewaddress)
                : _rpcConnector.MakeRequestAsync<string>(RpcMethods.getnewaddress, account);
        }

        public Task<List<GetPeerInfoResponse>> GetPeerInfo()
        {
            return _rpcConnector.MakeRequestAsync<List<GetPeerInfoResponse>>(RpcMethods.getpeerinfo);
        }

        public async Task<GetRawMemPoolResponse> GetRawMemPool(bool verbose)
        {
            var getRawMemPoolResponse = new GetRawMemPoolResponse
            {
                IsVerbose = verbose
            };

            var rpcResponse = await _rpcConnector.MakeRequestAsync<object>(RpcMethods.getrawmempool, verbose);

            if (!verbose)
            {
                var rpcResponseAsArray = (JArray)rpcResponse;

                foreach (string txId in rpcResponseAsArray)
                {
                    getRawMemPoolResponse.TxIds.Add(txId);
                }

                return getRawMemPoolResponse;
            }

            IList<KeyValuePair<string, JToken>> rpcResponseAsKvp = (new List<KeyValuePair<string, JToken>>(((JObject)(rpcResponse)))).ToList();
            IList<JToken> children = JObject.Parse(rpcResponse.ToString()).Children().ToList();

            for (var i = 0; i < children.Count(); i++)
            {
                var getRawMemPoolVerboseResponse = new GetRawMemPoolVerboseResponse
                {
                    TxId = rpcResponseAsKvp[i].Key
                };

                getRawMemPoolResponse.TxIds.Add(getRawMemPoolVerboseResponse.TxId);

                foreach (var property in children[i].SelectMany(grandChild => grandChild.OfType<JProperty>()))
                {
                    switch (property.Name)
                    {
                        case "currentpriority":

                            double currentPriority;

                            if (double.TryParse(property.Value.ToString(), out currentPriority))
                            {
                                getRawMemPoolVerboseResponse.CurrentPriority = currentPriority;
                            }

                            break;

                        case "depends":
                            getRawMemPoolVerboseResponse.Depends=new string[property.Value.Count()];

                            for (int j = 0; j < property.Value.Count(); j++)
                            {
                                getRawMemPoolVerboseResponse.Depends[j] = property.Value[j].Value<string>();
                            }
                            //foreach (var jToken in property.Value)
                            //{
                            //    getRawMemPoolVerboseResponse.Depends.Add(jToken.Value<string>());
                            //}

                            break;

                        case "fee":

                            decimal fee;

                            if (decimal.TryParse(property.Value.ToString(), out fee))
                            {
                                getRawMemPoolVerboseResponse.Fee = fee;
                            }

                            break;

                        case "height":

                            int height;

                            if (int.TryParse(property.Value.ToString(), out height))
                            {
                                getRawMemPoolVerboseResponse.Height = height;
                            }

                            break;

                        case "size":

                            int size;

                            if (int.TryParse(property.Value.ToString(), out size))
                            {
                                getRawMemPoolVerboseResponse.Size = size;
                            }

                            break;

                        case "startingpriority":

                            double startingPriority;

                            if (double.TryParse(property.Value.ToString(), out startingPriority))
                            {
                                getRawMemPoolVerboseResponse.StartingPriority = startingPriority;
                            }

                            break;

                        case "time":

                            int time;

                            if (int.TryParse(property.Value.ToString(), out time))
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

        public Task<string> GetRawChangeAddress()
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.getrawchangeaddress);
        }

        public async Task<GetRawTransactionResponse> GetRawTransaction(string txId, int verbose)
        {
            if (verbose == 0)
            {
                return new GetRawTransactionResponse
                {
                    Hex = await _rpcConnector.MakeRequestAsync<string>(RpcMethods.getrawtransaction, txId, verbose)
                };
            }

            if (verbose == 1)
            {
                return await _rpcConnector.MakeRequestAsync<GetRawTransactionResponse>(RpcMethods.getrawtransaction, txId, verbose);
            }

            throw new Exception("Invalid verbose value: " + verbose + " in GetRawTransaction()!");
        }

        public Task<decimal> GetReceivedByAccount(string account, int minConf)
        {
            return _rpcConnector.MakeRequestAsync<decimal>(RpcMethods.getreceivedbyaccount, account, minConf);
        }

        public Task<decimal> GetReceivedByAddress(string bitcoinAddress, int minConf)
        {
            return _rpcConnector.MakeRequestAsync<decimal>(RpcMethods.getreceivedbyaddress, bitcoinAddress, minConf);
        }

        public Task<GetTransactionResponse> GetTransaction(string txId, bool? includeWatchonly)
        {
            return includeWatchonly == null
                ? _rpcConnector.MakeRequestAsync<GetTransactionResponse>(RpcMethods.gettransaction, txId)
                : _rpcConnector.MakeRequestAsync<GetTransactionResponse>(RpcMethods.gettransaction, txId, includeWatchonly);
        }

        public Task<GetTransactionResponse> GetTxOut(string txId, int n, bool includeMemPool)
        {
            return _rpcConnector.MakeRequestAsync<GetTransactionResponse>(RpcMethods.gettxout, txId, n, includeMemPool);
        }

        public Task<GetTxOutSetInfoResponse> GetTxOutSetInfo()
        {
            return _rpcConnector.MakeRequestAsync<GetTxOutSetInfoResponse>(RpcMethods.gettxoutsetinfo);
        }

        public Task<decimal> GetUnconfirmedBalance()
        {
            return _rpcConnector.MakeRequestAsync<decimal>(RpcMethods.getunconfirmedbalance);
        }

        public Task<GetWalletInfoResponse> GetWalletInfo()
        {
            return _rpcConnector.MakeRequestAsync<GetWalletInfoResponse>(RpcMethods.getwalletinfo);
        }

        public Task<string> Help(string command)
        {
            return string.IsNullOrWhiteSpace(command)
                ? _rpcConnector.MakeRequestAsync<string>(RpcMethods.help)
                : _rpcConnector.MakeRequestAsync<string>(RpcMethods.help, command);
        }

        public Task ImportAddress(string address, string label, bool rescan)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.importaddress, address, label, rescan);
        }

        public Task<string> ImportPrivKey(string privateKey, string label, bool rescan)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.importprivkey, privateKey, label, rescan);
        }

        public Task ImportWallet(string filename)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.importwallet, filename);
        }

        public Task<string> KeyPoolRefill(uint newSize)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.keypoolrefill, newSize);
        }

        public Task<Dictionary<string, decimal>> ListAccounts(int minConf, bool? includeWatchonly)
        {
            return includeWatchonly == null
                ? _rpcConnector.MakeRequestAsync<Dictionary<string, decimal>>(RpcMethods.listaccounts, minConf)
                : _rpcConnector.MakeRequestAsync<Dictionary<string, decimal>>(RpcMethods.listaccounts, minConf, includeWatchonly);
        }

        public async Task<List<List<ListAddressGroupingsResponse>>> ListAddressGroupings()
        {
            var unstructuredResponse = await _rpcConnector.MakeRequestAsync<List<List<List<object>>>>(RpcMethods.listaddressgroupings);
            var structuredResponse = new List<List<ListAddressGroupingsResponse>>(unstructuredResponse.Count);

            for (var i = 0; i < unstructuredResponse.Count; i++)
            {
                for (var j = 0; j < unstructuredResponse[i].Count; j++)
                {
                    if (unstructuredResponse[i][j].Count > 1)
                    {
                        var response = new ListAddressGroupingsResponse
                        {
                            Address = unstructuredResponse[i][j][0].ToString()
                        };

                        decimal balance;
                        decimal.TryParse(unstructuredResponse[i][j][1].ToString(), out balance);

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

        public Task<string> ListLockUnspent()
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.listlockunspent);
        }

        public Task<List<ListReceivedByAccountResponse>> ListReceivedByAccount(int minConf, bool includeEmpty, bool? includeWatchonly)
        {
            return includeWatchonly == null
                ? _rpcConnector.MakeRequestAsync<List<ListReceivedByAccountResponse>>(RpcMethods.listreceivedbyaccount, minConf, includeEmpty)
                : _rpcConnector.MakeRequestAsync<List<ListReceivedByAccountResponse>>(RpcMethods.listreceivedbyaccount, minConf, includeEmpty, includeWatchonly);
        }

        public Task<List<ListReceivedByAddressResponse>> ListReceivedByAddress(int minConf, bool includeEmpty, bool? includeWatchonly)
        {
            return includeWatchonly == null
                ? _rpcConnector.MakeRequestAsync<List<ListReceivedByAddressResponse>>(RpcMethods.listreceivedbyaddress, minConf, includeEmpty)
                : _rpcConnector.MakeRequestAsync<List<ListReceivedByAddressResponse>>(RpcMethods.listreceivedbyaddress, minConf, includeEmpty, includeWatchonly);
        }

        public Task<ListSinceBlockResponse> ListSinceBlock(string blockHash, int targetConfirmations, bool? includeWatchonly)
        {
            return includeWatchonly == null
                ? _rpcConnector.MakeRequestAsync<ListSinceBlockResponse>(RpcMethods.listsinceblock, (string.IsNullOrWhiteSpace(blockHash) ? "*" : blockHash), targetConfirmations)
                : _rpcConnector.MakeRequestAsync<ListSinceBlockResponse>(RpcMethods.listsinceblock, (string.IsNullOrWhiteSpace(blockHash) ? "*" : blockHash), targetConfirmations, includeWatchonly);
        }

        public Task<List<ListTransactionsResponse>> ListTransactions(string account, int count, int from, bool? includeWatchonly)
        {
            return includeWatchonly == null
                ? _rpcConnector.MakeRequestAsync<List<ListTransactionsResponse>>(RpcMethods.listtransactions, (string.IsNullOrWhiteSpace(account) ? "*" : account), count, from)
                : _rpcConnector.MakeRequestAsync<List<ListTransactionsResponse>>(RpcMethods.listtransactions, (string.IsNullOrWhiteSpace(account) ? "*" : account), count, from, includeWatchonly);
        }

        public Task<List<ListUnspentResponse>> ListUnspent(int minConf, int maxConf, List<string> addresses)
        {
            return _rpcConnector.MakeRequestAsync<List<ListUnspentResponse>>(RpcMethods.listunspent, minConf, maxConf, (addresses ?? new List<string>()));
        }

        public Task<bool> LockUnspent(bool unlock, IList<ListUnspentResponse> listUnspentResponses)
        {
            IList<object> transactions = new List<object>();

            foreach (var listUnspentResponse in listUnspentResponses)
            {
                transactions.Add(new
                {
                    txid = listUnspentResponse.TxId,
                    listUnspentResponse.Vout
                });
            }

            return _rpcConnector.MakeRequestAsync<bool>(RpcMethods.lockunspent, unlock, transactions.ToArray());
        }

        public Task<bool> Move(string fromAccount, string toAccount, decimal amount, int minConf, string comment)
        {
            return _rpcConnector.MakeRequestAsync<bool>(RpcMethods.move, fromAccount, toAccount, amount, minConf, comment);
        }

        public Task Ping()
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.ping);
        }

        public Task<bool> PrioritiseTransaction(string txId, decimal priorityDelta, decimal feeDelta)
        {
            return _rpcConnector.MakeRequestAsync<bool>(RpcMethods.prioritisetransaction, txId, priorityDelta, feeDelta);
        }

        public Task<string> SendFrom(string fromAccount, string toBitcoinAddress, decimal amount, int minConf, string comment, string commentTo)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.sendfrom, fromAccount, toBitcoinAddress, amount, minConf, comment, commentTo);
        }

        public Task<string> SendMany(string fromAccount, Dictionary<string, decimal> toBitcoinAddress, int minConf, string comment)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.sendmany, fromAccount, toBitcoinAddress, minConf, comment);
        }

        public Task<string> SendRawTransaction(string rawTransactionHexString, bool? allowHighFees)
        {
            return allowHighFees == null
                ? _rpcConnector.MakeRequestAsync<string>(RpcMethods.sendrawtransaction, rawTransactionHexString)
                : _rpcConnector.MakeRequestAsync<string>(RpcMethods.sendrawtransaction, rawTransactionHexString, allowHighFees);
        }

        public Task<string> SendToAddress(string bitcoinAddress, decimal amount, string comment, string commentTo)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.sendtoaddress, bitcoinAddress, amount, comment, commentTo);
        }

        public Task<string> SetAccount(string bitcoinAddress, string account)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.setaccount, bitcoinAddress, account);
        }

        public Task<string> SetGenerate(bool generate, short generatingProcessorsLimit)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.setgenerate, generate, generatingProcessorsLimit);
        }

        public Task<string> SetTxFee(decimal amount)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.settxfee, amount);
        }

        public Task<string> SignMessage(string bitcoinAddress, string message)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.signmessage, bitcoinAddress, message);
        }

        public Task<SignRawTransactionResponse> SignRawTransaction(SignRawTransactionRequest request)
        {
            #region default values

            if (request.Inputs.Count == 0)
            {
                request.Inputs = null;
            }

            if (string.IsNullOrWhiteSpace(request.SigHashType))
            {
                request.SigHashType = SigHashType.All;
            }

            if (request.PrivateKeys.Count == 0)
            {
                request.PrivateKeys = null;
            }

            #endregion

            return _rpcConnector.MakeRequestAsync<SignRawTransactionResponse>(RpcMethods.signrawtransaction, request.RawTransactionHex, request.Inputs, request.PrivateKeys, request.SigHashType);
        }

        public Task<string> Stop()
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.stop);
        }

        public Task<string> SubmitBlock(string hexData, params object[] parameters)
        {
            return parameters == null
                ? _rpcConnector.MakeRequestAsync<string>(RpcMethods.submitblock, hexData)
                : _rpcConnector.MakeRequestAsync<string>(RpcMethods.submitblock, hexData, parameters);
        }

        public Task<ValidateAddressResponse> ValidateAddress(string bitcoinAddress)
        {
            return _rpcConnector.MakeRequestAsync<ValidateAddressResponse>(RpcMethods.validateaddress, bitcoinAddress);
        }

        public Task<bool> VerifyChain(ushort checkLevel, uint numBlocks)
        {
            return _rpcConnector.MakeRequestAsync<bool>(RpcMethods.verifychain, checkLevel, numBlocks);
        }

        public Task<bool> VerifyMessage(string bitcoinAddress, string signature, string message)
        {
            return _rpcConnector.MakeRequestAsync<bool>(RpcMethods.verifymessage, bitcoinAddress, signature, message);
        }

        public Task<string> WalletLock()
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.walletlock);
        }

        public Task<string> WalletPassphrase(string passphrase, int timeoutInSeconds)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.walletpassphrase, passphrase, timeoutInSeconds);
        }

        public Task<string> WalletPassphraseChange(string oldPassphrase, string newPassphrase)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.walletpassphrasechange, oldPassphrase, newPassphrase);
        }

        public override string ToString()
        {
            return Parameters.CoinLongName;
        }
    }
}