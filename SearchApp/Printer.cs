// See https://aka.ms/new-console-template for more information

[Serializable]
public class Printer
{
    public IPrint PrintStrategy; // Strategy
    public ISort OrderStrategy; // Strategy

    public void Print(List<string> items)
    {
        PrintStrategy.Print(OrderStrategy.Sort(items)); // Strategy
    }
}
