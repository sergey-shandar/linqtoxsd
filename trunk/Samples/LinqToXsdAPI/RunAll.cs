//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace LinqToXsdAPI
{
    using System;
    using System.IO;
    using System.Xml.Linq;

    public static class RunAll 
    {
        static void Main(string[] args)
        {
            LinqToXsdAPI.Cast.Test.Run();
            LinqToXsdAPI.New.Untyped.Test.Run();
            LinqToXsdAPI.New.PlainObjectInitialization.Test.Run();
            LinqToXsdAPI.New.XRootSample.Test.Run();
            LinqToXsdAPI.Clone.Test.Run();
            LinqToXsdAPI.XElementProperty.Test.Run();
            LinqToXsdAPI.Remove.Test.Run();
            LinqToXsdAPI.Ancestors.Test.Run();
            LinqToXsdAPI.Descendants.Test.Run();
            LinqToXsdAPI.Parent.Test.Run();
            LinqToXsdAPI.Append.Untyped.Test.Run();
            LinqToXsdAPI.Append.Typed.Test.Run();
            LinqToXsdAPI.Child.Untyped1.Test.Run();
            LinqToXsdAPI.Child.Typed1.Test.Run();
            LinqToXsdAPI.Child.Untyped2.Test.Run();
            LinqToXsdAPI.Child.Typed2.Test.Run();
            LinqToXsdAPI.Child.Untyped3.Test.Run();
            LinqToXsdAPI.Child.Typed3.Test.Run();
            LinqToXsdAPI.Child.Typed4.Test.Run();
            LinqToXsdAPI.Any.Test.Run();
            LinqToXsdAPI.Default.Test.Run();
        }
    }


    //
    // If you see this exception this means ...
    //   ... that the self-checking sample suite has been modified.
    // 
    public class LinqToXsdAPIException : Exception { }


    //
    // Infrastructure for making the samples self-checking
    // 
    public static class LinqToXsdAPIAssertions
    {

        public static void Require(this bool v)
        {
            //
            // If you see this exception this means ...
            //   ... that the self-checking sample suite has been modified.
            // 
            if (!v)
                throw new LinqToXsdAPIException();
        }

        public static void CompareWithBaseline(this Xml.Schema.Linq.XTypedElement x,string baseline) {
            x.Untyped.CompareWithBaseline(baseline);
        }

        public static void CompareWithBaseline(this System.Xml.Linq.XElement x,string baseline) {
                string Temp = "../../Temp.xml";
                x.Save(Temp);
                StreamReader srBaseline = null;
                StreamReader srActual = null;
                try
                {
                    srBaseline = File.OpenText("../../Baselines/" + baseline);
                    srActual = File.OpenText(Temp);
                    string strBaseline = srBaseline.ReadToEnd();
                    string strActual = srActual.ReadToEnd();
                    //
                    // If you see this exception this means ...
                    //   ... that the self-checking sample suite has been modified.
                    // 
                    if (strBaseline != strActual)
                        throw new LinqToXsdAPIException();
                }
                finally
                {
                    srBaseline.Close();
                    srActual.Close();
                    if (File.Exists(Temp))
                        File.Delete(Temp);
                }          
        }    
    }
}