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

    public static IEnumerable<(T First, T Second, T Third, T Fourth, T Fifth)> Quintuples<T>(this IEnumerable<T> source)
    {
        ArgumentNullException.ThrowIfNull(source);

        using var enumerator = source.GetEnumerator();

        // Buffer the first five elements
        if (!enumerator.MoveNext()) yield break;
        var first = enumerator.Current;

        if (!enumerator.MoveNext()) yield break;
        var second = enumerator.Current;

        if (!enumerator.MoveNext()) yield break;
        var third = enumerator.Current;

        if (!enumerator.MoveNext()) yield break;
        var fourth = enumerator.Current;

        if (!enumerator.MoveNext()) yield break;
        var fifth = enumerator.Current;

        while (true)
        {
            yield return (first, second, third, fourth, fifth);

            // Slide the window forward
            first = second;
            second = third;
            third = fourth;
            fourth = fifth;

            if (!enumerator.MoveNext())
                yield break;

            fifth = enumerator.Current;
        }
    }

    public static IEnumerable<T[]> Adjacent<T>(this IEnumerable<T> source, int length)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentOutOfRangeException.ThrowIfNegative(length);

        return AdjacentInternal(source, length);
    }

    private static IEnumerable<T[]> AdjacentInternal<T>(IEnumerable<T> source, int length)
    {
        var array = source as T[] ?? source.ToArray();
        if (length > array.Length)
            yield break;

        for (var i = 0; i <= array.Length - length; i++)
        {
            var segment = new T[length];
            Array.Copy(array, i, segment, 0, length);
            yield return segment;
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

    public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> source, int length)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentOutOfRangeException.ThrowIfNegative(length);
        return CombinationsInternal(source, length);
    }

    private static IEnumerable<IEnumerable<T>> CombinationsInternal<T>(IEnumerable<T> source, int length)
    {
        var array = source as T[] ?? source.ToArray();

        if (length == 0)
        {
            yield return [];
            yield break;
        }

        if (length > array.Length)
            yield break;

        // Stack to hold the current state for iteration
        var stack = new Stack<(int Index, List<T> Combination)>();
        stack.Push((-1, []));

        while (stack.Count > 0)
        {
            var (index, combination) = stack.Pop();

            // If the combination is complete, yield it
            if (combination.Count == length)
            {
                yield return combination;
                continue;
            }

            // Push next elements onto the stack in reverse order to ensure correct order
            for (var i = array.Length - 1; i > index; i--)
            {
                var newCombination = new List<T>(combination) { array[i] };
                stack.Push((i, newCombination));
            }
        }
    }
}