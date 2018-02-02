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
(https://bitcoin.org/en/developer-examples#regtest-mode) ready.
- Fully configurable.

Support
-------

Please use GitHub's `Issues` to report any issues.

Instructions
------------

- Locate your `smartcash.conf` file (in Windows it's under: `%AppData%\Roaming\Smartcash`, if it's not there just go ahead and create it) and add these lines:
	- rpcuser=MyRpcUsername
	- rpcpassword=MyRpcPassword
	- server=1
	- txindex=1

- Go to yout project and edit the `app.config`.

Configuration
-------------

Sample configuration in `app.config`:

	﻿<?xml version="1.0" encoding="utf-8"?>
	<configuration>
		<appSettings>

			<!-- Smartcash settings start -->

				<!-- Shared RPC settings start -->
				<add key="RpcRequestTimeoutInSeconds" value="10" />
				<!-- Shared RPC settings end -->

				<!-- Smartcash settings start -->
				<add key="Smartcash_DaemonUrl" value="http://localhost:8332" />
				<add key="Smartcash_WalletPassword" value="MyWalletPassword" />
				<add key="Smartcash_RpcUsername" value="MyRpcUsername" />
				<add key="Smartcash_RpcPassword" value="MyRpcPassword" />
				<!-- Smartcash settings end -->

			<!-- Smartcash settings end -->
			
		</appSettings>
	</configuration>

License
-------

See [LICENSE](LICENSE).
