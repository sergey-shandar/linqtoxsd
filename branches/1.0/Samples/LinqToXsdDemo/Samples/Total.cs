//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace LinqToXsdDemo.Total.Typed
{
    using System;
    using System.Linq;
    using www.example.com.Orders;

    public static class Test 
    {
        static double CalculateTotal(Batch batch) 
        {
            return
                (from purchaseOrder in batch.PurchaseOrder
                 from item in purchaseOrder.Item
                 select item.Price * item.Quantity
                ).Sum();
        }

        public static void Run()
        {
            // Load an element with orders
            var os = Batch.Load("../../Data/Orders.xml");

            // Calculuate and print the total
            var total = CalculateTotal(os);
            // Console.WriteLine(total);
            (total == 120.5).Require();

            // Construct orders and calculate again
            os = CreateOrders();
            total = CalculateTotal(os);
            (total == 120.5).Require();
        }

        public static Batch CreateOrders()
        {
            return new Batch {
                PurchaseOrder = new[] {
                    new PurchaseOrder {
                        CustId = "0815",
                        Item = new Item[] {
                            new Item {
                                ProdId   = "1234",
                                Price    = 37,
                                Quantity = 2 },
                            new Item {
                                ProdId   = "5678",
                                Price    = 1.5,
                                Quantity = 3 }}},
                    new PurchaseOrder {
                        CustId = "1324",
                        Item = new Item[] {
                            new Item {
                                ProdId   = "7788",
                                Price    = 42,
                                Quantity = 1 }}}}};
        }

        public static Batch CreateOrders2()
        {
            var b = new Batch();
            var o = new PurchaseOrder();
            o.CustId = "0815";
            var i = new Item();
            i.ProdId = "1234";
            i.Price = 37;
            i.Quantity = 2;
            o.Item.Add(i);
            i = new Item();
            i.ProdId = "5678";
            i.Price = 1.5;
            i.Quantity = 3;
            o.Item.Add(i);
            b.PurchaseOrder.Add(o);
            o = new PurchaseOrder();
            o.CustId = "1324";
            i = new Item();
            i.ProdId = "7788";
            i.Price = 42;
            i.Quantity = 1;
            b.PurchaseOrder.Add(o);
            return b;
        }
    }
}


namespace LinqToXsdDemo.Total.Untyped
{
    using System;
    using System.Linq;
    using System.Xml.Linq;

    public static class Test 
    {
        static double CalculateTotal(XElement batch) 
        {
            XNamespace ns = "http://www.example.com/Orders";
            return
                (from purchaseOrder in batch.Elements(ns + "PurchaseOrder")
                 from item in purchaseOrder.Elements(ns + "Item")
                 select (double)item.Element(ns + "Price")
                     * (int)item.Element(ns + "Quantity")
                ).Sum();
        }

        public static void Run()
        {
            // Load an element with orders
            var os = XElement.Load("../../Data/Orders.xml");

            // Calculuate and print the total
            var total = CalculateTotal(os);
            // Console.WriteLine(total);
            (total == 120.5).Require();

            // Construct orders and calculate again
            os = CreateOrders();
            total = CalculateTotal(os);
            (total == 120.5).Require();
        }

        public static XElement CreateOrders()
        {
            XNamespace ns = "http://www.example.com/Orders";
            return new XElement(ns + "Batch",
                new XAttribute("xmlns", ns),
                new XElement(ns + "PurchaseOrder",
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
                ),
                new XElement(ns + "PurchaseOrder",
                    new XElement(ns + "CustId", "1324"),
                    new XElement(ns + "Item",
                        new XElement(ns + "ProdId", "7788"),
                        new XElement(ns + "Price", "42"),
                        new XElement(ns + "Quantity", "1")
                    )
                )
            );
        }
    }
}
