//Copyright (c) Microsoft Corporation.  All rights reserved.

///////////////////////////////////////////////////////////////////////////////
// Test Cast from XElement to XTypedElement
///////////////////////////////////////////////////////////////////////////////

namespace LinqToXsdAPI.Cast
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using www.example.com.Company;

    public static class Test
    {
        public static void Run()
        {
            // Untyped load followed by cast
            XElement xe = XElement.Load("../../Data/Company.xml");
            var c1 = (Company)xe;
            (c1 != null).Require();

            // Typed load right from the start
            var c2 = Company.Load("../../Data/Company.xml");

            // Cast recalls existing typed view
            var c3 = (Company)c2.Untyped;
            c2.Equals(c3).Require();

        }
    }
}
