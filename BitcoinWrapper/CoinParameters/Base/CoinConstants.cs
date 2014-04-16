// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;

namespace BitcoinLib.CoinParameters.Base
{
    public abstract class CoinConstants<T> where T : CoinConstants<T>, new()
    {
        private static readonly Lazy<T> Lazy = new Lazy<T>(() => new T());

        public static T Instance
        {
            get
            {
                return Lazy.Value;
            }
        }
    }
}