// See https://aka.ms/new-console-template for more information
static class ConsoleExtension
{
    public static string Prompt(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
}
