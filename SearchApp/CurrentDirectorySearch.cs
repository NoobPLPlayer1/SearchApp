// See https://aka.ms/new-console-template for more information
[Serializable]
public class CurrentDirectorySearch : Search
{
    public string Filter;

    public static CurrentDirectorySearch FromUser()
    {
        Console.Clear();
        return new()
        {
            Filter = ConsoleExtension.Prompt("File filter... "),
        };
    }

    public override ISearcher CreateSearcher()
    {
        return new CurrentDirectortSearcher()
        {
            FileFilter = Filter,
            OnFileFound = Results.Add,
        };
    }
}