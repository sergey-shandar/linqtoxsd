//Copyright (c) Microsoft Corporation.  All rights reserved.

//
// Given an XML schema for abstract syntax trees of (simple, imperative) programs,
// we would like to define the evaluation for such programs as a virtual method
// over the generated classes for the different syntactical forms. Here, we assume
// that these forms were modeled as a substitution group or a type-derivation
// hierarchy in XSD, in which case this organization is mapped to OO subclassing.
// This method of customization is discussed in the LINQ to XSD manual.
//

namespace LinqToXsdDemo.Evaluate.Typed
{
    using www.example.com.Program;

    public static class Test
    {
        public static void Run()
        {
            var p = program.Load("../../Data/Program.xml");
            ((bool)p.Evaluate()).Require();
        }
    }
}

namespace www.example.com.Program
{
    using Store =
          System.Collections.Generic.Dictionary<string,object>;

    public partial class program
    {
        public object Evaluate()
        {
            return this.Content.Evaluate(new Store());
        }
    }

    public partial class expr 
    {
        public abstract object Evaluate(Store s);
    }

    public partial class constant
    {
        public override object Evaluate(Store s)
        {
            return this.integer.HasValue ?
                        (object)this.integer.Value
                      : (object)this.boolean.Value;
        }
    }

    public partial class variable
    {
        public override object Evaluate(Store s)
        {
            object result = null;
            s.TryGetValue(this.id, out result);
            return result;
        }
    }

    public partial class block
    {
        public override object Evaluate(Store s)
        {
            object result = null;
            foreach (var x in this.expr)
                result = x.Evaluate(s);
            return result;
        }
    }

    public partial class assign
    {
        public override object Evaluate(Store s)
        {
            var result = this.rhs.Evaluate(s);
            if (s.ContainsKey(this.id))
                s.Remove(this.id);
            s.Add(this.id,result);
            return result;
        }
    }
}
