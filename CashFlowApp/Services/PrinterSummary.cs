using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1;

public class PrinterSummary : IPrinter
{
    private decimal _sum = 0;
    public void Update(IEnumerable<CashFlowSimple> simpleFlows)
    {
        _sum += simpleFlows.Sum(x => x.Amount);
    }
    
    public void PrintResult()
    {
        Console.WriteLine($"Total sum of all cash flows: {_sum}.");
    }
}

public class PrinterMuted : IPrinter
{
    public void Update(IEnumerable<CashFlowSimple> simpleFlows)
    {
    }

    public void PrintResult()
    {
        Console.WriteLine("NO RESULTS");
    }
}