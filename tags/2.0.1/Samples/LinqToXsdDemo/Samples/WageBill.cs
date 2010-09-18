//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace LinqToXsdDemo.WageBill.Typed
{
    using System.Linq;
    using Xml.Schema.Linq;
    using www.example.com.Company;

    public static class Test
    {
        static double SumUpSalaries(Company c) 
        {
            return 
                (from e in c.Query.Descendants<EmployeeType>()
                 select e.Salary
                ).Sum();
        }

        public static void Run()
        {
            var c = Company.Load("../../Data/Company.xml");
            var sum = SumUpSalaries(c);
            (sum == 27370).Require();
        }
    }
}


namespace LinqToXsdDemo.WageBill.Untyped
{
    using System.Linq;
    using System.Xml.Linq;

    public static class Test
    {
        static double SumUpSalaries(XElement x) 
        {
            XNamespace ns = "http://www.example.com/Company";
            return 
                (from s in x.Descendants(ns + "Salary")
                 select (double)s
                ).Sum();
        }
        
        public static void Run()
        {
            var x = XElement.Load("../../Data/Company.xml");
            var sum = SumUpSalaries(x);
            (sum == 27370).Require();
        }
    }
}
