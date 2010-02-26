namespace MyDeliveryCompany.BusinessObjects
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Customer 
    {
        public Customer() { }
        public string Id
        {
            get {
                return _custid;
            }
            //
            // We would better not want to admit a setter for custid.
            // However, it's is needed if we want to use object initializer syntax.
            //
            set {
                if (_custid != null) throw new InvalidOperationException();
                _custid = value;
                custs.Add(Id,this);
            }
        }
        public string Name;
        public Address Addr;
        public static Customer Lookup(string custid)
        {
            Customer cust = null;
            custs.TryGetValue(custid,out cust);
            return cust;
        }
        private string _custid;
        private static Dictionary<string,Customer> custs = new Dictionary<string,Customer>();
    }

    public class Address
    {
        public Address() { }
        public string Street;
        public string City;
        public string Zip;
        public string State;
    }

    public class Order
    {
        public string OrdId
        {
            get {
                if (_ordid==null)
                {
                    lastid++;
                    _ordid = lastid.ToString();
                }
                return _ordid;
            }
        }
        public Customer Cust;
        public List<Item> Items
        {
            get {
                if (_items==null) _items = new List<Item>();
                return _items;
            }
            set {
                _items = value;
            }
        }
        private static int lastid = 0;
        private string _ordid;
        private List<Item> _items;

        public double Total()
        {
            return (from i in Items
                    select i.Price * i.Quantity).Sum();
        }
    }

    public class Item
    {
        public Item() { }
        public Product Prod;
        public double Price; // retail price
        public int Quantity; // quantity ordered
    }

    public class Product
    {
        public Product() { }
        public string Id
        {
            get {
                return _prodid;
            }
            set {
                if (_prodid != null) throw new InvalidOperationException();
                _prodid = value;
                prods.Add(Id,this);
            }
        }
        public double Price; // purchase price
        public int Quantity; // quantity in stock
        public static Product Lookup(string prodid)
        {
            Product prod = null;
            prods.TryGetValue(prodid,out prod);
            return prod;
        }
        private string _prodid;
        private static Dictionary<string,Product> prods = new Dictionary<string,Product>();
    }

    public class BizException : Exception
    {
        public BizException(string s) : base(s)
        {
        }
    }
}
