using System.Drawing;

namespace Day8;

public readonly record struct Antenna(char Frequency, Point Position)
{
    public int X => Position.X;

    public int Y => Position.Y;
}

public class Map
{
    private readonly char[] _cells;
    private readonly int _width;
    private readonly int _height;

    private const char Empty = '.';

    public Map(string map)
    {
        _cells = map.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .SelectMany(x => x)
            .ToArray();
        _width = map.IndexOf('\n');
        _height = _cells.Length / _width;
    }

    public bool Contains(Point point) =>
        point.X >= 0 && point.X < _width && point.Y >= 0 && point.Y < _height;

    public IEnumerable<Antenna> FindAntennae()
    {
        foreach (var (index, cell) in _cells.Index().Where(t => t.Item != Empty))
        {
            yield return new Antenna(cell, new Point(index % _width, index / _width));
        }
    }
}