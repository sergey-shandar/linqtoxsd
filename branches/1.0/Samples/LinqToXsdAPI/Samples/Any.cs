//Copyright (c) Microsoft Corporation.  All rights reserved.

///////////////////////////////////////////////////////////////////////////////
// Test wildcards
///////////////////////////////////////////////////////////////////////////////

namespace LinqToXsdAPI.Any
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using Xml.Schema.Linq;
    using LinqToXsdSamples.Schemas.Wild;

    public static class Test
    {
        public static void Run()
        {
            var p = Product.Load("../../Data/Wild.xml");
            var w = p.Any.First();
            w.CompareWithBaseline("Any.xml");
        }
    }
}
