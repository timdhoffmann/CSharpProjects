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
        var fileOption = new Option<FileInfo?>(
            name: "--file",
            description: "The file to read and display on the console");

        var rootCommand = new RootCommand("An 'echo' clone written in C#/.NET.");
        rootCommand.AddOption(fileOption);

        rootCommand.SetHandler ((file) =>
            {
                ReadFile(file!);
            },
            fileOption);

        rootCommand.Invoke(args);
    }

    internal static void ReadFile(FileInfo file)
    {
        Console.WriteLine("test");
    }
}