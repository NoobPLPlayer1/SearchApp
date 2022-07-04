// See https://aka.ms/new-console-template for more information
[Serializable]
public class DontCareSort : ISort // Strategy
{
    public IEnumerable<string> Sort(List<string> items)
    {
        return items;
    }
}
