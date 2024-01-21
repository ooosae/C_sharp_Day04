namespace d04.Model;

public interface ISearchable
{
    string Title { get; }
}

public static class MediaSearcher
{
    public static T[] Search<T>(this IEnumerable<T> list, string search)
        where T : ISearchable
    {
        var searchResults = list
            .Where(item => item.Title.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
            .OrderBy(item => item.Title)
            .ToArray();

        return searchResults;
    }
    
    public static T? FindBest<T>(IEnumerable<T> list, Func<T, int> rankSelector)
    {
        return list.OrderByDescending(rankSelector).FirstOrDefault();
    }
}