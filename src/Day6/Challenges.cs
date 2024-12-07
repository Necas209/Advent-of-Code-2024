using System.Drawing;

namespace Day6;

public static class Challenges
{
    public static int FirstChallenge(string input)
    {
        var map = new Map(input);
        var guard = map.FindGuard();

        while (true)
        {
            var nextGuard = guard.Advance();
            if (!map.Contains(nextGuard.Position))
            {
                map[guard.Position] = Map.Visited;
                break;
            }

            if (map[nextGuard.Position] == Map.Obstruction)
            {
                guard = guard.TurnRight();
            }
            else
            {
                map[guard.Position] = Map.Visited;
                guard = nextGuard;
            }
        }

        return map.Count(x => x == Map.Visited);
    }

    public static int SecondChallenge(string input)
    {
        var map = new Map(input);
        var initialGuard = map.FindGuard();

        // Find all possible obstructions
        var possibleObstructions = map
            .Index()
            .Where(t => t.Item == Map.Empty)
            .Select(t => map.PointOf(t.Index));

        // Clone the map to avoid modifying the original map
        var mutableMap = (Map)map.Clone();

        var obstructions = new HashSet<Point>();
        foreach (var possibleObstruction in possibleObstructions)
        {
            // Mark the possible obstruction as an obstruction
            mutableMap[possibleObstruction] = Map.Obstruction;

            var guard = initialGuard;
            while (true)
            {
                var nextGuard = guard.Advance();

                if (!mutableMap.Contains(nextGuard.Position))
                    break;

                if (mutableMap[nextGuard.Position] == Map.Obstruction)
                {
                    guard = guard.TurnRight();
                }
                else
                {
                    mutableMap[guard.Position] = guard.Direction;
                    guard = nextGuard;
                }

                var currentPosition = guard.Position;
                if (mutableMap[currentPosition] != guard.Direction)
                    continue;

                obstructions.Add(possibleObstruction);
                break;
            }

            // Restore the map to its original state
            // This avoids having to clone the map for each possible obstruction
            mutableMap.Update(map);
        }

        return obstructions.Count;
    }
}