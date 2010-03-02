//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace LinqToXsd
{
    using System.Linq;
    using OO = MyDeliveryCompany.BusinessObjects;
    using XmlIns = www.vertical.com.Invoice; // invoice namespace
    using XmlOns = www.vertical.com.Order;   // order namespace

    public static class Mapping
    {
        // Mapping a business object for an order to an invoice object of a schema-derived class

        public static XmlIns.Invoice OoOrder2XmlInvoice(OO.Order o)
        {
            return new XmlIns.Invoice {
                Name     = o.Cust.Name,
                Street   = o.Cust.Addr.Street,
                City     = o.Cust.Addr.City,
                Zip      = o.Cust.Addr.Zip,
                State    = o.Cust.Addr.State,
                Position =
                  (from i in o.Items
                   select new XmlIns.Position {
                            ProdId   = i.Prod.Id,
                            Price    = i.Price,
                            Quantity = i.Quantity }).ToList(),
                Total = o.Total() };
        }

        // Incorporate an external order into business objects

        public static OO.Order XmlOrder2OoOrder(XmlOns.Order o)
        {
            return new OO.Order {
                Cust  = OO.Customer.Lookup(o.CustId),
                Items = (from i in o.Item
                         select new OO.Item {
                           Prod     = OO.Product.Lookup(i.ProdId),
                           Price    = i.Price,
                           Quantity = i.Quantity }).ToList()};
        }

        // Additional preconditions on incorporation

        public static void Check(XmlOns.Order o)
        {
            double gain = 0;
            if (OO.Customer.Lookup(o.CustId) == null)
                throw new OO.BizException("Unknown customer");
            foreach (var i in o.Item)
            {
                var p = OO.Product.Lookup(i.ProdId);
                if (p == null)
                    throw new OO.BizException("Unknown product");
                gain += i.Price * i.Quantity - p.Price * p.Quantity;
            }
            if (gain <= 0.0)
                    throw new OO.BizException("No BizCase");
        }
    }
}
