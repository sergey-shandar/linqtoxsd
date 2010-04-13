using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace x
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new example.Property();
            p.E = new decimal[] { 3, 4 };
            p.A = new decimal[] { 1, 2, 3 };
        }
    }
}
