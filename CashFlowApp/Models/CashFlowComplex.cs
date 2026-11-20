using System;
using CsvHelper;

namespace ConsoleApp1.Models;

[CsvRecord]
public partial record CashFlowComplex(
    [CsvColumn(Index = 0)] DateTime Date,
    [CsvColumn(Index = 1)] int DaysOffset,
    [CsvColumn(Index = 2)] decimal Amount,
    [CsvColumn(Index = 3)] int RepeatCount,
    [CsvColumn(Index = 4)] string Currency);