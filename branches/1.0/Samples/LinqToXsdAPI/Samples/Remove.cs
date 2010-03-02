//Copyright (c) Microsoft Corporation.  All rights reserved.

///////////////////////////////////////////////////////////////////////////////
// Test Remove() on XTypedElement
///////////////////////////////////////////////////////////////////////////////

namespace LinqToXsdAPI.Remove
{
    using System;
    using System.Linq;
    using Xml.Schema.Linq;
    using www.example.com.Orders;

    public static class Test
    {
        public static void Run()
        {
            var b = Batch.Load("../../Data/Orders.xml");
            (b.PurchaseOrder.Count == 2).Require();
            foreach (var po in b.PurchaseOrder)
                if (po.CustId == "1324") po.Untyped.Remove();
            (b.PurchaseOrder.Count == 1).Require();
        }
    }
}
