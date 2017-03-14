// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BitcoinLib.Auxiliary;
using BitcoinLib.ExceptionHandling.RpcExtenderService;
using BitcoinLib.ExtensionMethods;
using BitcoinLib.Requests.AddNode;
using BitcoinLib.Requests.CreateRawTransaction;
using BitcoinLib.Requests.SignRawTransaction;
using BitcoinLib.Responses;
using BitcoinLib.RPC.Specifications;
using BitcoinLib.Services.Coins.Base;
using Newtonsoft.Json.Linq;

namespace BitcoinLib.Services
{
    public partial class CoinService : ICoinService
    {
        public Task<string> AddMultiSigAddressAsync(int nRquired, List<string> publicKeys, string account)
        {
            return account != null
                ? _rpcConnector.MakeRequestAsync<string>(RpcMethods.addmultisigaddress, nRquired, publicKeys, account)
                : _rpcConnector.MakeRequestAsync<string>(RpcMethods.addmultisigaddress, nRquired, publicKeys);
        }

        public Task AddNodeAsync(string node, NodeAction action)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.addnode, node, action.ToString());
        }

        public Task BackupWalletAsync(string destination)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.backupwallet, destination);
        }

        public Task<CreateMultiSigResponse> CreateMultiSigAsync(int nRquired, List<string> publicKeys)
        {
            return _rpcConnector.MakeRequestAsync<CreateMultiSigResponse>(RpcMethods.createmultisig, nRquired, publicKeys);
        }

        public Task<string> CreateRawTransactionAsync(CreateRawTransactionRequest rawTransaction)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.createrawtransaction, rawTransaction.Inputs, rawTransaction.Outputs);
        }

        public Task<DecodeRawTransactionResponse> DecodeRawTransactionAsync(string rawTransactionHexString)
        {
            return _rpcConnector.MakeRequestAsync<DecodeRawTransactionResponse>(RpcMethods.decoderawtransaction, rawTransactionHexString);
        }

        public Task<DecodeScriptResponse> DecodeScriptAsync(string hexString)
        {
            return _rpcConnector.MakeRequestAsync<DecodeScriptResponse>(RpcMethods.decodescript, hexString);
        }

        public Task<string> DumpPrivKeyAsync(string bitcoinAddress)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.dumpprivkey, bitcoinAddress);
        }

        public Task DumpWalletAsync(string filename)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.dumpwallet, filename);
        }

        public Task<decimal> EstimateFeeAsync(ushort nBlocks)
        {
            return _rpcConnector.MakeRequestAsync<decimal>(RpcMethods.estimatefee, nBlocks);
        }

        public Task<decimal> EstimatePriorityAsync(ushort nBlocks)
        {
            return _rpcConnector.MakeRequestAsync<decimal>(RpcMethods.estimatepriority, nBlocks);
        }

        public Task<string> GetAccountAsync(string bitcoinAddress)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.getaccount, bitcoinAddress);
        }

        public Task<string> GetAccountAddressAsync(string account)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.getaccountaddress, account);
        }

        public Task<GetAddedNodeInfoResponse> GetAddedNodeInfoAsync(string dns, string node)
        {
            return string.IsNullOrWhiteSpace(node)
                ? _rpcConnector.MakeRequestAsync<GetAddedNodeInfoResponse>(RpcMethods.getaddednodeinfo, dns)
                : _rpcConnector.MakeRequestAsync<GetAddedNodeInfoResponse>(RpcMethods.getaddednodeinfo, dns, node);
        }

        public Task<List<string>> GetAddressesByAccountAsync(string account)
        {
            return _rpcConnector.MakeRequestAsync<List<string>>(RpcMethods.getaddressesbyaccount, account);
        }

        public Task<decimal> GetBalanceAsync(string account, int minConf, bool? includeWatchonly)
        {
            return includeWatchonly == null
                ? _rpcConnector.MakeRequestAsync<decimal>(RpcMethods.getbalance, (string.IsNullOrWhiteSpace(account) ? "*" : account), minConf)
                : _rpcConnector.MakeRequestAsync<decimal>(RpcMethods.getbalance, (string.IsNullOrWhiteSpace(account) ? "*" : account), minConf, includeWatchonly);
        }

        public Task<string> GetBestBlockHashAsync()
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.getbestblockhash);
        }

        public Task<GetBlockResponse> GetBlockAsync(string hash, bool verbose)
        {
            return _rpcConnector.MakeRequestAsync<GetBlockResponse>(RpcMethods.getblock, hash, verbose);
        }

        public Task<GetBlockchainInfoResponse> GetBlockchainInfoAsync()
        {
            return _rpcConnector.MakeRequestAsync<GetBlockchainInfoResponse>(RpcMethods.getblockchaininfo);
        }

        public Task<uint> GetBlockCountAsync()
        {
            return _rpcConnector.MakeRequestAsync<uint>(RpcMethods.getblockcount);
        }

        public Task<string> GetBlockHashAsync(long index)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.getblockhash, index);
        }

        public Task<GetBlockTemplateResponse> GetBlockTemplateAsync(params object[] parameters)
        {
            return parameters == null
                ? _rpcConnector.MakeRequestAsync<GetBlockTemplateResponse>(RpcMethods.getblocktemplate)
                : _rpcConnector.MakeRequestAsync<GetBlockTemplateResponse>(RpcMethods.getblocktemplate, parameters);
        }

        public Task<List<GetChainTipsResponse>> GetChainTipsAsync()
        {
            return _rpcConnector.MakeRequestAsync<List<GetChainTipsResponse>>(RpcMethods.getchaintips);
        }

        public Task<int> GetConnectionCountAsync()
        {
            return _rpcConnector.MakeRequestAsync<int>(RpcMethods.getconnectioncount);
        }

        public Task<double> GetDifficultyAsync()
        {
            return _rpcConnector.MakeRequestAsync<double>(RpcMethods.getdifficulty);
        }

        public Task<bool> GetGenerateAsync()
        {
            return _rpcConnector.MakeRequestAsync<bool>(RpcMethods.getgenerate);
        }

        [Obsolete("Please use calls: GetWalletInfo(), GetBlockchainInfo() and GetNetworkInfo() instead")]
        public Task<GetInfoResponse> GetInfoAsync()
        {
            return _rpcConnector.MakeRequestAsync<GetInfoResponse>(RpcMethods.getinfo);
        }

        public Task<GetMemPoolInfoResponse> GetMemPoolInfoAsync()
        {
            return _rpcConnector.MakeRequestAsync<GetMemPoolInfoResponse>(RpcMethods.getmempoolinfo);
        }

        public Task<GetMiningInfoResponse> GetMiningInfoAsync()
        {
            return _rpcConnector.MakeRequestAsync<GetMiningInfoResponse>(RpcMethods.getmininginfo);
        }

        public Task<GetNetTotalsResponse> GetNetTotalsAsync()
        {
            return _rpcConnector.MakeRequestAsync<GetNetTotalsResponse>(RpcMethods.getnettotals);
        }

        public Task<ulong> GetNetworkHashPsAsync(uint blocks, long height)
        {
            return _rpcConnector.MakeRequestAsync<ulong>(RpcMethods.getnetworkhashps);
        }

        public Task<GetNetworkInfoResponse> GetNetworkInfoAsync()
        {
            return _rpcConnector.MakeRequestAsync<GetNetworkInfoResponse>(RpcMethods.getnetworkinfo);
        }

        public Task<string> GetNewAddressAsync(string account)
        {
            return string.IsNullOrWhiteSpace(account)
                ? _rpcConnector.MakeRequestAsync<string>(RpcMethods.getnewaddress)
                : _rpcConnector.MakeRequestAsync<string>(RpcMethods.getnewaddress, account);
        }

        public Task<List<GetPeerInfoResponse>> GetPeerInfoAsync()
        {
            return _rpcConnector.MakeRequestAsync<List<GetPeerInfoResponse>>(RpcMethods.getpeerinfo);
        }

        public async Task<GetRawMemPoolResponse> GetRawMemPoolAsync(bool verbose)
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
                            getRawMemPoolVerboseResponse.Depends = new string[property.Value.Count()];

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

        public Task<string> GetRawChangeAddressAsync()
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.getrawchangeaddress);
        }

        public async Task<GetRawTransactionResponse> GetRawTransactionAsync(string txId, int verbose)
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

        public Task<decimal> GetReceivedByAccountAsync(string account, int minConf)
        {
            return _rpcConnector.MakeRequestAsync<decimal>(RpcMethods.getreceivedbyaccount, account, minConf);
        }

        public Task<decimal> GetReceivedByAddressAsync(string bitcoinAddress, int minConf)
        {
            return _rpcConnector.MakeRequestAsync<decimal>(RpcMethods.getreceivedbyaddress, bitcoinAddress, minConf);
        }

        public Task<GetTransactionResponse> GetTransactionAsync(string txId, bool? includeWatchonly)
        {
            return includeWatchonly == null
                ? _rpcConnector.MakeRequestAsync<GetTransactionResponse>(RpcMethods.gettransaction, txId)
                : _rpcConnector.MakeRequestAsync<GetTransactionResponse>(RpcMethods.gettransaction, txId, includeWatchonly);
        }

        public Task<GetTransactionResponse> GetTxOutAsync(string txId, int n, bool includeMemPool)
        {
            return _rpcConnector.MakeRequestAsync<GetTransactionResponse>(RpcMethods.gettxout, txId, n, includeMemPool);
        }

        public Task<GetTxOutSetInfoResponse> GetTxOutSetInfoAsync()
        {
            return _rpcConnector.MakeRequestAsync<GetTxOutSetInfoResponse>(RpcMethods.gettxoutsetinfo);
        }

        public Task<decimal> GetUnconfirmedBalanceAsync()
        {
            return _rpcConnector.MakeRequestAsync<decimal>(RpcMethods.getunconfirmedbalance);
        }

        public Task<GetWalletInfoResponse> GetWalletInfoAsync()
        {
            return _rpcConnector.MakeRequestAsync<GetWalletInfoResponse>(RpcMethods.getwalletinfo);
        }

        public Task<string> HelpAsync(string command)
        {
            return string.IsNullOrWhiteSpace(command)
                ? _rpcConnector.MakeRequestAsync<string>(RpcMethods.help)
                : _rpcConnector.MakeRequestAsync<string>(RpcMethods.help, command);
        }

        public Task ImportAddressAsync(string address, string label, bool rescan)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.importaddress, address, label, rescan);
        }

        public Task<string> ImportPrivKeyAsync(string privateKey, string label, bool rescan)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.importprivkey, privateKey, label, rescan);
        }

        public Task ImportWalletAsync(string filename)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.importwallet, filename);
        }

        public Task<string> KeyPoolRefillAsync(uint newSize)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.keypoolrefill, newSize);
        }

        public Task<Dictionary<string, decimal>> ListAccountsAsync(int minConf, bool? includeWatchonly)
        {
            return includeWatchonly == null
                ? _rpcConnector.MakeRequestAsync<Dictionary<string, decimal>>(RpcMethods.listaccounts, minConf)
                : _rpcConnector.MakeRequestAsync<Dictionary<string, decimal>>(RpcMethods.listaccounts, minConf, includeWatchonly);
        }

        public async Task<List<List<ListAddressGroupingsResponse>>> ListAddressGroupingsAsync()
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

        public Task<string> ListLockUnspentAsync()
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.listlockunspent);
        }

        public Task<List<ListReceivedByAccountResponse>> ListReceivedByAccountAsync(int minConf, bool includeEmpty, bool? includeWatchonly)
        {
            return includeWatchonly == null
                ? _rpcConnector.MakeRequestAsync<List<ListReceivedByAccountResponse>>(RpcMethods.listreceivedbyaccount, minConf, includeEmpty)
                : _rpcConnector.MakeRequestAsync<List<ListReceivedByAccountResponse>>(RpcMethods.listreceivedbyaccount, minConf, includeEmpty, includeWatchonly);
        }

        public Task<List<ListReceivedByAddressResponse>> ListReceivedByAddressAsync(int minConf, bool includeEmpty, bool? includeWatchonly)
        {
            return includeWatchonly == null
                ? _rpcConnector.MakeRequestAsync<List<ListReceivedByAddressResponse>>(RpcMethods.listreceivedbyaddress, minConf, includeEmpty)
                : _rpcConnector.MakeRequestAsync<List<ListReceivedByAddressResponse>>(RpcMethods.listreceivedbyaddress, minConf, includeEmpty, includeWatchonly);
        }

        public Task<ListSinceBlockResponse> ListSinceBlockAsync(string blockHash, int targetConfirmations, bool? includeWatchonly)
        {
            return includeWatchonly == null
                ? _rpcConnector.MakeRequestAsync<ListSinceBlockResponse>(RpcMethods.listsinceblock, (string.IsNullOrWhiteSpace(blockHash) ? "*" : blockHash), targetConfirmations)
                : _rpcConnector.MakeRequestAsync<ListSinceBlockResponse>(RpcMethods.listsinceblock, (string.IsNullOrWhiteSpace(blockHash) ? "*" : blockHash), targetConfirmations, includeWatchonly);
        }

        public Task<List<ListTransactionsResponse>> ListTransactionsAsync(string account, int count, int from, bool? includeWatchonly)
        {
            return includeWatchonly == null
                ? _rpcConnector.MakeRequestAsync<List<ListTransactionsResponse>>(RpcMethods.listtransactions, (string.IsNullOrWhiteSpace(account) ? "*" : account), count, from)
                : _rpcConnector.MakeRequestAsync<List<ListTransactionsResponse>>(RpcMethods.listtransactions, (string.IsNullOrWhiteSpace(account) ? "*" : account), count, from, includeWatchonly);
        }

        public Task<List<ListUnspentResponse>> ListUnspentAsync(int minConf, int maxConf, List<string> addresses)
        {
            return _rpcConnector.MakeRequestAsync<List<ListUnspentResponse>>(RpcMethods.listunspent, minConf, maxConf, (addresses ?? new List<string>()));
        }

        public Task<bool> LockUnspentAsync(bool unlock, IList<ListUnspentResponse> listUnspentResponses)
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

        public Task<bool> MoveAsync(string fromAccount, string toAccount, decimal amount, int minConf, string comment)
        {
            return _rpcConnector.MakeRequestAsync<bool>(RpcMethods.move, fromAccount, toAccount, amount, minConf, comment);
        }

        public Task PingAsync()
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.ping);
        }

        public Task<bool> PrioritiseTransactionAsync(string txId, decimal priorityDelta, decimal feeDelta)
        {
            return _rpcConnector.MakeRequestAsync<bool>(RpcMethods.prioritisetransaction, txId, priorityDelta, feeDelta);
        }

        public Task<string> SendFromAsync(string fromAccount, string toBitcoinAddress, decimal amount, int minConf, string comment, string commentTo)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.sendfrom, fromAccount, toBitcoinAddress, amount, minConf, comment, commentTo);
        }

        public Task<string> SendManyAsync(string fromAccount, Dictionary<string, decimal> toBitcoinAddress, int minConf, string comment)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.sendmany, fromAccount, toBitcoinAddress, minConf, comment);
        }

        public Task<string> SendRawTransactionAsync(string rawTransactionHexString, bool? allowHighFees)
        {
            return allowHighFees == null
                ? _rpcConnector.MakeRequestAsync<string>(RpcMethods.sendrawtransaction, rawTransactionHexString)
                : _rpcConnector.MakeRequestAsync<string>(RpcMethods.sendrawtransaction, rawTransactionHexString, allowHighFees);
        }

        public Task<string> SendToAddressAsync(string bitcoinAddress, decimal amount, string comment, string commentTo)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.sendtoaddress, bitcoinAddress, amount, comment, commentTo);
        }

        public Task<string> SetAccountAsync(string bitcoinAddress, string account)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.setaccount, bitcoinAddress, account);
        }

        public Task<string> SetGenerateAsync(bool generate, short generatingProcessorsLimit)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.setgenerate, generate, generatingProcessorsLimit);
        }

        public Task<string> SetTxFeeAsync(decimal amount)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.settxfee, amount);
        }

        public Task<string> SignMessageAsync(string bitcoinAddress, string message)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.signmessage, bitcoinAddress, message);
        }

        public Task<SignRawTransactionResponse> SignRawTransactionAsync(SignRawTransactionRequest request)
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

        public Task<string> StopAsync()
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.stop);
        }

        public Task<string> SubmitBlockAsync(string hexData, params object[] parameters)
        {
            return parameters == null
                ? _rpcConnector.MakeRequestAsync<string>(RpcMethods.submitblock, hexData)
                : _rpcConnector.MakeRequestAsync<string>(RpcMethods.submitblock, hexData, parameters);
        }

        public Task<ValidateAddressResponse> ValidateAddressAsync(string bitcoinAddress)
        {
            return _rpcConnector.MakeRequestAsync<ValidateAddressResponse>(RpcMethods.validateaddress, bitcoinAddress);
        }

        public Task<bool> VerifyChainAsync(ushort checkLevel, uint numBlocks)
        {
            return _rpcConnector.MakeRequestAsync<bool>(RpcMethods.verifychain, checkLevel, numBlocks);
        }

        public Task<bool> VerifyMessageAsync(string bitcoinAddress, string signature, string message)
        {
            return _rpcConnector.MakeRequestAsync<bool>(RpcMethods.verifymessage, bitcoinAddress, signature, message);
        }

        public Task<string> WalletLockAsync()
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.walletlock);
        }

        public Task<string> WalletPassphraseAsync(string passphrase, int timeoutInSeconds)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.walletpassphrase, passphrase, timeoutInSeconds);
        }

        public Task<string> WalletPassphraseChangeAsync(string oldPassphrase, string newPassphrase)
        {
            return _rpcConnector.MakeRequestAsync<string>(RpcMethods.walletpassphrasechange, oldPassphrase, newPassphrase);
        }
        //  Note: This will return funky results if the address in question along with its private key have been used to create a multisig address with unspent funds
        public async Task<decimal> GetAddressBalanceAsync(string inWalletAddress, int minConf, bool validateAddressBeforeProcessing)
        {
            if (validateAddressBeforeProcessing)
            {
                var validateAddressResponse = await ValidateAddressAsync(inWalletAddress);

                if (!validateAddressResponse.IsValid)
                {
                    throw new GetAddressBalanceException($"Address {inWalletAddress} is invalid!");
                }

                if (!validateAddressResponse.IsMine)
                {
                    throw new GetAddressBalanceException($"Address {inWalletAddress} is not an in-wallet address!");
                }
            }

            var listUnspentResponses = await ListUnspentAsync(minConf, 9999999, new List<string>
            {
                inWalletAddress
            });

            return listUnspentResponses.Any() ? listUnspentResponses.Sum(x => x.Amount) : 0;
        }

        public async Task<string> GetImmutableTxIdAsync(string txId, bool getSha256Hash)
        {
            var response = await GetRawTransactionAsync(txId, 1);
            var text = response.Vin.First().TxId + "|" + response.Vin.First().Vout + "|" + response.Vout.First().Value;
            return getSha256Hash ? Hashing.GetSha256(text) : text;
        }

        //  Get a rough estimate on fees for non-free txs, depending on the total number of tx inputs and outputs
        [Obsolete("Please don't use this method to calculate tx fees, its purpose is to provide a rough estimate only")]
        public async Task<decimal> GetMinimumNonZeroTransactionFeeEstimateAsync(short numberOfInputs = 1, short numberOfOutputs = 1)
        {
            var rawTransactionRequest = new CreateRawTransactionRequest(new List<CreateRawTransactionInput>(numberOfInputs), new Dictionary<string, decimal>(numberOfOutputs));

            for (short i = 0; i < numberOfInputs; i++)
            {
                rawTransactionRequest.AddInput(new CreateRawTransactionInput
                {
                    TxId = "dummyTxId" + i.ToString(CultureInfo.InvariantCulture),
                    Vout = i
                });
            }

            for (short i = 0; i < numberOfOutputs; i++)
            {
                rawTransactionRequest.AddOutput(new CreateRawTransactionOutput
                {
                    Address = "dummyAddress" + i.ToString(CultureInfo.InvariantCulture),
                    Amount = i + 1
                });
            }

            return await GetTransactionFeeAsync(rawTransactionRequest, false, true);
        }

        public async Task<Dictionary<string, string>> GetMyPublicAndPrivateKeyPairsAsync()
        {
            const short secondsToUnlockTheWallet = 30;
            var keyPairs = new Dictionary<string, string>();
            await WalletPassphraseAsync(Parameters.WalletPassword, secondsToUnlockTheWallet);
            var myAddresses = await (this as ICoinService).ListReceivedByAddressAsync(0, true);

            foreach (var listReceivedByAddressResponse in myAddresses)
            {
                var validateAddressResponse = await ValidateAddressAsync(listReceivedByAddressResponse.Address);

                if (validateAddressResponse.IsMine && validateAddressResponse.IsValid && !validateAddressResponse.IsScript)
                {
                    var privateKey = await DumpPrivKeyAsync(listReceivedByAddressResponse.Address);
                    keyPairs.Add(validateAddressResponse.PubKey, privateKey);
                }
            }

            await WalletLockAsync();
            return keyPairs;
        }

        //  Note: As RPC's gettransaction works only for in-wallet transactions this had to be extended so it will work for every single transaction.
        public async Task<DecodeRawTransactionResponse> GetPublicTransactionAsync(string txId)
        {
            var rawTransaction = (await GetRawTransactionAsync(txId, 0)).Hex;
            return await DecodeRawTransactionAsync(rawTransaction);
        }

        [Obsolete("Please use EstimateFee() instead. You can however keep on using this method until the network fully adjusts to the new rules on fee calculation")]
        public async Task<decimal> GetTransactionFeeAsync(CreateRawTransactionRequest transaction, bool checkIfTransactionQualifiesForFreeRelay, bool enforceMinimumTransactionFeePolicy)
        {
            if (checkIfTransactionQualifiesForFreeRelay && await IsTransactionFreeAsync(transaction))
            {
                return 0;
            }

            decimal transactionSizeInBytes = GetTransactionSizeInBytesAsync(transaction);
            var transactionFee = ((transactionSizeInBytes / Parameters.FreeTransactionMaximumSizeInBytes) + (transactionSizeInBytes % Parameters.FreeTransactionMaximumSizeInBytes == 0 ? 0 : 1)) * Parameters.FeePerThousandBytesInCoins;

            if (transactionFee.GetNumberOfDecimalPlaces() > Parameters.CoinsPerBaseUnit.GetNumberOfDecimalPlaces())
            {
                transactionFee = Math.Round(transactionFee, Parameters.CoinsPerBaseUnit.GetNumberOfDecimalPlaces(), MidpointRounding.AwayFromZero);
            }

            if (enforceMinimumTransactionFeePolicy && Parameters.MinimumTransactionFeeInCoins != 0 && transactionFee < Parameters.MinimumTransactionFeeInCoins)
            {
                transactionFee = Parameters.MinimumTransactionFeeInCoins;
            }

            return transactionFee;
        }

        public async Task<GetRawTransactionResponse> GetRawTxFromImmutableTxIdAsync(string rigidTxId, int listTransactionsCount = int.MaxValue, int listTransactionsFrom = 0, bool getRawTransactionVersbose = true, bool rigidTxIdIsSha256 = false)
        {
            var allTransactions = await (this as ICoinService).ListTransactionsAsync("*", listTransactionsCount, listTransactionsFrom);

            foreach (var listTransactionsResponse in allTransactions)
            {
                if (rigidTxId == await GetImmutableTxIdAsync(listTransactionsResponse.TxId, rigidTxIdIsSha256))
                    return await GetRawTransactionAsync(listTransactionsResponse.TxId, getRawTransactionVersbose ? 1 : 0)
                ;
            }
            return null;
        }

        public async Task<decimal> GetTransactionPriorityAsync(CreateRawTransactionRequest transaction)
        {
            if (transaction.Inputs.Count == 0)
            {
                return 0;
            }

            var unspentInputs = (await (this as ICoinService).ListUnspentAsync(0)).ToList();
            var sumOfInputsValueInBaseUnitsMultipliedByTheirAge = transaction.Inputs.Select(input => unspentInputs.First(x => x.TxId == input.TxId)).Select(unspentResponse => (unspentResponse.Amount * Parameters.OneCoinInBaseUnits) * unspentResponse.Confirmations).Sum();
            return sumOfInputsValueInBaseUnitsMultipliedByTheirAge / GetTransactionSizeInBytesAsync(transaction);
        }

        public decimal GetTransactionPriorityAsync(IList<ListUnspentResponse> transactionInputs, int numberOfOutputs)
        {
            if (transactionInputs.Count == 0)
            {
                return 0;
            }

            return transactionInputs.Sum(input => input.Amount * Parameters.OneCoinInBaseUnits * input.Confirmations) / GetTransactionSizeInBytes(transactionInputs.Count, numberOfOutputs);
        }

        //  Note: Be careful when using GetTransactionSenderAddress() as it just gives you an address owned by someone who previously controlled the transaction's outputs
        //  which might not actually be the sender (e.g. for e-wallets) and who may not intend to receive anything there in the first place. 
        [Obsolete("Please don't use this method in production enviroment, it's for testing purposes only")]
        public async Task<string> GetTransactionSenderAddressAsync(string txId)
        {
            var rawTransaction = (await GetRawTransactionAsync(txId, 0)).Hex;
            var decodedRawTransaction = await DecodeRawTransactionAsync(rawTransaction);
            var transactionInputs = decodedRawTransaction.Vin;
            var rawTransactionHex = (await GetRawTransactionAsync(transactionInputs[0].TxId, 0)).Hex;
            var inputDecodedRawTransaction = await DecodeRawTransactionAsync(rawTransactionHex);
            var vouts = inputDecodedRawTransaction.Vout;
            return vouts[0].ScriptPubKey.Addresses[0];
        }

        public int GetTransactionSizeInBytesAsync(CreateRawTransactionRequest transaction)
        {
            return GetTransactionSizeInBytesAsync(transaction.Inputs.Count, transaction.Outputs.Count);
        }

        public int GetTransactionSizeInBytesAsync(int numberOfInputs, int numberOfOutputs)
        {
            return numberOfInputs * Parameters.TransactionSizeBytesContributedByEachInput
                   + numberOfOutputs * Parameters.TransactionSizeBytesContributedByEachOutput
                   + Parameters.TransactionSizeFixedExtraSizeInBytes
                   + numberOfInputs;
        }

        public async Task<bool> IsInWalletTransactionAsync(string txId)
        {
            //  Note: This might not be efficient if iterated, consider caching ListTransactions' results.
            return (await (this as ICoinService).ListTransactionsAsync(null, int.MaxValue)).Any(listTransactionsResponse => listTransactionsResponse.TxId == txId);
        }

        public async Task<bool> IsTransactionFreeAsync(CreateRawTransactionRequest transaction)
        {
            return transaction.Outputs.Any(x => x.Value < Parameters.FreeTransactionMinimumOutputAmountInCoins)
                   && GetTransactionSizeInBytes(transaction) < Parameters.FreeTransactionMaximumSizeInBytes
                   && (await GetTransactionPriorityAsync(transaction)) > Parameters.FreeTransactionMinimumPriority;
        }

        public bool IsTransactionFreeAsync(IList<ListUnspentResponse> transactionInputs, int numberOfOutputs, decimal minimumAmountAmongOutputs)
        {
            return minimumAmountAmongOutputs < Parameters.FreeTransactionMinimumOutputAmountInCoins
                   && GetTransactionSizeInBytes(transactionInputs.Count, numberOfOutputs) < Parameters.FreeTransactionMaximumSizeInBytes
                   && GetTransactionPriority(transactionInputs, numberOfOutputs) > Parameters.FreeTransactionMinimumPriority;
        }

        public async Task<bool> IsWalletEncryptedAsync()
        {
            return !(await HelpAsync(RpcMethods.walletlock.ToString())).Contains("unknown command");
        }
    }
}