// See https://aka.ms/new-console-template for more information
public interface ISearch
{
    ISearcher GetSearcher(); // Factory Method
    Task<IEnumerable<string>> SearchAsync();
}
