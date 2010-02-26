//Copyright (c) Microsoft Corporation.  All rights reserved.

//
// We reuse the abstract syntax of the folder Evaluate.
// This time, we implement a type checker for the little language.
// We use an extension method to add type checking as a method to the appropriate class.
// This method of customization is discussed in the LINQ to XSD manual.
//

namespace LinqToXsdDemo.TypeCheck.Typed
{
    using System;
    using System.Linq;
    using www.example.com.Program;

    public static class Test
    {
        public static void Run()
        {
            var p = program.Load("../../Data/Program.xml");
            p.Content.Resolve(null);
            var t = p.Content.TypeCheck();
            (t.ToString() == "boolean").Require();
        }
    }

    enum Type {error,boolean,integer};

    class DeclAnnotation {
        public decl reference;
    }

    class BlockAnnotation {
        public block reference;
    }

    //
    // Exception thrown means type-checking failed
    //
    static class TypeChecker {
        public static Type TypeCheck(this expr e)
        {
            var c = e as constant;
            if (c != null) 
            {    
                if (c.integer.HasValue) return Type.integer; 
                if (c.boolean.HasValue) return Type.boolean; 
                throw new InvalidOperationException();
            }
            var v = e as variable;
            if (v != null)
            {
                return v.Untyped.Annotation<DeclAnnotation>().reference.type.ToType();
            }
            var b = e as block;
            if (b != null)
            {
                Type t = Type.error;
                foreach (expr x in b.expr)
                    t = x.TypeCheck();
                if (t == Type.error)
                    throw new InvalidOperationException();
                return t;
            }
            var a = e as assign;
            if (a != null)
            {
                var t = a.Untyped.Annotation<DeclAnnotation>().reference.type.ToType();
                if (t != a.rhs.TypeCheck())
                    throw new InvalidOperationException();
                return t;
            }
            throw new InvalidOperationException();
        }
        public static Type ToType(this type t)
        {
            if (t is integer) return Type.integer;
            if (t is boolean) return Type.boolean;
            throw new InvalidOperationException();
        }
    }

    static class Resolver 
    {
        public static void Resolve(this expr e, block scope)
        {
            if (e is constant) 
            {
                return;
            }
            var v = e as variable;
            if (v != null) 
            {
                var d = scope.Lookup(v.id);
                v.Untyped.AddAnnotation(
                    new DeclAnnotation { reference = d });
                return;
            }
            var b = e as block;
            if (b != null) 
            {
                b.Untyped.AddAnnotation(
                    new BlockAnnotation { reference = scope });
                foreach (expr x in b.expr)
                    x.Resolve(b);
                return;
            }
            var a = e as assign;
            if (a != null) 
            {
                var d = scope.Lookup(a.id);
                a.Untyped.AddAnnotation(
                    new DeclAnnotation { reference = d });
                a.rhs.Resolve(scope);
                return;
            }
        }

        // Look up variables
        static decl Lookup(this block scope, string id)
        {
            decl local =
                (from d in scope.decl
                 where d.id == id
                 select d).FirstOrDefault();
            if (local!=null)
                return local;
            block up = null;
            try {
                up = scope.Untyped.Annotation<BlockAnnotation>().reference;
            }
            catch (Exception) {
                // Undeclared variable
                throw new InvalidOperationException();
            }
            return up.Lookup(id);
        }
    }
}
