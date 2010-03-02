//Copyright (c) Microsoft Corporation.  All rights reserved.

///////////////////////////////////////////////////////////////////////////////
// Typed XML object construction
///////////////////////////////////////////////////////////////////////////////


// Untyped XElement construction for comparison
namespace LinqToXsdAPI.New.Untyped
{
    using System.Xml.Linq;

    public static class Test
    {
        public static void Run()
        {
            //
            // Copied and pasted from ../Data/Orders.xml
            //
            XNamespace ns = "http://www.example.com/Orders";
            XElement o = new XElement(ns + "PurchaseOrder",
                new XElement(ns + "CustId", "0815"),
                new XElement(ns + "Item",
                    new XElement(ns + "ProdId", "1234"),
                    new XElement(ns + "Price", "37"),
                    new XElement(ns + "Quantity", "2")
                ),
                new XElement(ns + "Item",
                    new XElement(ns + "ProdId", "5678"),
                    new XElement(ns + "Price", "1.5"),
                    new XElement(ns + "Quantity", "3")
                )
            );
            o.CompareWithBaseline("New.xml");
        }
    }
}


//
// Typed XML object construction based on object initialization
//
namespace LinqToXsdAPI.New.PlainObjectInitialization
{
    using www.example.com.Orders;

    public static class Test
    {
        public static void Run()
        {
            var po = new PurchaseOrder {
                CustId = "0815",
                Item = new Item[] {
                    new Item {
                        ProdId = "1234",
                        Price = 37,
                        Quantity = 2
                    },
                    new Item {
                        ProdId = "5678",
                        Price = 1.5,
                        Quantity = 3
                    }
                }
            };
            po.CompareWithBaseline("New.xml");
        }
    }
}


//
// Typed XML object construction with the help of XRoot
//
namespace LinqToXsdAPI.New.XRootSample
{
    using www.example.com.Orders;

    public static class Test
    {
        public static void Run()
        {
            var root =
                new XRootNamespace(
                    new PurchaseOrder {
                        CustId = "0815",
                        Item = new Item[] {
                            new Item {
                                ProdId = "1234",
                                Price = 37,
                                Quantity = 2 },
                            new Item {
                                ProdId = "5678",
                                Price = 1.5,
                                Quantity = 3 }}});
            root.XDocument.Root.CompareWithBaseline("New.xml");
        }
    }
}
