// See https://aka.ms/new-console-template for more information
public class FileSearcher : AsyncSearcher
{
    public string FileName { get; init; }

    public override async Task SearchAsync(string directoryPath) => await base.SearchAsync(directoryPath, $"{FileName}", "*");
}
