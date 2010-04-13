//Copyright (c) Microsoft Corporation.  All rights reserved.

///////////////////////////////////////////////////////////////////////////////
// Test default attributes
// NOTE: default elements are not yet supported.
///////////////////////////////////////////////////////////////////////////////

namespace LinqToXsdAPI.Default
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using Xml.Schema.Linq;
    using www.w3.org.Item2001.XMLSchema;
    using www.example.com.Address;

    public static class Test
    {
        public static void Run()
        {
            // Create an international address
            var adr = new IntlStreetAddress {
                            Street = "123 Main St",
                            City = "Mercer Island",
                            Zip = 68042,
                            State = "WA"
            };

            // Test for default for country
            (adr.Country == "US").Require();

            // Create an XSD local element
            var e = new localElement();
            e.name = "foo";
            
            // Test for default values of min/maxOccurs
            (e.minOccurs == 1).Require();
            ((decimal)e.maxOccurs == 1).Require();

            // Test for a non-default value
            e.minOccurs = 0;
            (e.minOccurs == 0).Require();
        }
    }
}
