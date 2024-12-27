using System.Collections.Immutable;
using System.Drawing;
using DotNetExtensions;

namespace Day12;

public class GardenPlotMap
{
    private readonly ImmutableArray<char> _map;
    private readonly int _width;
    private readonly int _height;

    public GardenPlotMap(string input)
    {
        _map = [..input.Split(Environment.NewLine).SelectMany(line => line)];
        _width = input.IndexOf('\n');
        _height = _map.Length / _width;
    }

    private int IndexOf(Point point) => point.Y * _width + point.X;

    private Point PointOf(int index) => new(index % _width, index / _width);

    private bool Contains(Point point) =>
        point.X >= 0 && point.X < _width && point.Y >= 0 && point.Y < _height;

    public List<GardenPlot> GetGardenPlots()
    {
        var visited = new HashSet<Point>();
        var plots = new List<GardenPlot>();

        for (var i = 0; i < _map.Length; i++)
        {
            var startPoint = PointOf(i);
            if (visited.Contains(startPoint)) continue;
            var character = _map[i];
            var region = FloodFill(startPoint, character, visited);
            if (region.Count > 0)
            {
                plots.Add(new GardenPlot(region.ToImmutableHashSet()));
            }
        }

        return plots;
    }

    private HashSet<Point> FloodFill(Point start, char targetChar, HashSet<Point> visited)
    {
        var region = new HashSet<Point>();
        var queue = new Queue<Point>();

        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (visited.Contains(current) || !Contains(current) || _map[IndexOf(current)] != targetChar)
            {
                continue;
            }

            visited.Add(current);
            region.Add(current);

            // Add neighbors to the queue
            var (left, right, top, bottom) = current.GetNeighbors();
            if (Contains(left) && !visited.Contains(left))
            {
                queue.Enqueue(left);
            }
            
            if (Contains(right) && !visited.Contains(right))
            {
                queue.Enqueue(right);
            }
            if (Contains(top) && !visited.Contains(top))
            {
                queue.Enqueue(top);
            }
            if (Contains(bottom) && !visited.Contains(bottom))
            {
                queue.Enqueue(bottom);
            }
        }

        return region;
    }
}