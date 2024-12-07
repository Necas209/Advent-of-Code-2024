using System.Collections;
using System.Drawing;

namespace Day6;

public class Map : IEnumerable<char>, ICloneable
{
    private readonly char[] _cells;
    private readonly int _width;
    private readonly int _height;

    public const char Obstruction = '#';
    public const char Empty = '.';
    public const char Visited = 'X';

    public Map(string map)
    {
        _cells = map.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .SelectMany(x => x)
            .ToArray();
        _width = map.IndexOf('\n');
        _height = _cells.Length / _width;
    }

    private Map(char[] cells, int width, int height)
    {
        _cells = cells;
        _width = width;
        _height = height;
    }

    public char this[Point point]
    {
        get => _cells[IndexOf(point)];
        set => _cells[IndexOf(point)] = value;
    }

    private int IndexOf(Point point) => point.Y * _width + point.X;

    public Point PointOf(int index) => new(index % _width, index / _width);

    public bool Contains(Point point) =>
        point.X >= 0 && point.X < _width && point.Y >= 0 && point.Y < _height;

    public Guard FindGuard()
    {
        var cell = _cells.Index()
            .FirstOrDefault(t => Guard.Directions.Contains(t.Item));
        return new Guard(PointOf(cell.Index), cell.Item);
    }

    public void Update(Map map) => map._cells.CopyTo(_cells, 0);

    public IEnumerator<char> GetEnumerator() => _cells.AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public object Clone()
    {
        var map = new char[_cells.Length];
        _cells.CopyTo(map, 0);
        return new Map(map, _width, _height);
    }
}