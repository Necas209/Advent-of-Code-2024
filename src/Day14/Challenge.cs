using System.Collections.Immutable;
using System.Drawing;
using DotNetExtensions.Core;

namespace Day14;

public class Challenge(int width, int height)
{
    private readonly char[,] _grid = new char[height, width];

    public int Part1(string input)
    {
        var robots = input.Split(Environment.NewLine).Select(Robot.Parse).ToImmutableArray();

        foreach (var robot in robots)
        {
            robot.Move(100, width, height);
        }

        var groups = robots
            .GroupBy(robot =>
            {
                if (robot.X < width / 2 && robot.Y < height / 2)
                    return 1;
                if (robot.X > width / 2 && robot.Y < height / 2)
                    return 2;
                if (robot.X < width / 2 && robot.Y > height / 2)
                    return 3;
                if (robot.X > width / 2 && robot.Y > height / 2)
                    return 4;
                return 0;
            });

        var safetyFactor = groups
            .Where(g => g.Key != 0)
            .Select(g => g.Count())
            .Aggregate(1, (acc, count) => acc * count);

        return safetyFactor;
    }

    public int Part2(string input, bool printGrid)
    {
        var robots = input.Split(Environment.NewLine).Select(Robot.Parse).ToImmutableArray();

        var seconds = 0;
        var cycle = width * height;

        while (seconds < cycle)
        {
            var positions = robots.Select(r => r.Position).ToImmutableHashSet();
            if (HasClusters(positions))
            {
                if (printGrid)
                {
                    PrintGrid(seconds, positions);
                }

                break;
            }

            seconds++;
            foreach (var robot in robots)
            {
                robot.Move(1, width, height);
            }
        }

        return seconds;
    }

    private static bool HasClusters(ImmutableHashSet<Point> robots, int minClusterSize = 3)
    {
        var numClusters = 0;
        foreach (var robot in robots)
        {
            var (left, right, top, bottom) = robot.GetNeighbors();
            if (!robots.Contains(top) || !robots.Contains(bottom) || !robots.Contains(left) || !robots.Contains(right))
                continue;

            numClusters++;
            if (numClusters == minClusterSize)
                return true;
        }

        return false;
    }

    private void PrintGrid(int seconds, ImmutableHashSet<Point> robots)
    {
        // Initialize an empty grid
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                _grid[y, x] = '.'; // Empty space
            }
        }

        // Place robots on the grid
        foreach (var robot in robots)
        {
            _grid[robot.Y, robot.X] = '#'; // Mark robot position
        }

        Console.WriteLine($"Seconds: {seconds}");

        // Print the grid
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                Console.Write(_grid[y, x]);
            }

            Console.WriteLine();
        }
    }
}