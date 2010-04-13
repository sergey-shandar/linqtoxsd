//Copyright (c) Microsoft Corporation.  All rights reserved.

///////////////////////////////////////////////////////////////////////////////
// Test Parent property on XTypedElement
///////////////////////////////////////////////////////////////////////////////

namespace LinqToXsdAPI.Parent
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using Xml.Schema.Linq;
    using www.example.com.Company;

    public static class Test
    {
        public static void Run()
        {
            var c = Company.Load("../../Data/Company.xml");
            ((Company)c.Department[0].Untyped.Parent == c).Require();
        }
    }
}
