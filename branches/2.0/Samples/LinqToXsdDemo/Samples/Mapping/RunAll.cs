//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace LinqToXsdDemo.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;
    using MyDeliveryCompany.BusinessObjects;
    using Xml.Schema.Linq;

    public class Test
    {
        public static void RunAll()
        {
            // Setting up test data
            var bo = new Order {
                Cust = new Customer {
                    Id = "0815",
                    Name = "Fred Mueller",
                    Addr = new Address {
                        Street = "1234, BelRed Road",
                        City = "Redmond",
                        Zip = "98052",
                        State = "WA" }},
                Items = new List<Item> {
                    new Item {
                      Prod     = new Product { Id   = "1234", Price = 44, Quantity = 111 },
                      Price    = 37.0,
                      Quantity = 2 },
                    new Item {
                      Prod     = new Product { Id = "5678", Price = 3, Quantity = 222 },
                      Price    = 1.5,
                      Quantity = 3 }}};
            var xo1 = XElement.Load("../../Data/Order.xml");
            var xo2 = (www.vertical.com.Order.Order)xo1;

            // LINQ to XML
            var i1 = LinqToXml.Mapping.OoOrder2XmlInvoice(bo);
            i1.CompareWithBaseline("Invoice");
            var o1 = LinqToXml.Mapping.XmlOrder2OoOrder(xo1);

            // LINQ to XSD
            var i2 = LinqToXsd.Mapping.OoOrder2XmlInvoice(bo);
            i2.CompareWithBaseline("Invoice");
            var o2 = LinqToXsd.Mapping.XmlOrder2OoOrder(xo2);

            // VB9 XML integration
            var i3 = XmlIntegration.Mapping.OoOrder2XmlInvoice(bo);
            // We do not run comparison because of a Beta1 issue.
            // i3.CompareWithBaseline("Invoice");
            var o3 = XmlIntegration.Mapping.XmlOrder2OoOrder(xo1);

            // DOM
            var i4 = DOM.Mapping.OoOrder2XmlInvoice(bo);
            i4.CompareWithBaseline("Invoice");

            // Data Contract
            var i5 = WCF.Mapping.OoOrder2XmlInvoice(bo);
            i5.Save("../../DataContract/Invoice.xml");
        }
    }
}
