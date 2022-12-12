public static class Grouper
{
    public static IEnumerable<IEnumerable<T>> SplitBy<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        var outputSet = new List<T>();
        foreach(T item in source)
        {
            if (predicate(item)){
                yield return outputSet;
                outputSet = new List<T>();
            }
            else
                outputSet.Add(item);
        }
        yield return outputSet;
    }
    public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach(T item in source)
        {
            yield return item;
            if (!predicate(item)) break;
        }
    }
}
