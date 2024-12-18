using System.Collections.Immutable;
using System.Drawing;

namespace Day18;

public class MemorySpace
{
    private readonly ImmutableArray<Point> _fallenBytes;
    private readonly char[,] _memorySpace;

    private static readonly ImmutableArray<Size> Moves =
    [
        new(1, 0), // Move right
        new(-1, 0), // Move left
        new(0, 1), // Move down
        new(0, -1) // Move up
    ];

    public MemorySpace(Size size, ImmutableArray<Point> fallenBytes)
    {
        _fallenBytes = fallenBytes;
        _memorySpace = new char[size.Height, size.Width];
        Reset();
    }

    private int Width => _memorySpace.GetLength(1);

    private int Height => _memorySpace.GetLength(0);

    private void Reset()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                _memorySpace[y, x] = '.';
            }
        }
    }

    private bool IsVisitable(Point point)
    {
        return point.X >= 0
               && point.X < Width
               && point.Y >= 0
               && point.Y < Height
               && _memorySpace[point.Y, point.X] != '#';
    }

    public void Corrupt(int numFallenBytes)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(numFallenBytes, _fallenBytes.Length);

        Reset();
        foreach (var fallenByte in _fallenBytes.Take(numFallenBytes))
        {
            _memorySpace[fallenByte.Y, fallenByte.X] = '#';
        }
    }

    public int? FindFirstPath()
    {
        var queue = new Queue<(Point Position, int Steps)>();
        var visited = new HashSet<Point>();

        var start = Point.Empty;
        var end = new Point(Width - 1, Height - 1);

        // Initialize BFS queue with the start position
        queue.Enqueue((start, 0));
        visited.Add(start);

        while (queue.Count > 0)
        {
            var (current, steps) = queue.Dequeue();

            // If we reached the end, return the number of steps
            if (current == end)
                return steps;

            // Explore all possible moves
            foreach (var move in Moves)
            {
                var newPosition = current + move;

                // Skip invalid positions or already visited positions
                if (!IsVisitable(newPosition) || visited.Contains(newPosition))
                    continue;

                queue.Enqueue((newPosition, steps + 1));
                visited.Add(newPosition); // Mark as visited
            }
        }

        // If no path was found, return null
        return null;
    }

    public IEnumerable<int> FindAllPaths()
    {
        var priorityQueue = new SortedSet<(int Cost, Point point)>(
            Comparer<(int Cost, Point Point)>.Create(
                (a, b) =>
                    a.Cost != b.Cost
                        ? a.Cost.CompareTo(b.Cost)
                        : a.Point.X != b.Point.X
                            ? a.Point.X.CompareTo(b.Point.X)
                            : a.Point.Y.CompareTo(b.Point.Y)
            )
        );
        var memo = new Dictionary<Point, int>();

        // Initialize the queue with the start position
        var start = Point.Empty;
        var end = new Point(Width - 1, Height - 1);

        priorityQueue.Add((0, start));
        memo[start] = 0;

        while (priorityQueue.Count > 0)
        {
            var (currCost, currPos) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);

            // If we reached the end, reconstruct the path
            if (currPos == end)
            {
                yield return currCost;
                continue;
            }

            // Explore all possible moves
            foreach (var move in Moves)
            {
                var newPosition = currPos + move;
                if (!IsVisitable(newPosition))
                    continue;

                var newCost = currCost + 1;
                if (memo.TryGetValue(newPosition, out var memoCost) && newCost >= memoCost)
                    continue;

                memo[newPosition] = newCost;
                priorityQueue.Add((newCost, newPosition));
            }
        }
    }
}