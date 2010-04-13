//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace DOM
{
    using System.Xml;
    using OO = MyDeliveryCompany.BusinessObjects;

    public static class Mapping
    {

        // Mapping an business object for an order to an XmlDocument for an invoice

        public static XmlDocument OoOrder2XmlInvoice(OO.Order o)
        {
            string ns = "http://www.vertical.com/Invoice";
            var doc = new XmlDocument();

            var inv = doc.CreateElement("Invoice", ns);
            doc.AppendChild(inv);

            var name = doc.CreateElement("Name", ns);
            inv.AppendChild(name);
            name.AppendChild(doc.CreateTextNode(o.Cust.Name));

            // Ditto for street, city, zip, state

            var street = doc.CreateElement("Street", ns);
            inv.AppendChild(street);
            street.AppendChild(doc.CreateTextNode(o.Cust.Addr.Street));

            var city = doc.CreateElement("City", ns);
            inv.AppendChild(city);
            city.AppendChild(doc.CreateTextNode(o.Cust.Addr.City));

            var zip = doc.CreateElement("Zip", ns);
            inv.AppendChild(zip);
            zip.AppendChild(doc.CreateTextNode(o.Cust.Addr.Zip));

            var state = doc.CreateElement("State", ns);
            inv.AppendChild(state);
            state.AppendChild(doc.CreateTextNode(o.Cust.Addr.State));

            foreach (var i in o.Items)
            {
                var item = doc.CreateElement("Position", ns);
                inv.AppendChild(item);

                var prodid = doc.CreateElement("ProdId", ns);
                item.AppendChild(prodid);
                prodid.AppendChild(doc.CreateTextNode(i.Prod.Id));

                // Ditto for price, quantity

                var price = doc.CreateElement("Price", ns);
                item.AppendChild(price);
                price.AppendChild(doc.CreateTextNode(XmlConvert.ToString(i.Price)));

                var quantity = doc.CreateElement("Quantity", ns);
                item.AppendChild(quantity);
                quantity.AppendChild(doc.CreateTextNode(i.Quantity.ToString()));
            }

            var total = doc.CreateElement("Total", ns);
            inv.AppendChild(total);
            total.AppendChild(doc.CreateTextNode(o.Total().ToString()));

            return doc;
        }
    }
}
