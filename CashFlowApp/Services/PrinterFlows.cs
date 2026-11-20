using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1;

public class PrinterFlows : IPrinter
{
    private readonly List<CashFlowSimple> _allSimpleFlows = new();
    public void Update(IEnumerable<CashFlowSimple> simpleFlows)
    {
        _allSimpleFlows.AddRange(simpleFlows);
    }

    public void PrintResult()
    {
        _allSimpleFlows.OrderBy(x=>x.Date).ToList().ForEach(Console.WriteLine);
    }
}