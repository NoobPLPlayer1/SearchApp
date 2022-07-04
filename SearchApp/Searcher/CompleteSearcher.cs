// See https://aka.ms/new-console-template for more information
public class CompleteSearcher : AsyncSearcher
{
    public string FileFilter { get; init; }
    public string DirectoryFilter { get; init; }

    public override async Task SearchAsync(string directoryPath) => await base.SearchAsync(directoryPath, FileFilter, DirectoryFilter);
}
