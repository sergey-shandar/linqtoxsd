//Copyright (c) Microsoft Corporation.  All rights reserved.

///////////////////////////////////////////////////////////////////////////////
// Test type-driven ancestor axis
///////////////////////////////////////////////////////////////////////////////

namespace LinqToXsdAPI.Ancestors
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

            // Look up employee "Karl"
            var karl = 
                (from e in c.Query.Descendants<EmployeeType>()
                 where e.Name == "Karl"
                 select e).First();

            // Create a list of Karl's bosses
            var bosses =
                new XElement("Bosses",
                    from d in karl.Query.Ancestors<Department>()
                    where d.Manager != null
                    select new XElement("Boss",d.Manager.Name));

            bosses.CompareWithBaseline("Ancestors.xml");
        }
    }
}
