namespace Xml.Schema.Linq.Xunit
{
    using X = global::Xunit;
    using S = global::System.Xml.Schema;
    using XML = global::System.Xml;
    using A = global::System.Reflection.Assembly;
    using G = XObjectsGenerator.XObjectsGenerator;

    public class Attribute
    {
        [X.Fact]
        public void Empty()
        {
            var set = new S.XmlSchemaSet();
            G.ThisAssembly = A.GetExecutingAssembly();
            G.GenerateXObjects(set, null, null, null, true, false);
        }

        [X.Fact]
        public void Union()
        {
            var set = new S.XmlSchemaSet();
            var settings = new XML.XmlReaderSettings()
            {
                DtdProcessing = XML.DtdProcessing.Parse,
            };
            var reader = new System.IO.StringReader(Properties.Resources.UnionTest);
            var r = XML.XmlReader.Create(reader, settings);
            set.Add(null, r);
            G.ThisAssembly = A.GetExecutingAssembly();
            G.GenerateXObjects(set, null, null, null, true, false);
        }
    }
}
