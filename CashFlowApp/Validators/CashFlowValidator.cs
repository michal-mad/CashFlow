using FluentValidation;

namespace ConsoleApp1;

public class CashFlowValidator : AbstractValidator<Models.CashFlowComplex>
{
    public CashFlowValidator()
    {
        RuleFor(x => x.RepeatCount).GreaterThan(0);
        RuleFor(x => x.Currency).NotEmpty();
        RuleFor(x => x.DaysOffset).GreaterThanOrEqualTo(0);
    }
}