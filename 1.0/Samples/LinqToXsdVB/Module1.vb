'Copyright (c) Microsoft Corporation.  All rights reserved.

Imports www.example.com.Orders
Imports System.Diagnostics

Module Module1

    Function CalculateTotal(ByVal b As Batch) As Double
        Dim Total As Double = 0
        For Each o In b.PurchaseOrder
            For Each i In o.Item
                Total += i.Price * i.Quantity
            Next
        Next
        Return Total
    End Function

    Sub Main()

        Dim b = Batch.Load("../../Orders.xml")
        Trace.Assert(CalculateTotal(b) = 120.5)

    End Sub

End Module