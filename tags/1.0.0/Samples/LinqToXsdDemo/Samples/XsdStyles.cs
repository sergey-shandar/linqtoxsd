//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace LinqToXsdDemo.XsdStyles.Typed
{
    using System.Linq;
    using System.Xml.Linq;
    using www.w3.org.Item2001.XMLSchema;

    public static class Test
    {
        public static XElement WhatStyle(string file)
        {
            schema sch = schema.Load(file);
            
            int g_elds = sch.element.Count;

            int g_ctds = sch.complexType.Count;

            int l_elds = (from e in sch.Query.Descendants<localElement>()
                          where e.name != null
                          select e
                         ).Count();

            int l_ctds = (from ct in sch.Query.Descendants<localComplexType>()
                          select ct
                         ).Count();

            int l_elrs = (from e in sch.Query.Descendants<localElement>()
                          where e.@ref != null
                          select e
                         ).Count();

            return new XElement("style",
                new XAttribute("file", file),
                new XAttribute("g_elds", g_elds),
                new XAttribute("g_ctds", g_ctds),
                new XAttribute("l_elds", l_elds),
                new XAttribute("l_elrs", l_elrs),
                new XAttribute("l_ctds", l_ctds),

                   g_elds >  0
                && g_ctds == 0
                && l_elds >  0
                && l_ctds >  0
                && l_elrs == 0
                ? "Russian Doll" :
                   g_elds >  0
                && g_ctds == 0
                && l_elds == 0
                && l_ctds >  0
                && l_elrs >  0    
                ? "Salami Slice" :
                   g_ctds >  0
                && l_elds >  0
                && l_ctds == 0
                && l_elrs == 0    
                ? "Venetian Blind" :
                   g_elds >  0
                && g_ctds >  0
                && l_elds == 0
                && l_ctds == 0
                && l_elrs >  0    
                ? "Garden Of Eden" :
                  "Unknown style"
                );
        }

        public static void Run()
        {
            var report = new XElement("report",
                WhatStyle("../../Data/Orders.xsd"),
                WhatStyle("../../Data/Company.xsd"),
                WhatStyle("../../Data/xsdschema.xsd"),
                WhatStyle("../../Data/schemas.xsd"),
                WhatStyle("../../Data/MSBuild/Microsoft.Build.Commontypes.xsd"));

            report.CompareWithBaseline("XsdStyles");
        }
    }
}
