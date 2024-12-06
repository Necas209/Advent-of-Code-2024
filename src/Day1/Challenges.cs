using System.Collections.Immutable;

namespace Day1;

public static class Challenges
{
    public record Data(ImmutableArray<int> Left, ImmutableArray<int> Right);

    public static Data ProcessData(string path)
    {
        var lines = File.ReadLines(path);
        var left = new List<int>();
        var right = new List<int>();

        foreach (var line in lines)
        {
            var parts = line.Split("   ");
            left.Add(int.Parse(parts[0]));
            right.Add(int.Parse(parts[1]));
        }

        return new Data([..left], [..right]);
    }

    public static int FirstChallenge(Data data)
    {
        var left = data.Left.Sort();
        var right = data.Right.Sort();

        var totalDistance = left
            .Zip(right)
            .Sum(t => Math.Abs(t.First - t.Second));

        return totalDistance;
    }

    public static int SecondChallenge(Data data)
    {
        var (left, right) = data;
        var leftCount = left
            .Distinct()
            .ToDictionary(x => x, _ => 0);

        foreach (var rightNumber in right)
        {
            leftCount.TryGetValue(rightNumber, out var count);
            leftCount[rightNumber] = ++count;
        }

        var similarityScore = left.Sum(x => x * leftCount[x]);
        return similarityScore;
    }
}