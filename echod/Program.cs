using System.CommandLine;
using System.CommandLine.Invocation;

namespace echod;

internal static class Program
{
    static async Task<int> Main(string[] args)
    {
        var fileOption = new Option<FileInfo?>(
            name: "--file",
            description: "The file to read and display on the console");

        var delayOption = new Option<int>(
            name: "--delay",
            description: "Delay to show between lines.",
            getDefaultValue: () => 42);

        var fgColorOption = new Option<ConsoleColor>(
            name: "--console-color",
            description: "Foreground color of the text.",
            getDefaultValue: () => ConsoleColor.White);

        var lightModeOption = new Option<bool>(
            name: "--light-mode",
            description: "Shows bright background instead of the default dark one.");

        var rootCommand = new RootCommand("Sample app for System.Commandline.");

        var readCommand = new Command(name: "read", description: "Reads the file specified")
        {
            fileOption,
            delayOption,
            fgColorOption,
            lightModeOption
        };

        rootCommand.AddCommand(readCommand);

        rdfadCommand.SetHandler (async (file, delay, fgColor, lightMode) =>
            {
                await ReadFile(file!, delay, fgColor, lightMode);
            },
            fileOption, delayOption, fgColorOption, lightModeOption);

        return rootCommand.InvokeAsync(args).Result;
    }

    internal static async Task ReadFile(FileInfo file, int delay, ConsoleColor fgColor, bool isLightMode)
    {
        Console.BackgroundColor = isLightMode ? ConsoleColor.White : ConsoleColor.Black;
        Console.ForegroundColor = fgColor;
        var lines = File.ReadLines(file.FullName).ToList();
        foreach (var line in lines)
        {
            Console.WriteLine(line);
            await Task.Delay(delay * line.Length);
        }
    }
}