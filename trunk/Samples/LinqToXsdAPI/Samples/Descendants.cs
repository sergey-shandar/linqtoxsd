//Copyright (c) Microsoft Corporation.  All rights reserved.

///////////////////////////////////////////////////////////////////////////////
// Test type-driven descendant axis
// List all employees with their names and salaries
///////////////////////////////////////////////////////////////////////////////

namespace LinqToXsdAPI.Descendants
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
            var result = new XElement("Salaries",
                            from e in c.Query.Descendants<EmployeeType>()
                            select new XElement("Employee",
                                        new XElement("Name", e.Name),
                                        new XElement("Salary",e.Salary)));
            result.CompareWithBaseline("Descendants.xml");           
        }
    }
}
