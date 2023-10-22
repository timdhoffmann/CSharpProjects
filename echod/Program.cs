using System.CommandLine;
using System.CommandLine.Invocation;

namespace echod;

public static class Program
{

    // TODO: p. 45 - writing integration tests.

    /// <summary>
    /// A clone of the 'echo' Unix utility. Prints its arguments to the console.
    /// </summary>
    /// <param name="args">Strings to be printed.</param>
    /// <returns></returns>
    public static void Main(string[] args)
    {

        var textArg = new Argument<string>
        {
            Description = "The text to print to the console.",
            Name = "TEXT"
        };

        var omitNewLineOpt = new Option<bool>(
            description: "If present, omit new line at the end.",
            name: "-n");

        var rootCommand = new RootCommand("An 'echo' clone written in C#/.NET.");
        rootCommand.AddArgument(textArg);
        rootCommand.AddOption(omitNewLineOpt);

        rootCommand.SetHandler((text, omitNewLineOpt) =>
            {
                text = (omitNewLineOpt) ? text : $"{text}{Environment.NewLine}";
                Console.WriteLine(text);
            },
            textArg, omitNewLineOpt);

        rootCommand.Invoke(args);
    }

    private static void Print(string text)
    {
        Console.WriteLine(text);
    }
}