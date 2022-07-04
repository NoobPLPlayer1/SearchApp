// See https://aka.ms/new-console-template for more information
public abstract class AsyncSearcher : ISearcher
{
    public Action<string> OnFileFound { get; init; } // Observer
    public abstract Task SearchAsync(string directoryPath);
    protected async Task SearchAsync(string directoryPath, string fileSearch, string directorySearch)
    {
        try
        {
            foreach (var filePath in Directory.GetFiles(directoryPath, fileSearch))
            {
                OnFileFound(filePath); // Observer
            }
            List<Task> tasks = new();
            foreach (var dirPath in Directory.EnumerateDirectories(directoryPath, directorySearch))
            {
                if (Directory.Exists(dirPath))
                    tasks.Add(Task.Run(async () => await SearchAsync(dirPath)));
            }
            await Task.WhenAll(tasks);
        }
        catch
        {

        }
    }
}
