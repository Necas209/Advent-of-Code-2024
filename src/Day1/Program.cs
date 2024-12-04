using System.Diagnostics;

const string test = """
                    3   4
                    4   3
                    2   5
                    1   3
                    3   9
                    3   3
                    """;

var (testLeftNumbers, testRightNumbers) = GetNumbers(test.Split(Environment.NewLine));
Debug.Assert(TotalDistance(testLeftNumbers, testRightNumbers) == 11);
Debug.Assert(SimilarityScore(testLeftNumbers, testRightNumbers) == 31);

var (leftNumbers, rightNumbers) = GetNumbers(File.ReadLines("input.txt"));

var totalDistance = TotalDistance(leftNumbers, rightNumbers);
Console.WriteLine($"Total distance: {totalDistance}");

var similarityScore = SimilarityScore(leftNumbers, rightNumbers);
Console.WriteLine($"Similarity score: {similarityScore}");
return;

(List<int> LeftNumbers, List<int> RightNumbers) GetNumbers(IEnumerable<string> lines)
{
    var left = new List<int>();
    var right = new List<int>();

    foreach (var line in lines)
    {
        var parts = line.Split("   ");
        Debug.Assert(parts.Length == 2);
        left.Add(int.Parse(parts[0]));
        right.Add(int.Parse(parts[1]));
    }

    return (left, right);
}

int TotalDistance(List<int> left, List<int> right)
{
    left.Sort();
    right.Sort();

    var distance = left.Zip(right).Sum(t => Math.Abs(t.First - t.Second));
    return distance;
}

int SimilarityScore(List<int> left, List<int> right)
{
    var leftCount = left.Distinct().ToDictionary(x => x, _ => 0);
    foreach (var rightNumber in right)
    {
        leftCount.TryGetValue(rightNumber, out var count);
        leftCount[rightNumber] = ++count;
    }

    var i = left.Sum(x => x * leftCount[x]);
    return i;
}