namespace SharedLib;

public static class EnumerableExtensions
{
    public static IEnumerable<(T First, T Second)> Pairwise<T>(this IEnumerable<T> source)
    {
        ArgumentNullException.ThrowIfNull(source);

        using var enumerator = source.GetEnumerator();
        if (!enumerator.MoveNext()) yield break;

        var previous = enumerator.Current;
        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;
            yield return (previous, current);
            previous = current;
        }
    }

    public static IEnumerable<T> SkipAt<T>(this IEnumerable<T> source, int index)
    {
        ArgumentNullException.ThrowIfNull(source);

        var i = 0;
        foreach (var item in source)
        {
            if (i++ == index) continue;
            yield return item;
        }
    }
}