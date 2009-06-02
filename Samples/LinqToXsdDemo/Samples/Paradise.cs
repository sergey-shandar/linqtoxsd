//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace LinqToXsdDemo.Paradise.Typed
{
    using Xml.Schema.Linq;
    using www.example.com.Company;

    public static class Test
    {
        static void IncreaseSalary(Company c, double factor)
        {
            foreach(var e in c.Query.Descendants<EmployeeType>())
                e.Salary *= factor;
        }

        public static void Run()
        {
            var c = Company.Load("../../Data/Company.xml");
            IncreaseSalary(c,2);
            c.CompareWithBaseline("Paradise");
        }
    }
}


namespace LinqToXsdDemo.Paradise.Untyped
{
    using System.Xml.Linq;

    public static class Test
    {

        static void IncreaseSalary(XElement x, double factor) 
        {
            XNamespace ns = "http://www.example.com/Company";
            foreach(var s in x.Descendants(ns + "Salary"))
                s.SetValue((double)s * factor);
        }
        
        public static void Run()
        {
            var x = XElement.Load("../../Data/Company.xml");
            IncreaseSalary(x,2);
            x.CompareWithBaseline("Paradise");
        }
    }
}
