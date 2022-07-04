// See https://aka.ms/new-console-template for more information
public class CurrentDirectortSearcher : ISearcher
{
    public string FileFilter { get; init; }
    public Action<string> OnFileFound { get; init; } // Observer

    public async Task SearchAsync(string directoryPath)
    {
        try
        {
            foreach (var filePath in Directory.GetFiles(directoryPath, FileFilter))
            {
                OnFileFound(filePath); // Observer
            }
        }
        catch
        {

        }
    }
}
