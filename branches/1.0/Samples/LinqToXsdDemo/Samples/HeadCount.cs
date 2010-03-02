//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace LinqToXsdDemo.HeadCount.Typed
{
    using System.Linq;
    using www.example.com.Company;

    public static class Test
    {
        static int CountHeads(Company x)
        {
            return x.Query.Descendants<EmployeeType>().Count();
        }

        public static void Run()
        {
            var c = Company.Load("../../Data/Company.xml");
            var count = CountHeads(c);
            (count == 5).Require();
        }
    }
}


//
// A variation on the .Typed namespace.
// Be polymorphic in root type.
//
namespace LinqToXsdDemo.HeadCount.Generic
{
    using System.Linq;
    using Xml.Schema.Linq; // Needed for XTypedElement
    using www.example.com.Company;

    public static class Test
    {
        static int CountHeads(XTypedElement x)
        {
            return x.Query.Descendants<EmployeeType>().Count();
        }

        public static void Run()
        {
            var c = Company.Load("../../Data/Company.xml");
            var count = CountHeads(c);
            (count == 5).Require();
        }
    }
}

namespace LinqToXsdDemo.HeadCount.Untyped
{
    using System.Linq;
    using System.Xml.Linq;

    public static class Test
    {
        static int CountHeads(XElement x) 
        {
            XNamespace ns = "http://www.example.com/Company";
            return
                (from e in x.Descendants()
                 where e.Name== ns + "Manager" || e.Name== ns + "Employee"
                 select e
                ).Count();
        }
        
        public static void Run()
        {
            var x = XElement.Load("../../Data/Company.xml");
            var count = CountHeads(x);
            (count == 5).Require();
        }
    }
}
