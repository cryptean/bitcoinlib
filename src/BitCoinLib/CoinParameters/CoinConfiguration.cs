using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace BitCoinLib.CoinParameters
{
    public class CoinConfiguration
    {
        public static List<CoinSetting> CoinSettings { get; } = new List<CoinSetting>();
        public static void LoadConfigFile(string jsonFile)
        {
            var config = new ConfigurationBuilder().AddJsonFile(jsonFile).Build();
            var coinSettings = config.GetSection("coinSettings");
            var items = coinSettings.GetChildren();

            foreach (var item in items)
            {
                short timeout = 0;
                if (!short.TryParse(item.GetSection("rpcTimeoutSeconds").Value, out timeout))
                    timeout = 10;

                var setting = new CoinSetting()
                {
                    Name = item.GetSection("name").Value,
                    DaemonUrl = item.GetSection("daemonUrl").Value,
                    RpcUser = item.GetSection("rpcUser").Value,
                    RpcPassword = item.GetSection("rpcPassword").Value,
                    WalletPassword = item.GetSection("walletPassword").Value,
                    RpcTimeoutSeconds = timeout,
                };

                #region Invalid configuration / Missing parameters

                if (setting.RpcTimeoutSeconds <= 0)
                {
                    throw new Exception("RpcRequestTimeoutInSeconds must be greater than zero");
                }

                if (string.IsNullOrWhiteSpace(setting.DaemonUrl)
                    || string.IsNullOrWhiteSpace(setting.RpcUser)
                    || string.IsNullOrWhiteSpace(setting.RpcPassword))
                {
                    throw new Exception($"One or more required parameters, as defined in CoinSetting, were not found in the configuration file!");
                }

                #endregion

                CoinSettings.Add(setting);
            }
        }
    }
    public class CoinSetting
    {
        public string Name { get; set; }
        public string DaemonUrl { get; set; }
        public string RpcUser { get; set; }
        public string RpcPassword { get; set; }
        public string WalletPassword { get; set; }
        public short RpcTimeoutSeconds { get; set; }
    }
}