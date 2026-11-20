//  • • •
// > Receiving a number of SimBucks on a given date.
// > Paying a number of SimBucks on a given date.
// > Receiving a number of SimBucks every N days, repeated a given number of times, starting on a given date.
// > Paying a number of SimBucks every N days, repeated a given number of times, starting on a given date

// date         ; days offset   ;   amount  ;   repeat count    ;   currency
// 17.11.2026   ;       0       ;   +200    ;       1           ;   SimBucks
// 18.11.2026   ;       0       ;   -100    ;       1           ;   SimBucks
// 25.12.2026   ;       3       ;   +50     ;       4           ;   SimBucks

// Using the textual formats from task 1, design a program that:
// 
// 1. reads a newline-separated list of cashflow instructions, then
// 2. converts them to simple cashflows, and then
// 3. prints a newline-separated list of simple cashflows.
//
// date         ;   amount         ;   currency
// 17.11.2026   ;   +200           ;   SimBucks
// 18.11.2026   ;   -100           ;   SimBucks
// 25.12.2026   ;   +50            ;   SimBucks
// 28.12.2026   ;   +50            ;   SimBucks
// 31.12.2026   ;   +50            ;   SimBucks
// 03.01.2026   ;   +50            ;   SimBucks

using System;
using System.Globalization;
using CsvHelper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp1;

public static class Program
{
    public static void Main(string[] args)
    {
        var argsValidatorResult = new CmdArgsValidator().Validate(args);
        if (!argsValidatorResult.IsValid)
        {
            argsValidatorResult.Errors.ForEach(Console.WriteLine);
            return;
        }

        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddTransient<ICashFlowSimpleFactory, CashFlowSimpleFactory>();
        builder.Services.AddTransient<IValidator<Models.CashFlowComplex>, CashFlowValidator>();
        builder.Services.AddTransient<CsvReader>(_ =>
            CsvReader.FromFile(args[0],
                new CsvReaderOptions
                {
                    FormatProvider = CultureInfo.CurrentCulture,
                    HasHeader = false,
                    MissingField = CsvMissingFieldHandling.Throw,
                    TrackLineNumbers = true,
                    TrimWhitespace = true
                }
            ));
        builder.Services.AddTransient<IPrinter>(_ =>
        {
            switch (args[1])
            {
                case "-t1":
                    return new PrinterFlows();
                case "-t2":
                    return new PrinterSummary();
            }
            return new PrinterMuted();
        });
        builder.Services.AddTransient<App>();

        var host = builder.Build();
        var app = host.Services.GetRequiredService<App>();
        try
        {
            app.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        finally
        {
            app.Dispose();
        }

    }
}