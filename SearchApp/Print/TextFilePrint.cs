// See https://aka.ms/new-console-template for more information
[Serializable]
public class TextFilePrint : IPrint // Strategy
{
    public string OutputPath = "output.txt";
    public void Print(IEnumerable<string> items)
    {
        File.WriteAllLines(OutputPath, items);
    }
}
