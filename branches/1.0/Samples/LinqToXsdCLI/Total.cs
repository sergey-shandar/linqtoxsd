//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Linq;
using www.example.com.Orders;
using System.Diagnostics;

public static class Test 
{
    static double CalculateTotal(Batch os) 
    {
        double Total = 0.0;
        foreach (PurchaseOrder o in os.PurchaseOrder)
            foreach (Item i in o.Item)
                Total += i.Price * i.Quantity;
        return Total;
    }

    public static void Main()
    {
        // Load an element with orders
        Batch os = Batch.Load("Orders.xml");

        // Calculuate total and print it;
        double Total = CalculateTotal(os);
        Console.WriteLine(Total);

        // Also check it
        if (Total!=120.5)
            throw new Exception("Test case failed!");
    }
}