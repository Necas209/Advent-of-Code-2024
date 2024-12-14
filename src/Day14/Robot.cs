using System.Drawing;

namespace Day14;

public class Robot
{
    private readonly Size _velocity;

    private Robot(Point position, Size velocity)
    {
        Position = position;
        _velocity = velocity;
    }

    public Point Position { get; private set; }

    public int X => Position.X;

    public int Y => Position.Y;

    public static Robot Parse(string line)
    {
        var parts = line.Split(' ');
        var position = parts[0][2..].Split(',').Select(int.Parse).ToArray();
        var velocity = parts[1][2..].Split(',').Select(int.Parse).ToArray();
        return new Robot(new Point(position[0], position[1]), new Size(velocity[0], velocity[1]));
    }

    public void Move(int time, int width, int height)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(time);

        var x = (X + time * _velocity.Width) % width;
        if (x < 0) x += width;

        var y = (Y + time * _velocity.Height) % height;
        if (y < 0) y += height;
        Position = new Point(x, y);
    }
}