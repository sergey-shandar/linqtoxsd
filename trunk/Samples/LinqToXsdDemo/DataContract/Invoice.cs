using System;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WCF.vertical.com.Invoice
{
    [DataContract(Namespace = "http://www.vertical.com/Invoice")]
    public class Invoice
    {
        [DataMember(Order=0)]
        public string Name;
        [DataMember(Order = 1)]
        public string Street;
        [DataMember(Order = 2)]
        public string City;
        [DataMember(Order = 3)]
        public string Zip;
        [DataMember(Order = 4)]
        public string State;
        [DataMember(Order = 5)]
        public List<Position> Position;
        [DataMember(Order = 6)]
        public double Total;

        public void Save(string uri)
        {
            using (XmlWriter xw = XmlWriter.Create(uri,xws))
            {
                dcs.WriteObject(xw,this);
            }
        }
        private static XmlWriterSettings Settings()
        {
            var xws = new XmlWriterSettings();
            xws.Indent = true;
            return xws;
        }
        private static XmlWriterSettings xws = Settings();
        private static DataContractSerializer dcs = new DataContractSerializer(typeof(Invoice));
    }
    [DataContract(Namespace = "http://www.vertical.com/Invoice")]
    public class Position
    {
        [DataMember(Order = 0)]
        public string ProdId;
        [DataMember(Order = 1)]
        public double Price;
        [DataMember(Order = 2)]
        public int Quantity;
    }
}
