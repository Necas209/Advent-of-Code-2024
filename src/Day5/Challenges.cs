using System.Collections.Immutable;

namespace Day5;

public static class Challenges
{
    public record ChallengeData(
        ImmutableArray<(int Before, int After)> Rules,
        ImmutableArray<ImmutableArray<int>> Updates);

    public static ChallengeData ProcessData(string filePath)
    {
        var lines = File.ReadAllLines(filePath);

        var sectionIdx = Array.IndexOf(lines, string.Empty);

        var rules = lines[..sectionIdx]
            .Select(l => l.Split('|'))
            .Select(l => (Before: int.Parse(l[0]), After: int.Parse(l[1])))
            .ToImmutableArray();

        var updates = lines[(sectionIdx + 1)..]
            .Select(l => l.Split(','))
            .Select(n => n.Select(int.Parse).ToImmutableArray())
            .ToImmutableArray();

        return new ChallengeData(rules, updates);
    }

    public static int Part1(ChallengeData data)
    {
        return data.Updates
            .Where(update => IsValid(data.Rules, update))
            .Sum(update => update[update.Length / 2]);
    }

    public static int Part2(ChallengeData data)
    {
        return data.Updates
            .Where(update => !IsValid(data.Rules, update))
            .Select(update => MakeValid(data.Rules, update))
            .Sum(update => update[update.Length / 2]);
    }

    private static bool IsValid(ImmutableArray<(int Before, int After)> rules, ImmutableArray<int> update)
    {
        foreach (var (before, after) in rules)
        {
            var beforeIdx = update.IndexOf(before);
            var afterIdx = update.IndexOf(after);
            if (beforeIdx == -1 || afterIdx == -1)
                continue;

            if (beforeIdx > afterIdx)
                return false;
        }

        return true;
    }

    private static ImmutableArray<int> MakeValid(ImmutableArray<(int Before, int After)> rules,
        ImmutableArray<int> update)
    {
        var (dependencies, indegree, queue) = UpdateAnalysis.Analyze(update, rules);

        var result = new List<int>();
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            result.Add(current);

            // Decrease the indegree of all neighbors
            foreach (var neighbor in dependencies[current])
            {
                indegree[neighbor]--;
                if (indegree[neighbor] == 0) // Add to queue if no more incoming edges
                {
                    queue.Enqueue(neighbor);
                }
            }
        }

        // Ensure the result respects the original update order for pages not connected by rules
        return [..result.Intersect(update)];
    }
}