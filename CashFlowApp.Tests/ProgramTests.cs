using ConsoleApp1;

namespace TestProject1;

public class ProgramTests
{
    private static readonly string TestDataDirectory = Path.Combine(AppContext.BaseDirectory, "TestingData");
    
    [Theory()]
    [InlineData("input_0.txt", "output_0.txt", "-t1")]
    [InlineData("input_1.txt", "output_1.txt", "-t1")]
    [InlineData("input_0.txt", "output_t0.txt", "-t2")]
    [InlineData("input_1.txt", "output_t1.txt", "-t2")]
    public void Main_ShouldRunWithoutExceptions(string inputFileName, string outputFileName, string t)
    {
        // Arrange
        var inputFilePath = Path.Combine(TestDataDirectory, inputFileName);
        var outputFilePath = Path.Combine(TestDataDirectory, outputFileName);
        string[] args = { inputFilePath, t };
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act
        Program.Main(args);

        // Assert
        string output = stringWriter.ToString();
        Assert.NotEmpty(output); 
        Assert.Contains(File.ReadAllText(outputFilePath), output); 
    }
}