using System.CommandLine;

namespace echod;

public static class Program
{

    // TODO: p. 51 - next: write helper function.

    /// <summary>
    /// A clone of the 'echo' Unix utility. Prints its arguments to the console.
    /// </summary>
    /// <param name="args">Strings to be printed.</param>
    /// <returns></returns>
    public static void Main(string[] args)
    {

        var textArg = new Argument<string[]>
        {
            Description = "The text to print to the console.",
            Name = "TEXT",
            Arity = ArgumentArity.OneOrMore
        };

        var omitNewLineOpt = new Option<bool>(
            description: "If present, omit new line at the end.",
            name: "-n");

        var rootCommand = new RootCommand("An 'echo' clone written in C#/.NET.");
        rootCommand.AddArgument(textArg);
        rootCommand.AddOption(omitNewLineOpt);

        rootCommand.SetHandler((text, omitNewLineOpt) =>
            {
                var output = string.Join(" ", text);
                output = (omitNewLineOpt) ? output : $"{output}{Environment.NewLine}";
                Console.Write(output);
            },
            textArg, omitNewLineOpt);

        rootCommand.Invoke(args);
    }

    private static void Print(string text)
    {
        Console.WriteLine(text);
    }
}