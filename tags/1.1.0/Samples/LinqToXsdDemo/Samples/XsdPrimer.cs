//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace LinqToXsdDemo.XsdPrimer.Typed
{
    using www.example.com.PO1;

    public static class Test 
    {
        public static void Run()
        {
            var po = XRootNamespace
                        .Load("../../Data/po1.xml")
                        .purchaseOrder;
            po.billTo = po.shipTo;
            po.CompareWithBaseline("XsdPrimer");
        }
    }
}
