using System.Collections.Immutable;
using System.Drawing;

namespace Day18;

public static class Challenge
{
    public static int Part1(string input, Size memorySize, int numFallenBytes)
    {
        var fallenBytes = input
            .Split(Environment.NewLine)
            .Select(x => x.Split(','))
            .Select(x => new Point(int.Parse(x[0]), int.Parse(x[1])))
            .ToImmutableArray();

        var memorySpace = new MemorySpace(memorySize, fallenBytes);
        memorySpace.Corrupt(numFallenBytes);

        var bestPathLength = memorySpace
            .FindAllPaths()
            .Min();

        return bestPathLength;
    }

    public static Point Part2(string input, Size memorySize)
    {
        var fallenBytes = input
            .Split(Environment.NewLine)
            .Select(x => x.Split(','))
            .Select(x => new Point(int.Parse(x[0]), int.Parse(x[1])))
            .ToImmutableArray();
        
        var memorySpace = new MemorySpace(memorySize, fallenBytes);

        var numFallenBytes = 1;
        while (true)
        {
            memorySpace.Corrupt(numFallenBytes);

            var hasPath = memorySpace.FindAllPaths().Any();
            if (!hasPath)
            {
                return fallenBytes[numFallenBytes - 1];
            }

            numFallenBytes++;
        }
    }
}