using System.Collections.Generic;

namespace ConsoleApp1;

public interface ICashFlowSimpleFactory
{
    IEnumerable<CashFlowSimple> Produce(Models.CashFlowComplex? cashFlow);
}