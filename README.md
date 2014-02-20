BitcoinLib
==========

**C# Bitcoin Qt RPC Library / Wrapper for .Net 4.5+ projects**

Features
--------

- Fully compatible with QT's 0.8.6 RPC API.
- Design-by-contract, service-oriented architecture.
- Strongly-typed structures for complex RPC requests and responses.
- Implicit JSON casting for all RPC messages.
- Extended methods for every-day scenarios where the built-in methods fall short.
- Exposure of all Qt API's functionality as well as the extended methods through a single interface.
- Console and web test clients out of the box with demo methods implemented in them.
- Testnet ready.
- Fully configurable.

Instructions
------------

- Locate your `bitcoin.conf` file (in Windows it's under: `%AppData%\Roaming\Bitcoin`, if it's not there just go ahead and create it) and add these lines:
	- rpcuser = myRpcUsername
	- rpcpassword = myRpcPassword
	- daemon=1
	- txindex=1

- Call `bitcoind -reindex -txindex -debugnet -printtoconsole` and wait until it's finished reindexing (it might take a while). Append `-testnet` if you want to run it for Testnet. You need to do this just once.

- Shut down bitcoind and launch it again with these arguments: `bitcoind -daemon -debugnet -printtoconsole`. Append `-testnet` if you want to run it for Testnet. Wait until it is fully synchronized. 

- Edit the .config files in the solution to best fit your needs
	- `app.config` for the Console test client
	- `web.config` for the Web test client

  Make sure you also update the `bitcoin.conf` when you alter the `RpcUser` and `RpcPassword` parameters.

- You're good to go.

License
-------

BitcoinLib is released under the terms of the GPLv3 license. See [LICENSE](LICENSE) for more information or see http://opensource.org/licenses/GPL-3.0.

Support
-------

Please use GitHub's `Issues`

Donations
---------

BTC: 17GDskinpoPb4k4Xaaf9R9VpXeXhdzBvd9

Influenced by
-------------

- Bitnet, by Konstantin Ineshin (http://bitnet.sourceforge.net)
- BitcoinRpcSharp, by BitKoot (https://github.com/BitKoot/BitcoinRpcSharp)
- Bitcoin-wrapper, by Lars Holdgaard (https://github.com/LarsHoldgaard/bitcoin-wrapper)
