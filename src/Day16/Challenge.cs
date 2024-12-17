using System.Collections.Immutable;

namespace Day16;

public static class Challenge
{
    public static int Part1(string input)
    {
        var maze = new Maze(input);

        var optimalCost = maze.FindAllPaths()
            .Min(Maze.ComputeCost);

        return optimalCost;
    }

    public static int Part2(string input)
    {
        var maze = new Maze(input);

        var allPaths = maze.FindAllPaths();

        var optimalPaths = allPaths
            .GroupBy(Maze.ComputeCost)
            .OrderBy(g => g.Key)
            .First();

        var uniqueCells = optimalPaths
            .SelectMany(path => path)
            .ToImmutableHashSet();

        return uniqueCells.Count;
    }
}