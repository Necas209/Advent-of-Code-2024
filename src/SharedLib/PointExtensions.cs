using System.Drawing;

namespace SharedLib;

public static class PointExtensions
{
    public static IEnumerable<Point> GetNeighbors(this Point point)
    {
        yield return point with { X = point.X - 1 };
        yield return point with { X = point.X + 1 };
        yield return point with { Y = point.Y - 1 };
        yield return point with { Y = point.Y + 1 };
    }
}