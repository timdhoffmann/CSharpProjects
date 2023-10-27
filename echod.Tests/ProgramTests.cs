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

    // TODO: can this somehow be locally scoped instead or is it required because the Main method exists longer?
    private StringWriter stringWriter = new StringWriter();

    [Fact]
    public void Main_WithoutArgs_StdErrContainsMissingArgs()
    {
        // Arrange

        var args = Array.Empty<string>();
        const string expectedOutput = "Required argument missing for command";
        var originalError = Console.Error;
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