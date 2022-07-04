// See https://aka.ms/new-console-template for more information
[Serializable]
public class FileSearch : Search
{
    public string Name;
    public string Type;

    public static FileSearch FromUser()
    {
        Console.Clear();
        return new()
        {
            Name = ConsoleExtension.Prompt("File name filter... "),
            Type = ConsoleExtension.Prompt("File type filter... "),
        };
    }

    public override ISearcher GetSearcher()
    {
        return new FileSearcher()
        {
            FileName = $"{Name}.{Type}",
            OnFileFound = Results.Add,
        };
    }
}
