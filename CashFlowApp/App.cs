using System;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ConsoleApp1;

public sealed class App : IDisposable, IAsyncDisposable
{
    private readonly CsvReader _csvReader;
    private readonly ICashFlowSimpleFactory _simpleFactory;
    private readonly IPrinter _printer;
    private readonly IValidator<Models.CashFlowComplex> _flowValidator;
    private readonly ILogger<App> _logger;

    public App(
        CsvReader csvReader,
        ICashFlowSimpleFactory simpleFactory, 
        IPrinter printer,
        IValidator<Models.CashFlowComplex> flowValidator, 
        ILogger<App> logger)
    {
        _csvReader = csvReader;
        _simpleFactory = simpleFactory;
        _printer = printer;
        _flowValidator = flowValidator;
        _logger = logger;
    }
    public void Run()
    {
        while (_csvReader.Read())
        {
            var cashFlowComplex = _csvReader.GetRecord<Models.CashFlowComplex>();
            var validation = _flowValidator.Validate(cashFlowComplex);
            if (!validation.IsValid)
            {
                _logger.LogDebug("Invalid cash flow instruction on row={rowNumber}", _csvReader.RowNumber);
                _logger.LogDebug(string.Join(" | ", validation.Errors.Select(x=>x.ErrorMessage)));
                continue;
            }
            var cashFlowsSimple = _simpleFactory.Produce(cashFlowComplex);
            _printer.Update(cashFlowsSimple);
        }
        _printer.PrintResult();
    }

    public void Dispose()
    {
        _csvReader.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _csvReader.DisposeAsync();
    }
}