SmartcashLib
==========

**.NET Smartcash library**

Features
--------

- Fully compatible and up-to-date with [Smartcash Core 1.1.1](https://github.com/SmartCash/smartcash) RPC API.
- Strongly-typed structures for complex RPC requests and responses.
- Implicit JSON casting for all RPC messages.
- Extended methods for every-day scenarios where the built-in methods fall short.
- Exposure of all [RPC API's functionality](https://en.bitcoin.it/wiki/Original_Bitcoin_client/API_calls_list) as well as the extended methods through a single interface.
- Custom RPC exceptions.
- Demo client included.
- Disconnected raw RPC connector included for quick'n'dirty debugging.
- Handles and relays RPC internal server errors along with their error code.
- Can work without a `.config` file.
- Fully compatible with [Mono](http://www.mono-project.com/).
- [Test Network (testnet)](https://bitcoin.org/en/developer-examples#testnet) and [Regression Test Mode (regtest)](https://bitcoin.org/en/developer-examples#regtest-mode) ready.
- Fully configurable.

Support
-------

Please use GitHub's `Issues` to report any issues. For Premium Support and other inquiries please contact: [rc125@protonmail.com](mailto:rc125@protonmail.com).

License
-------

See [LICENSE](LICENSE).

Versioning
-------

SmartcashLib follows [Semantic Versioning 2.0.0](http://semver.org/spec/v2.0.0.html).


Instructions
------------

- Locate your `smartcash.conf` file (in Windows it's under: `%AppData%\Roaming\Smartcash`, if it's not there just go ahead and create it) and add these lines:
	- rpcuser=MyRpcUsername
	- rpcpassword=MyRpcPassword
	- server=1
	- txindex=1

- Edit the `app.config` file in the Console test client to best fit your needs. Make sure you also update the `smartcash.conf` file when you alter the `Smartcash_RpcUsername` and `Smartcash_RpcPassword` parameters.

Configuration
-------------

Sample configuration:

	﻿<?xml version="1.0" encoding="utf-8"?>
	<configuration>
		<appSettings>

			<!-- Smartcash settings start -->

				<!-- Shared RPC settings start -->
				<add key="RpcRequestTimeoutInSeconds" value="10" />
				<!-- Shared RPC settings end -->

				<!-- Smartcash settings start -->
				<add key="Smartcash_DaemonUrl" value="http://localhost:8332" />
				<add key="Smartcash_DaemonUrl_Testnet" value="http://localhost:18332" />
				<add key="Smartcash_WalletPassword" value="MyWalletPassword" />
				<add key="Bitcoin_RpcUsername" value="MyRpcUsername" />
				<add key="Bitcoin_RpcPassword" value="MyRpcPassword" />
				<!-- Smartcash settings end -->

			<!-- Smartcash settings end -->
			
		</appSettings>
	</configuration>

