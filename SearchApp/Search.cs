// See https://aka.ms/new-console-template for more information
[Serializable]
public abstract class Search : ISearch
{
    public List<string> Results { get; } = new();
    public abstract ISearcher GetSearcher();

    public async Task<IEnumerable<string>> SearchAsync()
    {
        ISearcher searcher = GetSearcher();
        await searcher.SearchAsync(Directory.GetCurrentDirectory());
        return Results;
    }
}
