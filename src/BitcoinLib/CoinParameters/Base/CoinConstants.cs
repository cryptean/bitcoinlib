// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

using System;

namespace BitcoinLib.CoinParameters.Base
{
    public abstract class CoinConstants<T> where T : CoinConstants<T>, new()
    {
        private static readonly Lazy<T> Lazy = new Lazy<T>(() => new T());
        public static T Instance => Lazy.Value;
    }
}