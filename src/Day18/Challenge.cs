using System.Collections.Immutable;
using System.Drawing;

namespace Day18;

public class Challenge
{
    private readonly Size _memorySize;
    private readonly int _numFallenBytes;
    private readonly ImmutableArray<Point> _fallenBytes;

    public Challenge(string input, Size memorySize, int numFallenBytes)
    {
        _memorySize = memorySize;
        _numFallenBytes = numFallenBytes;
        _fallenBytes =
        [
            ..input
                .Split(Environment.NewLine)
                .Select(x => x.Split(','))
                .Select(x => new Point(int.Parse(x[0]), int.Parse(x[1])))
        ];
    }

    public int Part1()
    {
        var memorySpace = new MemorySpace(_memorySize, _fallenBytes);
        memorySpace.Corrupt(_numFallenBytes);

        var bestPathLength = memorySpace
            .FindAllPaths()
            .Min();

        return bestPathLength;
    }

    public Point Part2()
    {
        var memorySpace = new MemorySpace(_memorySize, _fallenBytes);

        var numFallenBytes = _numFallenBytes;
        while (true)
        {
            memorySpace.Corrupt(numFallenBytes);

            var pathLength = memorySpace.FindFirstPath();
            if (pathLength is null)
            {
                return _fallenBytes[numFallenBytes - 1];
            }

            numFallenBytes++;
        }
    }
}