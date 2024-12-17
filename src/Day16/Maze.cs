using System.Collections.Immutable;
using System.Drawing;

namespace Day16;

public class Maze
{
    private static readonly ImmutableDictionary<Size, Orientation> Directions =
        new Dictionary<Size, Orientation>
        {
            { new Size(0, -1), Orientation.Up },
            { new Size(0, 1), Orientation.Down },
            { new Size(-1, 0), Orientation.Left },
            { new Size(1, 0), Orientation.Right }
        }.ToImmutableDictionary();

    private readonly int _width;
    private readonly int _height;
    private readonly char[,] _maze;
    private readonly Point _start;
    private readonly Point _end;

    public Maze(string input)
    {
        var lines = input.Split(Environment.NewLine);
        _width = lines[0].Length;
        _height = lines.Length;
        _maze = new char[_height, _width];
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                _maze[y, x] = lines[y][x];
                switch (lines[y][x])
                {
                    case 'E':
                        _end = new Point(x, y);
                        break;
                    case 'S':
                        _start = new Point(x, y);
                        break;
                }
            }
        }
    }

    private bool IsVisitable(Point point)
    {
        return point.X >= 0
               && point.X < _width
               && point.Y >= 0
               && point.Y < _height
               && _maze[point.Y, point.X] != '#';
    }

    // Recursive method to reconstruct all paths
    private IEnumerable<ImmutableArray<Point>> ReconstructPaths(Reindeer reindeer,
        IReadOnlyDictionary<Reindeer, (int Cost, List<Reindeer> Parents)> stateInfo)
    {
        if (reindeer.Position == _start)
        {
            yield return [_start];
            yield break;
        }

        foreach (var parentReindeer in stateInfo[reindeer].Parents)
        {
            foreach (var path in ReconstructPaths(parentReindeer, stateInfo))
            {
                yield return path.Add(reindeer.Position);
            }
        }
    }

    public IEnumerable<ImmutableArray<Point>> FindAllPaths()
    {
        var priorityQueue = new SortedSet<(int Cost, Reindeer Reindeer)>(
            Comparer<(int Cost, Reindeer Reindeer)>.Create(
                (a, b) =>
                    a.Cost != b.Cost
                        ? a.Cost.CompareTo(b.Cost)
                        : a.Reindeer.CompareTo(b.Reindeer)
            )
        );

        var stateInfo = new Dictionary<Reindeer, (int Cost, List<Reindeer> Parents)>();

        // Initialize the queue with the start position
        var start = new Reindeer(_start, Orientation.Right);
        priorityQueue.Add((0, start));
        stateInfo[start] = (0, []);

        while (priorityQueue.Count > 0)
        {
            var (currentCost, currentReindeer) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);

            // If we reached the end, reconstruct the path
            if (currentReindeer.Position == _end)
            {
                foreach (var path in ReconstructPaths(currentReindeer, stateInfo))
                {
                    yield return path;
                }

                continue;
            }

            // Explore all possible moves
            foreach (var (move, newDirection) in Directions)
            {
                var newPosition = currentReindeer.Position + move;
                if (!IsVisitable(newPosition))
                    continue;

                var moveCost = newDirection == currentReindeer.Orientation ? 1 : 1001;
                var newCost = currentCost + moveCost;
                var newState = new Reindeer(newPosition, newDirection);

                // If the state is unvisited, or we found an equal-cost path
                if (!stateInfo.TryGetValue(newState, out var parentsState))
                {
                    stateInfo[newState] = (newCost, [currentReindeer]);
                    priorityQueue.Add((newCost, newState));
                }
                else if (parentsState.Cost == newCost)
                {
                    parentsState.Parents.Add(currentReindeer);
                }
            }
        }
    }

    public static int ComputeCost(ImmutableArray<Point> path)
    {
        var cost = 0;
        var direction = Orientation.Right;
        var previousPosition = path[0];

        foreach (var position in path.Skip(1))
        {
            var move = new Size(position.X - previousPosition.X, position.Y - previousPosition.Y);
            var orientation = Directions[move];

            cost += orientation == direction ? 1 : 1000 + 1;
            direction = orientation;
            previousPosition = position;
        }

        return cost;
    }
}