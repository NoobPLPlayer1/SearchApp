// See https://aka.ms/new-console-template for more information
[Serializable]
public class ConsolePrint : IPrint // Strategy
{
    public static ConsolePrint Instance { get; } = new(); // Singleton
    private ConsolePrint() { } // Singleton
    private object mutex = new();
    public void Print(IEnumerable<string> items)
    {
        lock (mutex) // Vi vill inte skriva två olika listor samtidigt
        {
            foreach (var item in items)
                Console.WriteLine(item);
        }
    }
}