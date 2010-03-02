//Copyright (c) Microsoft Corporation.  All rights reserved.

///////////////////////////////////////////////////////////////////////////////
// Access to properties that corresponding recurring element names and choices
///////////////////////////////////////////////////////////////////////////////

// LINQ to XML code
namespace LinqToXsdAPI.Child.Untyped1
{
    using System;
    using System.Linq;
    using System.Xml.Linq;

    public static class Test
    {
        static XNamespace ns = "http://www.example.com/Address";

        static string Format(XElement a) 
        {
            string variablePart = null;
            if (a.Element(ns + "Street") != null)
                variablePart = (string)a.Element(ns + "Street");
            else if (a.Element(ns + "POBox") != null)
                // Prefix POBox with "PO Box"
                variablePart = "PO Box " + (string)a.Element(ns + "POBox");
            return 
                  variablePart + "\n" // new line for rest
                + (string)a.Element(ns + "City") + ", "
                + (string)a.Element(ns + "State") + " "
                + (string)a.Element(ns + "Zip");
        }
        
        public static void Run()
        {
            var e = XElement.Load("../../Data/Address.xml");
            var s = Format(e);
            new XElement("formatted",s).CompareWithBaseline("Child.xml");
        }
    }
}


// LINQ to XSD code
namespace LinqToXsdAPI.Child.Typed1
{
    using System;
    using System.Linq;
    using www.example.com.Address;
    using System.Xml.Linq;

    public static class Test
    {
        static string Format(USAddress a) 
        {
            string variablePart = null;
            if (a.Street != null)
                variablePart = a.Street;
            else if (a.POBox != null)
                variablePart = "PO Box " + a.POBox;
            return 
                  variablePart + "\n"
                + a.City + ", "
                + a.State + " "
                + a.Zip;
        }
        
        public static void Run()
        {
            var a = Address.Load("../../Data/Address.xml");
            var s = Format(a.Content);
            new XElement("formatted",s).CompareWithBaseline("Child.xml");
        }
    }
}


//
// An aside:
//  Illustrate the possibility of compile-time class extension
//  See the demo solution for an advanced example; see folder Evaluate.
//
namespace www.example.com.Address
{
    public partial class USAddress
    {
        public override string ToString()
        {
            string variablePart = null;
            if (this.Street != null)
                variablePart = this.Street;
            else if (this.POBox != null)
                variablePart = "PO Box " + this.POBox;
            return 
                  variablePart + "\n"
                + this.City + ", "
                + this.State + " "
                + this.Zip;
        }
    }
}


//
// Another aside:
//  Illustrate the possibility of post-compile-time class extension
//  See the demo solution for an advanced example; see folder TypeCheck.
//
namespace www.example.com.Address
{
    public static class USAddressExtension
    {
        public static string Format(this USAddress anAddress)
        {
            string variablePart = null;
            if (anAddress.Street != null)
                variablePart = anAddress.Street;
            else if (anAddress.POBox != null)
                variablePart = "PO Box " + anAddress.POBox;
            return 
                  variablePart + "\n"
                + anAddress.City + ", "
                + anAddress.State + " "
                + anAddress.Zip;
        }
    }
}


///////////////////////////////////////////////////////////////////////////////
// Evaluation of arithmetic expressions
// Various styles are explored based on different schema styles in turn.
///////////////////////////////////////////////////////////////////////////////


// LINQ to XML code operating on a susbtitution group
namespace LinqToXsdAPI.Child.Untyped2
{
    using System;
    using System.Linq;
    using System.Xml.Linq;

    public static class Test
    {
        static XNamespace ns = "http://LinqToXsdSamples/Schemas/ExpSubst";

        static int Eval(XElement e) 
        {
            if (e.Name == ns+"zero") return 0;
            if (e.Name == ns+"succ") return Eval(e.Elements().First()) + 1;
            if (e.Name == ns+"add")  return Eval(e.Elements().ElementAt(0)) 
                                          + Eval(e.Elements().ElementAt(1));
            throw new InvalidOperationException();
        }
        
        public static void Run()
        {
            var e = XElement.Load("../../Data/ExpSubst.xml");
            var i = Eval(e);
            (i == 1).Require();
        }
    }
}


// LINQ to XML code discriminating on a choice
namespace LinqToXsdAPI.Child.Untyped3
{
    using System;
    using System.Linq;
    using System.Xml.Linq;

    public static class Test
    {
        static XNamespace ns = "http://LinqToXsdSamples/Schemas/ExpChoice";

        static int Eval(XElement e) 
        {
            if (e.Element(ns+"zero")!=null) return 0;
            var s = e.Element(ns+"succ");
            if (s != null) return Eval(s.Elements().First()) + 1;
            var a = e.Element(ns+"add");
            if (a!=null) return Eval(a.Elements().ElementAt(0)) 
                              + Eval(a.Elements().ElementAt(1));
            throw new InvalidOperationException();
        }
        
        public static void Run()
        {
            var e = XElement.Load("../../Data/ExpChoice.xml");
            var i = Eval(e);
            (i == 1).Require();
        }
    }
}


// LINQ to XSD code operating on a susbtitution group
namespace LinqToXsdAPI.Child.Typed2
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using LinqToXsdSamples.Schemas.ExpSubst;

    public static class Test
    {

        static int Eval(exp e) 
        {
            if (e is zero) return 0;
            var s = e as succ;
            if (s != null) return Eval(s.exp) + 1;
            var a = e as add;
            if (a != null) return Eval(a.exp[0]) + Eval(a.exp[1]);
            throw new InvalidOperationException();
        }

        public static void Run()
        {
            var e = exp.Load("../../Data/ExpSubst.xml");
            var i = Eval(e);
            (i == 1).Require();
        }
    }
}


// LINQ to XSD code discriminating on a choice
namespace LinqToXsdAPI.Child.Typed3
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using LinqToXsdSamples.Schemas.ExpChoice;

    public static class Test
    {

        static int Eval(exp e) 
        {
            if (e.zero != null) return 0;
            var s = e.succ;
            if (s != null) return Eval(s.exp) + 1;
            var a = e.add;
            if (a != null) return Eval(a.exp[0]) + Eval(a.exp[1]);
            throw new InvalidOperationException();
        }

        public static void Run()
        {
            var e = exp.Load("../../Data/ExpChoice.xml");
            var i = Eval(e);
            (i == 1).Require();
        }
    }
}


// LINQ to XSD code using a virtual method defined in a partial class
namespace LinqToXsdAPI.Child.Typed4
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using LinqToXsdSamples.Schemas.ExpSubst;

    public static class Test
    {
        public static void Run()
        {
            var e = exp.Load("../../Data/ExpSubst.xml");
            var i = e.Eval();
            (i == 1).Require();
        }
    }
}

namespace LinqToXsdSamples.Schemas.ExpSubst {
    public abstract partial class exp {
        public abstract int Eval();
    }
    public partial class zero {
        public override int Eval() { return 0; }
    }
    public partial class succ {
        public override int Eval() { return exp.Eval() + 1; }
    }
    public partial class add {
        public override int Eval() { return exp[0].Eval() + exp[1].Eval(); }
    }
}
