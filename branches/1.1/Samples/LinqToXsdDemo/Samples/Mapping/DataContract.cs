//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace WCF
{
    using System.Linq;
    using OO = MyDeliveryCompany.BusinessObjects;
    using Xml = WCF.vertical.com.Invoice;

    public static class Mapping
    {
        // Mapping a business object for an order to an invoice object of a schema-derived class

        public static Xml.Invoice OoOrder2XmlInvoice(OO.Order o)
        {
            return new Xml.Invoice {
                Name     = o.Cust.Name,
                Street   = o.Cust.Addr.Street,
                City     = o.Cust.Addr.City,
                Zip      = o.Cust.Addr.Zip,
                State    = o.Cust.Addr.State,
                Position =
                  (from i in o.Items
                   select new Xml.Position {
                            ProdId   = i.Prod.Id,
                            Price    = i.Price,
                            Quantity = i.Quantity }).ToList(),
                Total = o.Total() };
        }
    }
}
