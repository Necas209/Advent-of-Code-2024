using System.Collections.Immutable;
using System.Drawing;
using DotNetExtensions.Core;

namespace Day20;

public class RaceTrack
{
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

        var map = new char[width, height];
        var start = Point.Empty;
        var end = Point.Empty;
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                map[x, y] = lines[y][x];
                switch (map[x, y])
                {
                    case 'S':
                        start = new Point(x, y);
                        break;
                    case 'E':
                        end = new Point(x, y);
                        break;
                }
            }
        }

        Path = FindOnlyPath(map, start, end);
    }

    public ImmutableArray<Point> Path { get; }

    private static ImmutableArray<Point> FindOnlyPath(char[,] map, Point start, Point end)
    {
        var bounds = new Rectangle(0, 0, map.GetLength(0), map.GetLength(1));
        var path = new List<Point> { start };

        while (path[^1] != end)
        {
            var last = path[^1];
            foreach (var next in Moves
                         .Select(move => last + move)
                         .Where(next => bounds.Contains(next))
                         .Where(next => map[next.X, next.Y] != '#'))
            {
                map[last.X, last.Y] = '#';
                path.Add(next);
                break;
            }
        }

        return [..path];
    }

    public int GetCheats(int minSaved, int maxCheatLength = 2)
    {
        // Iterate over all points along the shortest path
        var cheats = 0;
        for (var i = 0; i < Path.Length - 2; i++)
        {
            var point = Path[i];

            // Iterate over all potential end points for the cheat
            for (var j = i + minSaved; j < Path.Length; j++)
            {
                var otherPoint = Path[j];
                var distance = point.ManhattanDistance(otherPoint);

                // Ensure the cheat saves enough time and is unique
                if (j - i - distance >= minSaved && distance <= maxCheatLength)
                {
                    cheats++;
                }
            }
        }

        return cheats;
    }
}