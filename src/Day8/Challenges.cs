using System.Collections.Immutable;
using System.Diagnostics;
using System.Drawing;
using DotNetExtensions.Collections;

namespace Day8;

public static class Challenges
{
    public static int Part1(string input)
    {
        var map = new Map(input);

        var antennae = map.FindAntennae()
            .GroupBy(a => a.Frequency)
            .SelectMany(g => g.Combinations(2))
            .Select(g => g.ToImmutableArray());

        var antiNodes = antennae
            .SelectMany(c => SameDistanceAntiNodes(map, c[0], c[1]))
            .ToHashSet();

        return antiNodes.Count;
    }

    public static int Part2(string input)
    {
        var map = new Map(input);

        var antennae = map.FindAntennae()
            .GroupBy(a => a.Frequency)
            .SelectMany(g => g.Combinations(2))
            .Select(g => g.ToImmutableArray());

        var antiNodes = antennae
            .SelectMany(c => AllAntiNodes(map, c[0], c[1]))
            .ToHashSet();

        return antiNodes.Count;
    }

    private static IEnumerable<Point> SameDistanceAntiNodes(Map map, Antenna first, Antenna second)
    {
        Debug.Assert(first.Frequency == second.Frequency);

        var dx = first.X - second.X;
        var dy = first.Y - second.Y;

        var firstAntiNode = new Point(first.X + dx, first.Y + dy);
        if (map.Contains(firstAntiNode)) yield return firstAntiNode;

        var secondAntiNode = new Point(second.X - dx, second.Y - dy);
        if (map.Contains(secondAntiNode)) yield return secondAntiNode;
    }

    private static IEnumerable<Point> AllAntiNodes(Map map, Antenna first, Antenna second)
    {
        Debug.Assert(first.Frequency == second.Frequency);

        var dx = first.X - second.X;
        var dy = first.Y - second.Y;

        while (dx % 2 == 0 && dy % 2 == 0)
        {
            dx /= 2;
            dy /= 2;
        }

        yield return first.Position;
        for (var i = 1;; i++)
        {
            var antiNode = new Point(first.X + i * dx, first.Y + i * dy);
            if (!map.Contains(antiNode)) break;
            yield return antiNode;
        }

        yield return second.Position;
        for (var i = 1;; i++)
        {
            var antiNode = new Point(second.X - i * dx, second.Y - i * dy);
            if (!map.Contains(antiNode)) break;
            yield return antiNode;
        }
    }
}