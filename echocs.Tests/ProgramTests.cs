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

    // Original console output writers for restoring after manipulation.
    private readonly TextWriter outWriter = Console.Out;
    private readonly TextWriter errorWriter = Console.Error;

    [Fact]
    public void Main_WithoutArgs_StdErrContainsMissingArgs()
    {
        // Arrange

        var args = Array.Empty<string>();
        const string expectedOutput = "Required argument missing for command";
        
        using var tempErrWriter = new StringWriter();
        Console.SetError(tempErrWriter);

        // Act

        Program.Main(args);
        var actualOutput = tempErrWriter.ToString();
        Console.SetError(errorWriter);

        // Assert
        Assert.Contains(expectedOutput, actualOutput);
    }

    [Fact]
    public void Main_WithSomeArg_StdOutContainsArg()
    {
        // Arrange

        var args = new [] { "hello" };
        const string expectedOutput = "hello";

        using var tempOutWriter = new StringWriter();
        Console.SetOut(tempOutWriter);

        // Act

        Program.Main(args);
        var actualOutput = tempOutWriter.ToString();
        Console.SetOut(outWriter);
        // To see this being printed to the console, run the test with
        // 'dotnet test --logger "console;verbosity=detailed"'
        output.WriteLine($"[DEBUG] {actualOutput}");

        // Assert
        Assert.Contains(expectedOutput, actualOutput);
    }

    [Fact]
    public void Main_Input1_MatchesOriginalEcho()
    {
        var args = new[] { "Hello there" };

        var originalOutputFilePath = Path.Combine("..", "..", "..", "expected", "hello1.txt");
        // TODO: handle nonexistent file.
        // TODO: read and compare line by line with a buffer instead of the whole file at once.
        var expectedOutput = File.ReadAllText(originalOutputFilePath).ReplaceLineEndings();

        using var tempOutWriter = new StringWriter();
        Console.SetOut(tempOutWriter);

        // Act
        Program.Main(args);
        var actualOutput = tempOutWriter.ToString();
        Console.SetOut(outWriter);

        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }
}