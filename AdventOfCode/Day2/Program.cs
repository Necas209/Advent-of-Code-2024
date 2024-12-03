using System.Collections.Immutable;
using System.Diagnostics;
using SharedLib;

const string test = """
                    7 6 4 2 1
                    1 2 7 8 9
                    9 7 6 2 1
                    1 3 2 4 5
                    8 6 4 4 1
                    1 3 6 7 9
                    """;

var testLines = test.Split(Environment.NewLine).ToImmutableArray();
Debug.Assert(GetSafetyCount(testLines) == 2);
Debug.Assert(GetSafetyCount(testLines, true) == 4);

var content = File.ReadLines("input.txt").ToImmutableArray();
Console.WriteLine($"Safe Reports: {GetSafetyCount(content)}");
Console.WriteLine($"Safe Reports (w/ skipping allowed): {GetSafetyCount(content, true)}");
return;

int GetSafetyCount(IEnumerable<string> lines, bool skippingAllowed = false)
{
    var safetyCount = 0;

    foreach (var report in lines)
    {
        var levels = report.Split(" ")
            .Select(int.Parse)
            .ToImmutableArray();

        var overallSafety = GetSafety(levels);
        if (skippingAllowed)
        {
            overallSafety = Enumerable.Range(0, levels.Length)
                .Select(i => levels.SkipAt(i))
                .Select(GetSafety)
                .Append(overallSafety)
                .Aggregate((a, b) => a | b);
        }

        if (overallSafety is not LevelSafety.Stagnant) safetyCount++;
    }

    return safetyCount;
}

LevelSafety GetSafety(IEnumerable<int> levels)
{
    var safety = LevelSafety.Increasing | LevelSafety.Decreasing;
    foreach (var (first, second) in levels.Pairwise())
    {
        safety &= first.CompareTo(second) switch
        {
            -1 => LevelSafety.Increasing,
            1 => LevelSafety.Decreasing,
            0 => LevelSafety.Stagnant,
            _ => throw new ArgumentOutOfRangeException()
        };

        if (Math.Abs(first - second) is < 1 or > 3) safety = LevelSafety.Stagnant;

        if (safety is LevelSafety.Stagnant) break;
    }

    return safety;
}

[Flags]
internal enum LevelSafety
{
    Stagnant = 0,
    Increasing = 1 << 0,
    Decreasing = 1 << 1
}