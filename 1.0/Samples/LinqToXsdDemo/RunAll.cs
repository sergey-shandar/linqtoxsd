//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace LinqToXsdDemo
{
    using System;
    using System.IO;
    using System.Diagnostics;
    using System.Xml;
    using System.Xml.Linq;
    using Xml.Schema.Linq;

    public static class RunAll
    {
        static void Main(string[] args)
        {
            LinqToXsdDemo.XsdPrimer.Typed.Test.Run();
            LinqToXsdDemo.Total.Untyped.Test.Run();
            LinqToXsdDemo.Total.Typed.Test.Run();
            LinqToXsdDemo.HeadCount.Typed.Test.Run();
            LinqToXsdDemo.HeadCount.Generic.Test.Run();
            LinqToXsdDemo.HeadCount.Untyped.Test.Run();
            LinqToXsdDemo.WageBill.Untyped.Test.Run();
            LinqToXsdDemo.WageBill.Typed.Test.Run();
            LinqToXsdDemo.Paradise.Typed.Test.Run();
            LinqToXsdDemo.Paradise.Untyped.Test.Run();
            LinqToXsdDemo.XsdStyles.Typed.Test.Run();
            LinqToXsdDemo.Reorganization.Typed.Test.Run();
            LinqToXsdDemo.Reports.Mix1.Test.Run();
            LinqToXsdDemo.Reports.Mix2.Test.Run();
            LinqToXsdDemo.QueryExcel.Typed.Test.Run();
            LinqToXsdDemo.Evaluate.Typed.Test.Run();
            LinqToXsdDemo.TypeCheck.Typed.Test.Run();
            LinqToXsdDemo.Mapping.Test.RunAll();
        }
    }


    //
    // If you see this exception this means ...
    //   ... that the self-checking sample suite has been modified.
    // 
    public class LinqToXsdDemoException : Exception { }


    //
    // Infrastructure for making the samples self-checking
    // 
    public static class LinqToXsdDemoAssertions
    {

        public static void Require(this bool v)
        {
            //
            // If you see this exception this means ...
            //   ... that the self-checking sample suite has been modified.
            // 
            if (!v)
                throw new LinqToXsdDemoException();
        }

        public static void CompareWithBaseline(this XmlDocument x, string baseline)
        {
            var ws = new XmlWriterSettings();
            ws.Indent = true;
            var writer = XmlWriter.Create(tempFile, ws);
            x.Save(writer);
            writer.Close();
            CompareWithBaseline(baseline);
        }

        public static void CompareWithBaseline(this XElement x, string baseline)
        {
            var ws = new XmlWriterSettings();
            ws.Indent = true;
            var writer = XmlWriter.Create(tempFile, ws);
            x.Save(writer);
            writer.Close();
            CompareWithBaseline(baseline);
        }

        public static void CompareWithBaseline(this XTypedElement x, string baseline)
        {
            (x.Untyped).CompareWithBaseline(baseline);
        }

        public static void CompareWithBaseline(string baseline)
        {
            StreamReader srBaseline = null;
            StreamReader srActual = null;
            try
            {
                srBaseline = File.OpenText("../../Baselines/" + baseline + ".xml");
                srActual = File.OpenText(tempFile);
                string strBaseline = srBaseline.ReadToEnd();
                string strActual = srActual.ReadToEnd();
                //
                // If you see this exception this means ...
                //   ... that the self-checking sample suite has been modified.
                // 
                if (strBaseline != strActual)
                    throw new LinqToXsdException();
            }
            finally
            {
                srBaseline.Close();
                srActual.Close();
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        static string tempFile = "../../Temp.xml";
    }
}