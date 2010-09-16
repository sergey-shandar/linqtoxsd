namespace Xml.Schema.Linq.Xunit
{
    using X = global::Xunit;
    using S = global::System.Xml.Schema;
    using XML = global::System.Xml;
    using A = global::System.Reflection.Assembly;
    using G = XObjectsGenerator.XObjectsGenerator;
    using R = Properties.Resources;

    public class Attribute
    {
        [X.Fact]
        public void Empty()
        {
            var set = new S.XmlSchemaSet();
            G.ThisAssembly = A.GetExecutingAssembly();
            G.GenerateXObjects(set, null, null, null, true, false);
        }

        public void UnionTest(string s)
        {
            var set = new S.XmlSchemaSet();
            var settings = new XML.XmlReaderSettings()
            {
                DtdProcessing = XML.DtdProcessing.Parse,
            };
            var reader = new System.IO.StringReader(s);
            var r = XML.XmlReader.Create(reader, settings);
            set.Add(null, r);
            G.ThisAssembly = A.GetExecutingAssembly();
            G.GenerateXObjects(set, null, null, null, true, false);
        }

        [X.Fact]
        public void Union()
        {
            this.UnionTest(R.UnionTest);
        }

        [X.Fact]
        public void UnionBasic()
        {
            this.UnionTest(R.UnionTestBasic);
        }

        [X.Fact]
        public void UnionType()
        {
            this.UnionTest(R.UnionTestType);
        }

    }
}
