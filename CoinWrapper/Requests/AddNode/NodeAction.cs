// Copyright (c) 2014 George Kimionis
// Distributed under the GPLv3 software license, see the accompanying file LICENSE or http://opensource.org/licenses/GPL-3.0

namespace BitcoinLib.Requests.AddNode
{
    //  Note: Do not alter the capitalization of the enum members as they are being cast as-is to the RPC server
    public enum NodeAction
    {
        add,
        remove,
        onetry
    }
}