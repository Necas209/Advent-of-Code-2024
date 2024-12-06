using System.Collections.Immutable;
using SharedLib;

namespace Day2;

public static class Challenges
{
    [Flags]
    private enum LevelSafety
    {
        Stagnant = 0,
        Increasing = 1 << 0,
        Decreasing = 1 << 1
    }

    public static ImmutableArray<ImmutableArray<int>> ProcessData(string filePath)
    {
        return
        [
            ..File.ReadLines(filePath)
                .Select(l => l.Split(' ')
                    .Select(int.Parse)
                    .ToImmutableArray())
        ];
    }

    public static int FirstChallenge(ImmutableArray<ImmutableArray<int>> reports)
    {
        return reports
            .Select(report => GetSafety(report))
            .Count(overallSafety => overallSafety is not LevelSafety.Stagnant);
    }

    public static int SecondChallenge(ImmutableArray<ImmutableArray<int>> reports)
    {
        return reports
            .Select(report => Enumerable.Range(0, report.Length)
                .Select(i => GetSafety(report.SkipAt(i)))
                .Append(GetSafety(report))
                .Aggregate((a, b) => a | b))
            .Count(overallSafety => overallSafety is not LevelSafety.Stagnant);
    }

    private static LevelSafety GetSafety(IEnumerable<int> levels)
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

            if (Math.Abs(first - second) is < 1 or > 3)
            {
                safety = LevelSafety.Stagnant;
            }

            if (safety is LevelSafety.Stagnant) break;
        }

        return safety;
    }
}