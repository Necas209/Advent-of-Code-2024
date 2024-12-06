using System.Collections.Immutable;

namespace Day5;

internal record UpdateAnalysis(
    Dictionary<int, HashSet<int>> Dependencies,
    Dictionary<int, int> Indegree,
    Queue<int> Queue)
{
    public static UpdateAnalysis Analyze(ImmutableArray<int> update, ImmutableArray<(int Before, int After)> rules)
    {
        var dependencies = new Dictionary<int, HashSet<int>>(); // Graph of dependencies
        var indegree = new Dictionary<int, int>();

        // Initialize graph and indegree for all pages in the update
        foreach (var page in update)
        {
            dependencies[page] = [];
            indegree[page] = 0;
        }

        // Build the dependency graph based on the rules
        foreach (var (before, after) in rules)
        {
            if (!dependencies.ContainsKey(before) || !dependencies.ContainsKey(after))
                continue; // Skip rules that reference pages not in the update

            if (dependencies[before].Add(after)) // Add edge from 'before' to 'after'
            {
                indegree[after]++; // Increment indegree of 'after'
            }
        }

        // Perform topological sorting
        var queue = new Queue<int>();

        // Add all pages with no incoming edges to the queue
        foreach (var page in update.Where(page => indegree[page] == 0))
        {
            queue.Enqueue(page);
        }

        return new UpdateAnalysis(dependencies, indegree, queue);
    }
}