//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace LinqToXml
{
    using System.Linq;
    using System.Xml.Linq;
    using OO = MyDeliveryCompany.BusinessObjects;

    public static class Mapping
    {
        static XNamespace ins = "http://www.vertical.com/Invoice";
        static XNamespace ons = "http://www.vertical.com/Order";

        // Mapping an business object for an order to an XElement for an invoice

        public static XElement OoOrder2XmlInvoice(OO.Order o)
        {
            return new XElement(ins + "Invoice",
                      new XElement(ins + "Name", o.Cust.Name),
                      new XElement(ins + "Street",o.Cust.Addr.Street),
                      new XElement(ins + "City",o.Cust.Addr.City),
                      new XElement(ins + "Zip",o.Cust.Addr.Zip),
                      new XElement(ins + "State",o.Cust.Addr.State),
                      from i in o.Items
                      select new XElement(ins + "Position",
                                new XElement(ins + "ProdId", i.Prod.Id),
                                new XElement(ins + "Price", i.Price),
                                new XElement(ins + "Quantity", i.Quantity)),
                      new XElement(ins + "Total", o.Total()));
        }

        // Incorporate an external order into business objects

        public static OO.Order XmlOrder2OoOrder(XElement o)
        {
            return new OO.Order {
                Cust  = OO.Customer.Lookup((string)o.Element(ons + "CustId")),
                Items = (from i in o.Elements(ons + "Item")
                         select new OO.Item {
                           Prod     = OO.Product.Lookup((string)i.Element(ons + "ProdId")),
                           Price    = (double)i.Element(ons + "Price"),
                           Quantity = (int)i.Element(ons + "Quantity")}).ToList()};
        }
    }
}
