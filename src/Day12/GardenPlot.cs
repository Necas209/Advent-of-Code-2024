using System.Collections.Immutable;
using System.Drawing;
using SharedLib;

namespace Day12;

public class GardenPlot(ImmutableHashSet<Point> plot)
{
    public int Area => plot.Count;

    public int GetPerimeter()
    {
        return plot
            .Sum(position => position.GetNeighbors()
                .Count(neighbor => !plot.Contains(neighbor)));
    }

    public int GetNumberOfSides()
    {
        // Keep track of the visited boundaries and their side locations
        var visited = new Dictionary<Point, SideLocation>();
        var numberOfSides = 0;

        foreach (var cell in plot.OrderBy(c => c.X).ThenBy(c => c.Y))
        {
            var boundarySideLocation = default(SideLocation);

            // Check each side of the cell
            foreach (var location in Enum.GetValues<SideLocation>())
            {
                var sidePoint = location switch
                {
                    SideLocation.Top => cell with { Y = cell.Y - 1 },
                    SideLocation.Bottom => cell with { Y = cell.Y + 1 },
                    SideLocation.Left => cell with { X = cell.X - 1 },
                    SideLocation.Right => cell with { X = cell.X + 1 },
                    _ => throw new ArgumentOutOfRangeException(nameof(location), location, null)
                };

                // If the side is part of the plot, skip it (it's not a boundary)
                if (plot.Contains(sidePoint))
                    continue;

                // Add the side location to the boundary
                boundarySideLocation |= location;

                // Check if the side has already been visited
                var firstNeighbor = location switch
                {
                    SideLocation.Top => cell with { X = cell.X - 1 },
                    SideLocation.Bottom => cell with { X = cell.X + 1 },
                    SideLocation.Left => cell with { Y = cell.Y - 1 },
                    SideLocation.Right => cell with { Y = cell.Y + 1 },
                    _ => throw new ArgumentOutOfRangeException(nameof(location), location, null)
                };

                if (visited.TryGetValue(firstNeighbor, out var firstLocation) && firstLocation.HasAnyFlag(location))
                    continue;

                var secondNeighbor = location switch
                {
                    SideLocation.Top => cell with { X = cell.X + 1 },
                    SideLocation.Bottom => cell with { X = cell.X - 1 },
                    SideLocation.Left => cell with { Y = cell.Y + 1 },
                    SideLocation.Right => cell with { Y = cell.Y - 1 },
                    _ => throw new ArgumentOutOfRangeException(nameof(location), location, null)
                };

                if (visited.TryGetValue(secondNeighbor, out var secondLocation) && secondLocation.HasAnyFlag(location))
                    continue;

                // If the side has not been visited, increment the number of sides
                numberOfSides++;
            }

            visited.Add(cell, boundarySideLocation);
        }

        return numberOfSides;
    }

    [Flags]
    private enum SideLocation
    {
        Top = 1 << 0,
        Bottom = 1 << 1,
        Left = 1 << 2,
        Right = 1 << 3
    }
}