// Copyright (c) 2014 - 2016 George Kimionis
// See the accompanying file LICENSE for the Software License Aggrement

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