// Copyright (c) 2014 George Kimionis & Shaun Barratt
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

using System;
using BitcoinLib.CoinParameters.Base;

namespace BitcoinLib.CoinParameters.Dogecoin
{
	public static class DogecoinConstants
	{
		public sealed class Constants : CoinConstants<Constants>
		{
			public readonly UInt16 CoinReleaseHalfsEveryXInYears = 4;
			public readonly UInt16 DifficultyIncreasesEveryXInBlocks = 2016;
			public readonly UInt32 OneDogecoinInKoinus = 100000000;
			public readonly Decimal OneKoinuInXDG = 0.00000001M;
			public readonly Decimal OneMicrocoinInXDG = 0.000001M;
			public readonly Decimal OneMillicoinInXDG = 0.001M;
			public readonly String Symbol = "Ð";
		}
	}
}