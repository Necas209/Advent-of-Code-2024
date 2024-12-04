using System.Diagnostics;
using System.Text.RegularExpressions;

const string test = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

Debug.Assert(GetMultiplicationsSum(test) == 161);

var allText = File.ReadAllText("input.txt");
Console.WriteLine($"Sum of Multiplications: {GetMultiplicationsSum(allText)}");

const string doDontTest = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
Debug.Assert(GetMultiplicationsSum(doDontTest, true) == 48);

Console.WriteLine($"Sum of Multiplications (Complex): {GetMultiplicationsSum(allText, true)}");

return;

int GetMultiplicationsSum(string content, bool doDont = false)
{
    var regex = doDont ? DoDontMulRegex() : MulRegex();
    var mulMatches = regex.Matches(content);

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

internal static partial class Program
{
    [GeneratedRegex(@"mul\((?<first>\d+),(?<second>\d+)\)")]
    private static partial Regex MulRegex();

    [GeneratedRegex(@"(do\(\))|(don't\(\))|(mul\((?<first>\d+),(?<second>\d+)\))")]
    private static partial Regex DoDontMulRegex();
}