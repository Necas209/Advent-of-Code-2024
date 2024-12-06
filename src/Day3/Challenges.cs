using System.Text.RegularExpressions;

namespace Day3;

public static partial class Challenges
{
    [GeneratedRegex(@"mul\((?<first>\d+),(?<second>\d+)\)")]
    private static partial Regex MulRegex();

    [GeneratedRegex(@"(do\(\))|(don't\(\))|(mul\((?<first>\d+),(?<second>\d+)\))")]
    private static partial Regex DoDontMulRegex();

    public static int FirstChallenge(string content)
    {
        var mulMatches = MulRegex().Matches(content);

        var sum = 0;

        foreach (Match match in mulMatches)
        {
            var first = int.Parse(match.Groups["first"].Value);
            var second = int.Parse(match.Groups["second"].Value);
            sum += first * second;
        }

        return sum;
    }

    public static int SecondChallenge(string content)
    {
        var mulMatches = DoDontMulRegex().Matches(content);

        var sum = 0;

        var doMul = true;
        foreach (Match match in mulMatches)
        {
            switch (match.Value)
            {
                case "do()":
                    doMul = true;
                    break;
                case "don't()":
                    doMul = false;
                    break;
                default:
                    if (!doMul) continue;
                    var first = int.Parse(match.Groups["first"].Value);
                    var second = int.Parse(match.Groups["second"].Value);
                    sum += first * second;
                    break;
            }
        }

        return sum;
    }
}