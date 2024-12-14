using System.Drawing;

namespace SharedLib;

public static class PointExtensions
{
    public static (Point Left, Point Right, Point Top, Point Bottom) GetNeighbors(this Point point)
    {
        var left = point with { X = point.X - 1 };
        var right = point with { X = point.X + 1 };
        var top = point with { Y = point.Y - 1 };
        var bottom = point with { Y = point.Y + 1 };
        return (left, right, top, bottom);
    }
}