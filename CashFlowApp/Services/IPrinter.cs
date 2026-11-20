using System.Collections.Generic;

namespace ConsoleApp1;

public interface IPrinter
{
    void Update(IEnumerable<CashFlowSimple> simpleFlows);
    void PrintResult();
}