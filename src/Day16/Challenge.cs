using System.Collections.Immutable;

namespace Day16;

public static class Challenge
{
    public static int Part1(Maze maze)
    {
        var optimalCost = maze.FindAllPaths()
            .Min(Maze.ComputeCost);
        
        return optimalCost;
    }

    public static int Part2(Maze maze)
    {
        var allPaths = maze.FindAllPaths();

        var uniqueCells = allPaths
            .GroupBy(Maze.ComputeCost)
            .First()
            .SelectMany(x => x)
            .ToImmutableHashSet();

        return uniqueCells.Count;
    }
}