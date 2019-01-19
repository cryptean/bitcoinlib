// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

namespace BitcoinLib.RPC.Specifications
{
    //  Note: Do not alter the capitalization of the enum members as they are being cast as-is to the RPC server
    public enum RpcMethods
    {
        //== Blockchain ==
        getbestblockhash,
        getblock,
        getblockchaininfo,
        getblockcount,
        getblockhash,
        getblockheader,
        getchaintips,
        getdifficulty,
        getmempoolinfo,
        getrawmempool,
        gettxout,
        gettxoutproof,
        gettxoutsetinfo,
        verifychain,
        verifytxoutproof,

        //== Control ==
        getinfo,
        help,
        stop,

        //== Generating ==
        generate,
        getgenerate,
        setgenerate,

        //== Mining ==
        getblocktemplate,
        getmininginfo,
        getnetworkhashps,
        prioritisetransaction,
        submitblock,

        //== Network ==
        addnode,
        clearbanned,
        disconnectnode,
        getaddednodeinfo,
        getconnectioncount,
        getnettotals,
        getnetworkinfo,
        getpeerinfo,
        listbanned,
        ping,
        setban,

        //== Rawtransactions ==
        createrawtransaction,
        decoderawtransaction,
        decodescript,
        fundrawtransaction,
        getrawtransaction,
        sendrawtransaction,
        signrawtransaction,
        signrawtransactionwithkey,
        signrawtransactionwithwallet,
        sighashtype,

        //== Util ==
        createmultisig,
        estimatefee,
        estimatepriority,
        estimatesmartfee,
        estimatesmartpriority,
        validateaddress,
        mirroraddress,
        verifymessage,

        //== Wallet ==
        abandontransaction,
        addmultisigaddress,
        addwitnessaddress,
        backupwallet,
        dumpprivkey,
        dumpwallet,
        getaccount,
        getaccountaddress,
        getaddressesbyaccount,
        getaddressesbylabel,
        getaddressinfo,
        getbalance,
        getnewaddress,
        getrawchangeaddress,
        getreceivedbyaccount,
        getreceivedbyaddress,
        getreceivedbylabel,
        gettransaction,
        getunconfirmedbalance,
        getwalletinfo,
        importaddress,
        importprivkey,
        importpubkey,
        importwallet,
        keypoolrefill,
        listaccounts,
        listaddressgroupings,
        listlabels,
        listlockunspent,
        listreceivedbyaccount,
        listreceivedbyaddress,
        listreceivedbylabel,
        listsinceblock,
        listtransactions,
        listmirrtransactions,
        listunspent,
        lockunspent,
        move,
        sendfrom,
        sendmany,
        sendtoaddress,
        setaccount,
        setlabel,
        settxfee,
        signmessage,
        walletlock,
        walletpassphrase,
        walletpassphrasechange,
				//2018-01-20: added Dash privatesend mixing support
				privatesend,
				//2018-03-02: added getaddressbalance (needs addressindex = 1 in dash.conf)
				getaddressbalance,
				//2018-07-23: Masternode support, usually list command is used
				masternode
    }
}