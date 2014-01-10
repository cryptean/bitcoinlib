namespace BitcoinLib.RPC
{
    //  Note: Do not alter the capitalization of the enum members as they are being cast as-is to the RPC server
    //  Note for parameters: <>:required, []:optional, [=x]:optional with default value
    public enum RpcMethods
    {
        addmultisigaddress,         // <nrequired> <'["key","key"]'> [account]
        addnode,                    // <node> <add|remove|onetry>
        backupwallet,               // <destination>
        createmultisig,             // <nrequired> <'["key","key"]'>
        createrawtransaction,       // [{"txid":txid,"vout":n},...] {address:amount,...}
        decoderawtransaction,       // <hex string>
        dumpprivkey,                // <bitcoinaddress>
        encryptwallet,              //  <passphrase>  
        getaccount,                 // <bitcoinaddress>
        getaccountaddress,          // <account>
        getaddednodeinfo,           // <dns> [node]
        getaddressesbyaccount,      // <account>
        getbalance,                 // [account] [minconf=1]
        getbestblockhash,           // (!) --> pre-release
        getblock,                   // <hash>
        getblockcount,
        getblockhash,               // <index>
        getblocktemplate,           // [params]
        getconnectioncount,
        getdifficulty,
        getgenerate,
        gethashespersec,
        getinfo,
        getmininginfo,
        getnewaddress,              // [account]
        getpeerinfo,
        getrawchangeaddress,        // [account]    (!) --> pre-release
        getrawmempool,
        getrawtransaction,          // <txid> [verbose=0]
        getreceivedbyaccount,       // <account> [minconf=1]
        getreceivedbyaddress,       // <bitcoinaddress> [minconf=1]
        gettransaction,             // <txid>
        gettxout,                   // <txid> <n> [includemempool=true]
        gettxoutsetinfo,
        getwork,                    // [data]
        help,                       // [command]
        importprivkey,              // <bitcoinprivkey> [label] [rescan=true]
        keypoolrefill,
        listaccounts,               // [minconf=1]
        listaddressgroupings,
        listlockunspent,
        listreceivedbyaccount,      // [minconf=1] [includeempty=false]
        listreceivedbyaddress,      // [minconf=1] [includeempty=false]
        listsinceblock,             // [blockhash] [target-confirmations]
        listtransactions,           // [account] [count=10] [from=0]
        listunspent,                // [minconf=1] [maxconf=9999999] ["address",...]
        lockunspent,                // unlock? [array-of-Objects]
        move,                       // <fromaccount> <toaccount> <amount> [minconf=1] [comment]
        sendfrom,                   // <fromaccount> <tobitcoinaddress> <amount> [minconf=1] [comment] [comment-to]
        sendmany,                   // <fromaccount> {address:amount,...} [minconf=1] [comment]
        sendrawtransaction,         // <hex string>
        sendtoaddress,              // <bitcoinaddress> <amount> [comment] [comment-to]
        setaccount,                 // <bitcoinaddress> <account>
        setgenerate,                // <generate> [genproclimit]
        settxfee,                   // <amount>
        signmessage,                // <bitcoinaddress> <message>
        signrawtransaction,         // <hex string> [{"txid":txid,"vout":n,"scriptPubKey":hex,"redeemScript":hex},...] [<privatekey1>,...] [sighashtype="ALL"]
        stop,
        submitblock,                // <hex data> [optional-params-obj]
        validateaddress,            // <bitcoinaddress>
        verifymessage,              // <bitcoinaddress> <signature> <message>
        walletlock,
        walletpassphrase,           // <passphrase> <timeout>
        walletpassphrasechange,     // <oldpassphrase> <newpassphrase>
    }
}
