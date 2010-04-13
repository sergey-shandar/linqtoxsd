//Copyright (c) Microsoft Corporation.  All rights reserved.

///////////////////////////////////////////////////////////////////////////////
// Test append semantics of DML
///////////////////////////////////////////////////////////////////////////////

namespace LinqToXsdAPI.Append.Untyped
{
    using System.Xml.Linq;

    public static class Test
    {
        public static void Run()
        {
            XNamespace ns = "http://LinqToXsdSamples/Schemas/Tricky";
            XElement t =
                new XElement(ns + "tricky",
                    new XElement(ns + "a", "1"),
                    new XElement(ns + "b", "2"),
                    new XElement(ns + "c", "3"),
                    new XElement(ns + "c", "4"),
                    new XElement(ns + "b", "5"),
                    new XElement(ns + "c", "6"));
            t.CompareWithBaseline("Append.xml");
        }
    }
}

namespace LinqToXsdAPI.Append.Typed
{
    using LinqToXsdSamples.Schemas.Tricky;
    public static class Test
    {
        public static void Run()
        {
            tricky t = new tricky();
            t.a = "1";
            t.b.Add("2");
            t.c.Add("3");
            t.c.Add("4");
            t.b.Add("5");
            t.c.Add("6");
            t.CompareWithBaseline("Append.xml");
        }
    }
}
