using System.CommandLine;
using System.CommandLine.Invocation;

namespace echod;

internal static class Program
{
    /// <summary>
    /// A clone of the 'echo' Unix utility. Prints its arguments to the console.
    /// </summary>
    /// <param name="args">Strings to be printed.</param>
    /// <returns></returns>
    static void Main(string[] args)
    {

        // TODO: Make this arg required with a minimum of 1 token/values.
        var textArg = new Argument<String>
        {
            Description = "The text to print to the console.",
            Name = "TEXT"
        };

        // var newLineOption = new Option<String>(
        //     name: "--no-newline",
        //     description: "Omit new line at the end.");

        var rootCommand = new RootCommand("An 'echo' clone written in C#/.NET.");
        rootCommand.AddArgument(textArg);

        rootCommand.SetHandler((text) =>
            {
                Print(text!);
            },
            textArg);

        rootCommand.Invoke(args);
    }

    internal static void Print(String text)
    {
        Console.WriteLine(text);
    }
}