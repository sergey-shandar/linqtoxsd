namespace Xml.Schema.Linq.Xunit
{
    using X = global::Xunit;
    using S = global::System.Xml.Schema;
    using A = global::System.Reflection.Assembly;
    using G = XObjectsGenerator.XObjectsGenerator;

    public class Attribute
    {
        [X.Fact]
        public void Fact()
        {
            var set = new S.XmlSchemaSet();
            G.ThisAssembly = A.GetExecutingAssembly();
            G.GenerateXObjects(set, null, null, null, true, false);
        }
    }
}
