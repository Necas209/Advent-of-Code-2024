using System.Collections.Immutable;

namespace Day23;

public class ComputerNetwork
{
    private readonly ImmutableDictionary<int, ImmutableHashSet<int>> _graph;

    public ComputerNetwork(string input)
    {
        var computers = input.Split(Environment.NewLine)
            .Select(line => line.Split('-'))
            .Select(parts => (parts[0], parts[1]));

        // Parse the input into a graph
        var graph = new Dictionary<int, HashSet<int>>();
        foreach (var (a, b) in computers)
        {
            var encodedA = Encode(a);
            var encodedB = Encode(b);

            if (!graph.ContainsKey(encodedA)) graph[encodedA] = [];
            if (!graph.ContainsKey(encodedB)) graph[encodedB] = [];

            graph[encodedA].Add(encodedB);
            graph[encodedB].Add(encodedA);
        }

        _graph = graph.ToImmutableDictionary(
            pair => pair.Key,
            pair => pair.Value.ToImmutableHashSet());
    }

    public int NumberOfTriangles(char c)
    {
        // Find all triangles with a computer starting with 't'
        var triangles = new HashSet<string>();

        foreach (var node in _graph.Keys)
        {
            foreach (var neighbor in _graph[node])
            {
                foreach (var commonNeighbor in _graph[neighbor])
                {
                    if (!StartsWith(node, c) && !StartsWith(neighbor, c) && !StartsWith(commonNeighbor, c))
                        continue;

                    if (!_graph[node].Contains(commonNeighbor)
                        || node == neighbor
                        || neighbor == commonNeighbor
                        || node == commonNeighbor)
                        continue;

                    // Sort the triangle nodes to avoid duplicates
                    var triangle = ImmutableArray.Create(node, neighbor, commonNeighbor).Sort();
                    var triangleKey = string.Join("", triangle);

                    triangles.Add(triangleKey);
                }
            }
        }

        return triangles.Count;
    }

    public IEnumerable<string> LargestClique()
    {
        // Find the largest clique
        var largestClique = BronKerbosch([], [.._graph.Keys], [], []);

        // Sort the clique and generate the password
        return largestClique.Select(Decode).Order();
    }

    public override string ToString() => $"Network with {_graph.Count} computers";

    private ImmutableArray<int> BronKerbosch(ImmutableArray<int> r, ImmutableArray<int> p, ImmutableArray<int> x,
        ImmutableArray<int> largestClique)
    {
        if (p.IsEmpty && x.IsEmpty)
        {
            // Maximal clique found
            if (r.Length > largestClique.Length)
            {
                largestClique = [..r];
            }

            return largestClique;
        }

        var pivot = p.Concat(x).First();
        var nonNeighbors = p.Except(_graph[pivot]);

        foreach (var node in nonNeighbors)
        {
            var newR = r.Add(node);
            var newP = p.Intersect(_graph[node]).ToImmutableArray();
            var newX = x.Intersect(_graph[node]).ToImmutableArray();

            largestClique = BronKerbosch(newR, newP, newX, largestClique);

            p = p.Remove(node);
            x = x.Add(node);
        }

        return largestClique;
    }

    private static int Encode(string computer) => (computer[0] - 'a') * 26 + (computer[1] - 'a');

    private static string Decode(int computer) => $"{(char)('a' + computer / 26)}{(char)('a' + computer % 26)}";

    private static bool StartsWith(int computer, char c) => computer / 26 == c - 'a';
}