//Copyright (c) Microsoft Corporation.  All rights reserved.

///////////////////////////////////////////////////////////////////////////////
// Test Clone() method on XTypedElement
///////////////////////////////////////////////////////////////////////////////

namespace LinqToXsdAPI.Clone
{
    using System;
    using System.Linq;
    using Xml.Schema.Linq;
    using LinqToXsdSamples.Schemas.PO1;

    public static class Test
    {
        public static void Run()
        {
            // Explicit cloning
            var po1 = PurchaseOrder.Load("../../Data/po1.xml");
            var po2 = (PurchaseOrder)po1.Clone();
            po2.CompareWithBaseline("Clone.xml");

            // Implicit cloning
            var po = new PurchaseOrder();
            var adr = new USAddress();
            adr.Country = "US";
            adr.City = "Mercer Island";
            adr.Name = "Patrick Hines";
            adr.State = "WA";
            adr.Street = "123 Main St";
            adr.Zip = 68042;
            po.BillTo = adr;
            po.ShipTo = adr;
            po.BillTo.Equals(adr).Require();
            (!po.BillTo.Equals(po.ShipTo)).Require();

        }
    }
}
