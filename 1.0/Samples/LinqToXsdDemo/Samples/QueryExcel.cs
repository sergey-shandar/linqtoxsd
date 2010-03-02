//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace LinqToXsdDemo.QueryExcel.Typed
{
    using System.Linq;
    using System.Xml.Linq;
    using urn.schemas.microsoft.com.office.excel;
    using urn.schemas.microsoft.com.office.office;
    using urn.schemas.microsoft.com.office.spreadsheet;

    public static class Test
    {
        public static void Run()
        {
            var wb = Workbook.Load("../../Data/schemas.xml");
            int G_ELDS = 0;
            int G_CTDS = 0;
            foreach (var r in wb.Worksheet.Table.Row.Skip(1)) {
                G_ELDS += (int)r.Cell[1].Data.Untyped;
                G_CTDS += (int)r.Cell[2].Data.Untyped;
            }
            var report = new XElement("report",
                            new XAttribute("G_ELDS", G_ELDS),
                            new XAttribute("G_CTDS", G_CTDS));
            report.CompareWithBaseline("QueryExcel");
        }
    }
}
