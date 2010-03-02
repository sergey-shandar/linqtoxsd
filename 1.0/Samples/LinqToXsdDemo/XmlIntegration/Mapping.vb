'Copyright (c) Microsoft Corporation.  All rights reserved.

Imports System.Linq
Imports System.Xml.Linq
Imports OO = MyDeliveryCompany.BusinessObjects
Imports <xmlns:ins="http://www.vertical.com/Invoice">
Imports <xmlns:ons="http://www.vertical.com/Order">

Namespace XmlIntegration

    Public Class Mapping

        ' Mapping a business object for an order to an invoice object of a schema-derived class
        <CLSCompliant(False)> _
        Shared Function OoOrder2XmlInvoice(ByVal o As OO.Order) _
                        As XElement
            Return _
                <Invoice xmlns="http://www.vertical.com/Invoice">
                    <Name><%= o.Cust.Name %></Name>
                    <Street><%= o.Cust.Addr.Street %></Street>
                    <City><%= o.Cust.Addr.City %></City>
                    <Zip><%= o.Cust.Addr.Zip %></Zip>
                    <State><%= o.Cust.Addr.State %></State>
                    <%= From i In o.Items _
                        Select _
                        <Position xmlns="http://www.vertical.com/Invoice">
                            <ProdId><%= i.Prod.Id %></ProdId>
                            <Price><%= i.Price %></Price>
                            <Quantity><%= i.Quantity %></Quantity>
                        </Position> _
                    %>
                    <Total><%= o.Total() %></Total>
                </Invoice>
        End Function

        ' Incorporate an external order into business objects
        <CLSCompliant(False)> _
        Shared Function XmlOrder2OoOrder(ByVal o As XElement) _
                        As OO.Order
            Return _
                New OO.Order With { _
                    .Cust = OO.Customer.Lookup(o.<ons:CustId>.Value), _
                    .Items = _
                (From i In o.<ons:Item> _
                Select _
                    New OO.Item With { _
                        .Prod = OO.Product.Lookup(i.<ons:ProdId>.Value), _
                        .Price = i.<ons:Quantity>.Value, _
                        .Quantity = i.<ons:Quantity>.Value}).ToList()}
        End Function
    End Class
End Namespace
