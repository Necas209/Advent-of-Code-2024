using System.Collections.Immutable;
using System.Drawing;

namespace Day15;

public enum Move
{
    Up = '^',
    Down = 'v',
    Left = '<',
    Right = '>'
}

public class Warehouse(char[][] map, ImmutableArray<Move> moves)
{
    public const char Empty = '.';
    private const char Wall = '#';
    public const char Box = 'O';
    public const char Robot = '@';
    public const char WideBoxLeft = '[';
    public const char WideBoxRight = ']';

    private char this[Point point]
    {
        get => map[point.Y][point.X];
        set => map[point.Y][point.X] = value;
    }

    private Point FindRobotPosition()
    {
        var cell = map
            .SelectMany((row, y) => row
                .Select((c, x) => (c, x, y)))
            .First(x => x.c == Robot);

        return new Point(cell.x, cell.y);
    }

    public IEnumerable<Point> FindBoxes()
    {
        return map
            .SelectMany((row, y) => row
                .Select((c, x) => (c, x, y)))
            .Where(x => x.c is Box or WideBoxLeft)
            .Select(x => new Point(x.x, x.y));
    }

    public static int GetGpsCoordinate(Point point)
    {
        return point.Y * 100 + point.X;
    }

    public void Run()
    {
        var robotPosition = FindRobotPosition();

        foreach (var move in moves)
        {
            var newPosition = move switch
            {
                Move.Up => robotPosition with { Y = robotPosition.Y - 1 },
                Move.Down => robotPosition with { Y = robotPosition.Y + 1 },
                Move.Left => robotPosition with { X = robotPosition.X - 1 },
                Move.Right => robotPosition with { X = robotPosition.X + 1 },
                _ => throw new ArgumentOutOfRangeException(nameof(move), move, null)
            };

            switch (this[newPosition])
            {
                case Wall:
                    continue;
                case Box:
                    if (!CanPushBox(newPosition, move))
                        continue;

                    PushBox(newPosition, move);
                    break;
                case WideBoxLeft:
                    var checkRight = move is not (Move.Left or Move.Right);
                    var rightSide = newPosition with { X = newPosition.X + 1 };
                    if (!CanPushBox(newPosition, move) || (checkRight && !CanPushBox(rightSide, move)))
                        continue;

                    PushBox(newPosition, move);
                    if (checkRight)
                    {
                        PushBox(rightSide, move);
                    }

                    break;
                case WideBoxRight:
                    var checkLeft = move is not (Move.Left or Move.Right);
                    var leftSide = newPosition with { X = newPosition.X - 1 };
                    if (!CanPushBox(newPosition, move) || (checkLeft && !CanPushBox(leftSide, move)))
                        continue;

                    PushBox(newPosition, move);
                    if (checkLeft)
                    {
                        PushBox(leftSide, move);
                    }

                    break;
                case Empty:
                    break;
            }

            this[robotPosition] = Empty;
            this[newPosition] = Robot;
            robotPosition = newPosition;
        }
    }

    private bool CanPushBox(Point boxPosition, Move move)
    {
        var stack = new Stack<Point>();
        stack.Push(boxPosition);

        while (stack.Count > 0)
        {
            var currBoxPos = stack.Pop();
            var newBoxPos = move switch
            {
                Move.Up => currBoxPos with { Y = currBoxPos.Y - 1 },
                Move.Down => currBoxPos with { Y = currBoxPos.Y + 1 },
                Move.Left => currBoxPos with { X = currBoxPos.X - 1 },
                Move.Right => currBoxPos with { X = currBoxPos.X + 1 },
                _ => throw new ArgumentOutOfRangeException(nameof(move), move, null)
            };

            switch (this[newBoxPos])
            {
                case Box:
                    stack.Push(newBoxPos);
                    continue;
                case WideBoxLeft:
                    stack.Push(newBoxPos);
                    if (move is not (Move.Left or Move.Right))
                    {
                        var rightSide = newBoxPos with { X = newBoxPos.X + 1 };
                        stack.Push(rightSide);
                    }

                    break;
                case WideBoxRight:
                    stack.Push(newBoxPos);
                    if (move is not (Move.Left or Move.Right))
                    {
                        var leftSide = newBoxPos with { X = newBoxPos.X - 1 };
                        stack.Push(leftSide);
                    }

                    break;
                case Wall:
                    return false;
            }
        }

        return true;
    }

    private void PushBox(Point boxPosition, Move move)
    {
        if (this[boxPosition] is not (Box or WideBoxLeft or WideBoxRight))
            throw new InvalidOperationException("Invalid box position");

        var newBoxPosition = move switch
        {
            Move.Up => boxPosition with { Y = boxPosition.Y - 1 },
            Move.Down => boxPosition with { Y = boxPosition.Y + 1 },
            Move.Left => boxPosition with { X = boxPosition.X - 1 },
            Move.Right => boxPosition with { X = boxPosition.X + 1 },
            _ => throw new ArgumentOutOfRangeException(nameof(move), move, null)
        };

        switch (this[newBoxPosition])
        {
            case Box:
                PushBox(newBoxPosition, move);
                break;
            case WideBoxLeft:
            {
                PushBox(newBoxPosition, move);
                if (move is not (Move.Left or Move.Right))
                {
                    PushBox(newBoxPosition with { X = newBoxPosition.X + 1 }, move);
                }

                break;
            }
            case WideBoxRight:
            {
                PushBox(newBoxPosition, move);
                if (move is not (Move.Left or Move.Right))
                {
                    PushBox(newBoxPosition with { X = newBoxPosition.X - 1 }, move);
                }

                break;
            }
        }

        (this[boxPosition], this[newBoxPosition]) = (Empty, this[boxPosition]);
    }
}