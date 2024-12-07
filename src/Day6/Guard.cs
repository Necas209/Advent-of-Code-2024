using System.Collections.Immutable;
using System.Drawing;

namespace Day6;

public readonly record struct Guard(Point Position, char Direction)
{
    private const char Up = '^';
    private const char Down = 'v';
    private const char Left = '<';
    private const char Right = '>';

    public static ImmutableArray<char> Directions => [Up, Down, Left, Right];

    public Guard Advance() =>
        this with
        {
            Position = Direction switch
            {
                Up => Position with { Y = Position.Y - 1 },
                Down => Position with { Y = Position.Y + 1 },
                Left => Position with { X = Position.X - 1 },
                Right => Position with { X = Position.X + 1 },
                _ => throw new ArgumentOutOfRangeException(nameof(Direction), Direction, null)
            }
        };

    public Guard TurnRight() =>
        this with
        {
            Direction = Direction switch
            {
                Up => Right,
                Right => Down,
                Down => Left,
                Left => Up,
                _ => throw new ArgumentOutOfRangeException(nameof(Direction), Direction, null)
            }
        };
}