using System.IO;
using System.Linq;
using FluentValidation;

namespace ConsoleApp1;

public class CmdArgsValidator : AbstractValidator<string[]>
{
    public CmdArgsValidator()
    {
        RuleFor(x => x).NotEmpty()
            .WithMessage("args must provide path to input file and a flag '-t1' or '-t2'.")
            .WithName("Command-line args");
        RuleFor(x => x.FirstOrDefault()).Must(File.Exists)
            .WithMessage("The file '{PropertyValue}' does not exist.")
            .WithName("args[0]");;
        RuleFor(x => x.Skip(1).FirstOrDefault()).Must(arg => arg is "-t1" or "-t2")
            .WithMessage("The argument '{PropertyValue}' must be either '-t1' or '-t2'.")
            .WithName("args[1]");
    }
}