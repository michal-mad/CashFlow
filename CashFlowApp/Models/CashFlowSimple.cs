using System;

namespace ConsoleApp1;

public record CashFlowSimple(DateTime Date, decimal Amount, string Currency, Guid Id)
{
    public override string ToString()
    {
        return $"{Date}     ;   {Amount}    ;    {Currency}";
    }
}