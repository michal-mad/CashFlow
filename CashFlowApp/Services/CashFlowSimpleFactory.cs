using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1;

public class CashFlowSimpleFactory : ICashFlowSimpleFactory
{
    public IEnumerable<CashFlowSimple> Produce(Models.CashFlowComplex? cashFlow)
    {
        if (cashFlow is null)
            return Enumerable.Empty<CashFlowSimple>();
        var id = Guid.NewGuid();
        var decomposed = Enumerable
            .Range(1, cashFlow.RepeatCount)
            .Select(i =>
                {
                    int index = i - 1;
                    return new CashFlowSimple(
                        cashFlow.Date.AddDays(index * cashFlow.DaysOffset),
                        cashFlow.Amount,
                        cashFlow.Currency,
                        id);
                }
            );
        return decomposed;
    }
}