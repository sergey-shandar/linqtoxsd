//Copyright (c) Microsoft Corporation.  All rights reserved.

//
// Two different ways of mixing typed and untyped APIs
//

namespace LinqToXsdDemo.Reports.Mix1
{
    using System.Linq;
    using System.Xml.Linq;
    using Xml.Schema.Linq;
    using www.example.com.Company;

    public static class Test
    {
        static XElement CountReports(Company c)
        {
            XNamespace ns = "http://www.example.com/Company";
            return new XElement("Reports",
              from e in c.Query.Descendants<EmployeeType>()
              where e.Untyped.Name == ns + "Manager"
              select new XElement("Report",
                new XAttribute("Name",e.Name), 
                (from r in ((Department)(e.Untyped.Parent))
                             .Query.Descendants<EmployeeType>()           
                 where r != e
                 select r
                ).Count()));
        }

        public static void Run()
        {
            var c = Company.Load("../../Data/Company.xml");
            var counts = CountReports(c);
            counts.CompareWithBaseline("Reports");
        }
    }
}

namespace LinqToXsdDemo.Reports.Mix2
{
    using System.Linq;
    using System.Xml.Linq;
    using Xml.Schema.Linq;
    using www.example.com.Company;

    public static class Test
    {
        static XElement CountReports(Company x)
        {
            XNamespace ns = "http://www.example.com/Company";
            return new XElement("Reports",
              from euntyped in x.Untyped.Descendants(ns + "Manager")
              let etyped = (EmployeeType)euntyped
              select new XElement("Report",
                new XAttribute("Name",etyped.Name), 
                (from r in ((Department)(etyped.Untyped.Parent))
                                 .Query.Descendants<EmployeeType>()           
                 where r != etyped
                 select r
                ).Count()));
        }

        public static void Run()
        {
            var c = Company.Load("../../Data/Company.xml");
            //
            // The following descendants compensates for an open bug.
            // That is, by a typed descendants we make sure that the tree gets typed.
            // As a result, casts on untyped subtrees will be correct.
            //
            c.Query.Descendants<EmployeeType>().ToList();
            var counts = CountReports(c);
            counts.CompareWithBaseline("Reports");
        }
    }
}
