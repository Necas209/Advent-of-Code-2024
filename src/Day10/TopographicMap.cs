using System.Collections.Immutable;
using System.Drawing;

namespace Day10;

public class TopographicMap
{
    private readonly ImmutableArray<ImmutableArray<int>> _map;
    private readonly int _width;
    private readonly int _height;

    public TopographicMap(string input)
    {
        _map =
        [
            ..input
                .Split(Environment.NewLine)
                .Select(x => x
                    .Select(char.GetNumericValue).Select(d => (int)d)
                    .ToImmutableArray()
                )
        ];
        _width = _map[0].Length;
        _height = _map.Length;
    }

    private int this[Point point] => _map[point.Y][point.X];

    public IEnumerable<Point> FindTrailheads()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                if (_map[y][x] == 0)
                {
                    yield return new Point(x, y);
                }
            }
        }
    }

    private IEnumerable<Point> GetNeighbors(Point point)
    {
        var x = point.X;
        var y = point.Y;

        if (x > 0)
            yield return new Point(x - 1, y);

        if (x < _width - 1)
            yield return new Point(x + 1, y);

        if (y > 0)
            yield return new Point(x, y - 1);

        if (y < _height - 1)
            yield return new Point(x, y + 1);
    }

    public int GetScore(Point trailhead, bool allPaths)
    {
        var visited = new HashSet<Point>();
        var queue = new Queue<Point>();
        queue.Enqueue(trailhead);

        var score = 0;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var currentHeight = this[current];

            if (currentHeight == 9)
            {
                score++;
                continue;
            }

            foreach (var neighbor in GetNeighbors(current))
            {
                if (this[neighbor] != currentHeight + 1)
                    continue;

                if (allPaths || visited.Add(neighbor))
                {
                    queue.Enqueue(neighbor);
                }
            }
        }

        return score;
    }
}