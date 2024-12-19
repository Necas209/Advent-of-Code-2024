using System.Collections.Immutable;

namespace Day19;

public static class Challenge
{
    public static int Part1(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var patterns = lines[0].Split(", ").ToImmutableArray();
        var designs = lines[1..].ToImmutableArray();

        var memo = new Dictionary<string, bool>();
        return designs.Count(design => CanFormDesign(design, patterns, memo));
    }

    public static long Part2(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var patterns = lines[0].Split(", ").ToImmutableArray();
        var designs = lines[1..].ToImmutableArray();

        var memo = new Dictionary<string, long>();
        return designs.Sum(design => CountWays(design, patterns, memo));
    }

    private static bool CanFormDesign(string design, ImmutableArray<string> patterns, Dictionary<string, bool> memo)
    {
        if (memo.TryGetValue(design, out var result))
            return result;

        // If the design is empty, we've successfully constructed it
        if (design.Length == 0)
            return true;

        foreach (var remainingDesign in patterns
                     .Where(design.StartsWith)
                     .Select(pattern => design[pattern.Length..]))
        {
            if (!memo.TryGetValue(remainingDesign, out var canFormRemainingDesign))
            {
                canFormRemainingDesign = CanFormDesign(remainingDesign, patterns, memo);
                memo[remainingDesign] = canFormRemainingDesign;
            }

            if (canFormRemainingDesign)
                return true;
        }

        memo[design] = false;
        return false;
    }

    private static long CountWays(string design, ImmutableArray<string> patterns, Dictionary<string, long> memo)
    {
        if (memo.TryGetValue(design, out var result))
            return result;

        // If the design is empty, there is exactly one way to form it (by doing nothing)
        if (design.Length == 0)
            return 1;

        var totalWays = 0L;
        foreach (var remainingDesign in patterns
                     .Where(design.StartsWith)
                     .Select(pattern => design[pattern.Length..]))
        {
            if (!memo.TryGetValue(remainingDesign, out var ways))
            {
                ways = CountWays(remainingDesign, patterns, memo);
                memo[remainingDesign] = ways;
            }

            totalWays += ways;
        }

        memo[design] = totalWays;
        return totalWays;
    }
}