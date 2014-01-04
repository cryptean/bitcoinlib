using System;
using System.Collections.Generic;
using BitcoinWrapper.Auxiliary;
using BitcoinWrapper.Enums;
using BitcoinWrapper.RPC;
using BitcoinWrapper.Requests.CreateRawTransaction;
using BitcoinWrapper.Requests.SignRawTransaction;
using BitcoinWrapper.Responses;

namespace BitcoinWrapper.Services
{
    //   Implementation of API calls list, as found at: https://en.bitcoin.it/wiki/Original_Bitcoin_client/API_Calls_list (note: this list is often out-of-date)
    public sealed class RpcService : IRpcService
    {
        private readonly IRpcConnector _rpcConnector;

        public RpcService()
        {
            _rpcConnector = new RpcConnector();
        }

        public String AddMultiSigAddress(Int32 nRquired, List<String> publicKeys, String account)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.addmultisigaddress, nRquired, publicKeys, account);
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

        public String CreateRawTransaction(List<CreateRawTransactionInput> inputs, Dictionary<String, Decimal> outputs)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.createrawtransaction, inputs, outputs);
        }

        public DecodeRawTransactionResponse DecodeRawTransaction(String rawTransactionHexString)
        {
            return _rpcConnector.MakeRequest<DecodeRawTransactionResponse>(RpcMethods.decoderawtransaction, rawTransactionHexString);
        }

        public String DumpPrivKey(String bitcoinAddress)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.dumpprivkey, bitcoinAddress);
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

        public String GetAddressesByAccount(String account)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.getaddressesbyaccount, account);
        }

        public Decimal GetBalance(String account, Int32 minConf)
        {
            return String.IsNullOrWhiteSpace(account)
               ? _rpcConnector.MakeRequest<Decimal>(RpcMethods.getbalance)
               : _rpcConnector.MakeRequest<Decimal>(RpcMethods.getbalance, account, minConf);
        }

        public GetBlockResponse GetBlock(String hash)
        {
            return _rpcConnector.MakeRequest<GetBlockResponse>(RpcMethods.getblock, hash);
        }

        public Int32 GetBlockCount()
        {
            return _rpcConnector.MakeRequest<Int32>(RpcMethods.getblockcount);
        }

        public String GetBlockHash(Int32 index)
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

        public GetInfoResponse GetInfo()
        {
            return _rpcConnector.MakeRequest<GetInfoResponse>(RpcMethods.getinfo);
        }

        public GetMiningInfoResponse GetMiningInfo()
        {
            return _rpcConnector.MakeRequest<GetMiningInfoResponse>(RpcMethods.getmininginfo);
        }

        public String GetNewAddress(String account)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.getnewaddress, account);
        }

        public GetPeerInfoResponse GetPeerInfo()
        {
            return _rpcConnector.MakeRequest<GetPeerInfoResponse>(RpcMethods.getpeerinfo);
        }

        public String GetRawMemPool()
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.getrawmempool);
        }

        public String GetRawTransaction(String txId, Int32 verbose)
        {
            if (txId == Constants.GenesisBlock)
            {
                throw new Exception("GetRawTransaction does not work with the Genesis block coinbase transaction as it is not a valid, spendable transaction");
            }
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

        public String KeyPoolRefill()
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.keypoolrefill);
        }

        public String ListAccounts(Int32 minConf)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.listaccounts, minConf);
        }

        public List<List<ListAddressGroupingsResponse>> ListAddressGroupings()
        {
            return _rpcConnector.MakeRequest<List<List<ListAddressGroupingsResponse>>>(RpcMethods.listaddressgroupings);
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

        public List<ListUnspentResponse> ListUnspent(Int32 minConf, Int32 maxConf, List<String> addreses)
        {
            return _rpcConnector.MakeRequest<List<ListUnspentResponse>>(RpcMethods.listunspent, minConf, maxConf, (addreses ?? new List<string>()));
        }

        public String Move(String fromAccount, String toAccount, Decimal amount, Int32 minConf, String comment)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.move, fromAccount, toAccount, amount, minConf, comment);
        }

        public String SendFrom(String fromAccount, String toBitcoinAddress, Decimal amount, Int32 minConf, String comment, String commentTo)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.sendfrom, fromAccount, toBitcoinAddress, amount, minConf, comment, commentTo);
        }

        public String SendMany(String fromAccount, Dictionary<String, Decimal> toBitcoinAddress, Int32 minConf, String comment)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.sendmany, fromAccount, toBitcoinAddress, minConf, comment);
        }

        public String SendRawTransaction(String rawTransactionHexString)
        {
            return _rpcConnector.MakeRequest<String>(RpcMethods.sendrawtransaction, rawTransactionHexString);
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

        public SignRawTransactionResponse SignRawTransaction(SignRawTransactionRequest rawTransaction)
        {
            return _rpcConnector.MakeRequest<SignRawTransactionResponse>(RpcMethods.signrawtransaction, rawTransaction);
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
    }
}
