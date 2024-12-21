using System.Collections.Immutable;
using System.Drawing;
using SharedLib;

namespace Day20;

public class RaceTrack
{
    private readonly char[,] _map;
    private readonly Point _start;
    private readonly Point _end;

    private static readonly ImmutableArray<Size> Moves =
    [
        new(1, 0), // Move right
        new(-1, 0), // Move left
        new(0, 1), // Move down
        new(0, -1) // Move up
    ];

    public RaceTrack(string input)
    {
        var lines = input.Split(Environment.NewLine);

        var width = lines[0].Length;
        var height = lines.Length;

        _map = new char[width, height];
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                _map[x, y] = lines[y][x];
                switch (_map[x, y])
                {
                    case 'S':
                        _start = new Point(x, y);
                        break;
                    case 'E':
                        _end = new Point(x, y);
                        break;
                }
            }
        }
    }

    private Size Size => new(_map.GetLength(0), _map.GetLength(1));

    public ImmutableArray<Point> FindShortestPath(uint maxCost = uint.MaxValue)
    {
        var priorityQueue = new SortedSet<(uint Cost, Point Point)>(
            Comparer<(uint Cost, Point Point)>.Create(
                (a, b) =>
                    a.Cost != b.Cost
                        ? a.Cost.CompareTo(b.Cost)
                        : a.Point.X != b.Point.X
                            ? a.Point.X.CompareTo(b.Point.X)
                            : a.Point.Y.CompareTo(b.Point.Y)
            )
        );

        var memo = new Dictionary<Point, uint>();
        var predecessors = new Dictionary<Point, Point>();

        // Initialize the queue with the start position
        priorityQueue.Add((0, _start));
        memo[_start] = 0;

        while (priorityQueue.Count > 0)
        {
            var (currCost, currPos) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);

            if (currCost > maxCost)
                continue;

            // If we reached the end, reconstruct the path
            if (currPos == _end)
            {
                var path = new List<Point>();
                var current = currPos;
                while (predecessors.ContainsKey(current))
                {
                    path.Add(current);
                    current = predecessors[current];
                }

                path.Add(_start); // Add the start point
                path.Reverse(); // Reverse to get the path from start to end
                return [..path];
            }

            // Explore all possible moves
            foreach (var move in Moves)
            {
                var newPosition = currPos + move;
                var isValid = new Rectangle(Point.Empty, Size).Contains(newPosition);
                if (!isValid || _map[newPosition.X, newPosition.Y] == '#')
                    continue;

                var newCost = currCost + 1;
                if (memo.TryGetValue(newPosition, out var memoCost) && newCost >= memoCost)
                    continue;

                memo[newPosition] = newCost;
                predecessors[newPosition] = currPos; // Track how we got to `newPosition`
                priorityQueue.Add((newCost, newPosition));
            }
        }

        throw new InvalidOperationException("Could not find a path to the end");
    }

    public int GetCheats(int minSaved, uint maxCheatLength = 2)
    {
        var shortestPath = FindShortestPath();

        var shortcuts = new HashSet<(int, int)>();

        // Iterate over all points along the shortest path
        for (var i = 0; i < shortestPath.Length; i++)
        {
            var point = shortestPath[i];

            // Check all cheat lengths from 2 to `maxCheatLength`
            for (var cheatLength = 2; cheatLength <= maxCheatLength; cheatLength++)
            {
                // Iterate over all potential end points for the cheat
                for (var j = i + 1 + minSaved; j < shortestPath.Length; j++)
                {
                    var otherPoint = shortestPath[j];
                    var distance = point.ManhattanDistance(otherPoint);

                    // If the cheat can bridge the distance, count it
                    if (distance > cheatLength)
                        continue;

                    // Ensure the cheat saves enough time and is unique
                    if (j - i - distance < minSaved)
                        continue;

                    shortcuts.Add((i, j));
                }
            }
        }

        return shortcuts.Count;
    }
}