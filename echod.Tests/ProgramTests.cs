using Xunit.Abstractions;

namespace echod.Tests;

public class ProgramTests
{

    public ProgramTests(ITestOutputHelper output)
    {
        this.output = output;
    }

    // Helper for debug logging.
    private readonly ITestOutputHelper output;

    [Fact]
    public void Main_WithoutArgs_StdErrContainsMissingArgs()
    {
        // Arrange

        var args = Array.Empty<string>();
        const string expectedOutput = "Required argument missing for command";
        using var stringWriter = new StringWriter();
        Console.SetError(stringWriter);

        // Act

        Program.Main(args);
        var actualOutput = stringWriter.ToString();

        // Assert
        Assert.Contains(expectedOutput, actualOutput);
    }

    [Fact]
    public void Main_WithSomeArg_StdOutContainsArg()
    {
        // Arrange

        var args = new [] { "hello" };
        const string expectedOutput = "hello";

        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act

        Program.Main(args);
        var actualOutput = stringWriter.ToString();
        // To see this being printed to the console, run the test with
        // 'dotnet test --logger "console;verbosity=detailed"'
        output.WriteLine($"[DEBUG] {actualOutput}");

        // Assert
        Assert.Contains(expectedOutput, actualOutput);
    }
}