//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace LinqToXsdDemo.Reorganization.Typed
{
    using System.Linq;
    using www.example.com.Company;

    public static class Test
    {
        static void PullUpDepartment(Department dep)
        {
            // Locate department one level up
            var depAbove =
                dep.
                Query.
                Ancestors<Department>().
                First();

            //
            // Disconnect original department
            // In fact, remove the member; cf. ".Parent".
            //
            dep.Untyped.Parent.Remove();

            //
            // Move employees one department up.
            // Note: the manager does not move.
            //
            foreach (var m in dep.Member.ToList())
            {
                m.Untyped.Remove();
                depAbove.Member.Add(m);
            }
        }

        public static void Run()
        {
            var c = Company.Load("../../Data/Company.xml");

            // Look up employee by name
            var emp = 
                c.
                Query.
                Descendants<EmployeeType>().
                Where(e => e.Name == "Douglas").
                First();

            // Locate immediate department of employee
            var depEmp =
                emp.
                Query.
                Ancestors<Department>().
                First();

            PullUpDepartment(depEmp);
            c.CompareWithBaseline("Reorg");
        }
    }
}
