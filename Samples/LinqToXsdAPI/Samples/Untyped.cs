//Copyright (c) Microsoft Corporation.  All rights reserved.

///////////////////////////////////////////////////////////////////////////////
// Test XElement property on XTypedElement
// Use untyped descendants to locate managers.
// Cast these managers back to typed world to access their name.
///////////////////////////////////////////////////////////////////////////////

namespace LinqToXsdAPI.XElementProperty
{
    using System;
    using System.Linq;
    using Xml.Schema.Linq;
    using System.Xml.Linq;
    using www.example.com.Company;

    public static class Test
    {
        public static void Run()
        {
            XNamespace ns = "http://www.example.com/Company";
            var c = Company.Load("../../Data/Company.xml");
            var result = new XElement("Managers",
                            from m in c.Untyped.Descendants(ns + "Manager")
                            select new XElement("Manager",((EmployeeType)m).Name));
            result.CompareWithBaseline("Untyped.xml");
        }    
    }
}
