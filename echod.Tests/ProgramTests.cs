namespace echod.Tests;

public class ProgramTests
{
    [Fact]
    public void Main_WithoutArgs_ReturnsMissingArgsError()
    {
        var args = Array.Empty<string>();
        const string expectedOutput = "Required argument missing for command";
        using var stringWriter = new StringWriter();
        Console.SetError(stringWriter);

        Program.Main(args);
        var actualOutput = stringWriter.ToString();

        Assert.Contains(expectedOutput, actualOutput);
    }
}