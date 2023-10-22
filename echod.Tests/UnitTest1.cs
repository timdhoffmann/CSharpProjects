namespace echod.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var args = Array.Empty<string>();
        using var stringWriter = new StringWriter();
        Console.SetError(stringWriter);

        Program.Main(args);

        var consoleOutput = stringWriter.ToString();

        Assert.Contains("Required argument missing for command", consoleOutput);
    }
}