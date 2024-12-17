using System.Drawing;

namespace Day16;

public enum Orientation
{
    Up,
    Down,
    Left,
    Right
}

public readonly record struct Reindeer(Point Position, Orientation Orientation) : IComparable<Reindeer>
{
    public int CompareTo(Reindeer other)
    {
        if (Position != other.Position)
            return Position.X != other.Position.X
                ? Position.X.CompareTo(other.Position.X)
                : Position.Y.CompareTo(other.Position.Y);
        return Orientation.CompareTo(other.Orientation);
    }
}