BitcoinLib
==========

**C# Bitcoin, Litecoin and Bitcoin-Clones Library & RPC Wrapper**

Features
--------

- Fully compatible and up-to-date with Bitcoin 0.9.3 RPC API.
- Design-by-contract, service-oriented architecture.
- Strongly-typed structures for complex RPC requests and responses.
- Implicit JSON casting for all RPC messages.
- Extended methods for every-day scenarios where the built-in methods fall short.
- Exposure of all RPC API's functionality as well as the extended methods through a single interface.
- Fallback mechanism for timed-out RPC requests.
- Custom RPC exceptions.
- Supports all Bitcoin clones.
- Can operate on unlimited daemons with a single library reference.
- Litecoin integration included.
- Each coin instance can be fully parametrized at run-time and implement its own constants.
- Console test client with demo methods implemented in it.
- Disconnected raw RPC connector included for quick'n'dirty debugging.
- Testnet ready.
- Available at NuGet: https://www.nuget.org/packages/BitcoinLib/
- Mono compatible.
- Blockchain.info compatible.
- Fully configurable.


Instructions for Bitcoin
------------------------

- Locate your `bitcoin.conf` file (in Windows it's under: `%AppData%\Roaming\Bitcoin`, if it's not there just go ahead and create it) and add these lines:
	- rpcuser = MyRpcUsername
	- rpcpassword = MyRpcPassword
	- server=1
	- txindex=1

- Call `bitcoind -reindex -txindex -debug=net -printtoconsole` and wait until it's finished re-indexing (it might take a while). Append `-testnet` if you want to run it for Testnet. You need to do this just once.

- Shut down bitcoind and run it again with these arguments: `bitcoind -daemon -debug=net -printtoconsole`. Append `-testnet` if you want to run it for Testnet. Wait until it is fully synchronized. 

- Edit the `app.config` file in the Console test client to best fit your needs. Make sure you also update the `bitcoin.conf` when you alter the `Bitcoin_RpcUsername` and `Bitcoin_RpcPassword` parameters.

- You're good to go.


Instructions for Litecoin and other Bitcoin clones
--------------------------------------------------

- Perform the same steps as those mentioned above for Bitcoin.

- Litecoin configuration file is: `litecoin.conf` under: `%AppData%\Roaming\Litecoin` and its daemon is: `litecoind`.

- Each coin can be initialized by its own interface specification:
	- `IBitcoinService BitcoinService = new BitcoinService();` 
	- `ILitecoinService LitecoinService = new LitecoinService();` 

- Any bitcoin clone can be adopted without any further installation steps with the use of the generic `ICryptocoinService`:
	- `ICryptocoinService cryptocoinService = new CryptocoinService("daemonUrl", "rpcUsername", "rpcPassword", "walletPassword");`

- Use `(ICryptocoinService).Parameters` to fully configure each coin pointer at run-time. 


Configuration
-------------

Sample configuration:

	﻿<?xml version="1.0" encoding="utf-8"?>
	<configuration>
		<appSettings>

			<!-- BitcoinLib settings start -->

				<!-- Shared RPC settings start -->
				<add key="RpcRequestTimeoutInSeconds" value="10" />
				<add key="RpcResendTimedOutRequests" value="true" />
				<add key="RpcTimedOutRequestsResendAttempts" value="5" />
				<add key="RpcDelayResendingTimedOutRequests" value="true" />
				<add key="RpcUseBase2ExponentialDelaysWhenResendingTimedOutRequests" value="true" />
				<add key="RpcUnlockWalletImplicitlyWhenRequired" value="true" />
				<!-- Shared RPC settings end -->

				<!-- Bitcoin settings start -->
				<add key="Bitcoin_DaemonUrl" value="http://localhost:8332" />
				<add key="Bitcoin_DaemonUrl_Testnet" value="http://localhost:18332" />
				<add key="Bitcoin_WalletPassword" value="MyWalletPassword" />
				<add key="Bitcoin_RpcUsername" value="MyRpcUsername" />
				<add key="Bitcoin_RpcPassword" value="MyRpcPassword" />
				<!-- Bitcoin settings end -->

				<!-- Litecoin settings start -->
				<add key="Litecoin_DaemonUrl" value="http://localhost:9332" />
				<add key="Litecoin_DaemonUrl_Testnet" value="http://localhost:19332" />
				<add key="Litecoin_WalletPassword" value="MyWalletPassword" />
				<add key="Litecoin_RpcUsername" value="MyRpcUsername" />
				<add key="Litecoin_RpcPassword" value="MyRpcPassword" />
				<!-- Litecoin settings end -->

			<!-- BitcoinLib settings end -->
			
		</appSettings>
	</configuration>


Web Test Client
---------------
The web test client is not maintained anymore. The latest operating version can be found [here](https://github.com/GeorgeKimionis/BitcoinLib-TestClient-Web).


License
-------

BitcoinLib is released under the terms of the GPLv3 license. See [LICENSE](LICENSE) for more information or see http://opensource.org/licenses/GPL-3.0.


Support
-------

Please use GitHub's `Issues`. Premium support is also available [here](mailto:mcaizgk2+bitcoinlib@gmail.com).


Pull Requests
-------------

Please contact [the repository owner](mailto:mcaizgk2+bitcoinlib@gmail.com) via e-mail before submitting your first pull request.


Donations
---------

If you use BitcoinLib for a commercial project you are expected to make a donation. BTC: [1GeorgeKZn9SaTmGsuRQRasvoJS3YWzQ5a](https://blockchain.info/address/1GeorgeKZn9SaTmGsuRQRasvoJS3YWzQ5a)


Influenced by
-------------

- [Bitnet](http://bitnet.sourceforge.net), by Konstantin Ineshin
- [BitcoinRpcSharp](https://github.com/BitKoot/BitcoinRpcSharp), by BitKoot 
- [Bitcoin-wrapper](https://github.com/LarsHoldgaard/bitcoin-wrapper), by Lars Holdgaard 


Credits
-------

Thanks to everyone who contributed to making this library better: [@OperatorOverload](https://github.com/OperatorOverload), Angelos Leoussis, everyone who reported any issues, et al.
