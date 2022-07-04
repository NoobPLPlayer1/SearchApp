// See https://aka.ms/new-console-template for more information
[Serializable]
public class FileDateSort : ISort, IComparer<string> // Strategy
{
    public int Compare(string? x, string? y)
    {
        try
        {
            return File.GetLastAccessTime(x) > File.GetLastAccessTime(y) ? 1 : -1;
        }
        catch { 
            return -1; 
        }
    }

    public IEnumerable<string> Sort(List<string> items)
    {
        items.Sort(this);
        return items;
    }
}
