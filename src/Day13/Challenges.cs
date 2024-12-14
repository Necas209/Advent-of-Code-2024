using System.Text.RegularExpressions;

namespace Day13;

public static partial class Challenges
{
    [GeneratedRegex(
        @"Button A: X\+(?<aX>\d+), Y\+(?<aY>\d+)\s+Button B: X\+(?<bX>\d+), Y\+(?<bY>\d+)\s+Prize: X=(?<pX>\d+), Y=(?<pY>\d+)")]
    private static partial Regex ClawMachineRegex();

    public static long Part1(string input) => Challenge(input, false);

    public static long Part2(string input) => Challenge(input, true);

    private static long Challenge(string input, bool prizeAdjustment)
    {
        var totalTokens = 0L;

        var matches = ClawMachineRegex().Matches(input);
        foreach (Match match in matches)
        {
            var aX = int.Parse(match.Groups["aX"].Value);
            var aY = int.Parse(match.Groups["aY"].Value);
            var bX = int.Parse(match.Groups["bX"].Value);
            var bY = int.Parse(match.Groups["bY"].Value);
            var prizeX = decimal.Parse(match.Groups["pX"].Value);
            var prizeY = decimal.Parse(match.Groups["pY"].Value);

            if (prizeAdjustment)
            {
                prizeX += 10_000_000_000_000;
                prizeY += 10_000_000_000_000;
            }

            var (a, b) = SolveLinearEquations(aX, aY, bX, bY, prizeX, prizeY);
            totalTokens += 3 * a + b;
        }

        return totalTokens;
    }

    private static (long A, long B) SolveLinearEquations(int aX, int aY, int bX, int bY, decimal pX, decimal pY)
    {
        // Compute determinant of A
        decimal determinant = aX * bY - aY * bX;
        if (Math.Abs(determinant) < 1e-9m) // Check for non-invertible matrix
            return (0, 0);

        // Compute the inverse of A
        var invAxx = bY / determinant;
        var invAxy = -bX / determinant;
        var invAyx = -aY / determinant;
        var invAyy = aX / determinant;

        // Solve for X = A^(-1) * B
        var a = invAxx * pX + invAxy * pY;
        var b = invAyx * pX + invAyy * pY;

        // Check if a and b are close to integer values
        if (Math.Abs(a - Math.Round(a)) > 1e-9m || Math.Abs(b - Math.Round(b)) > 1e-9m)
            return (0, 0);

        // Ensure that a and b are non-negative and integers
        if (a < 0 || b < 0)
            return (0, 0);

        return ((long)Math.Round(a), (long)Math.Round(b));
    }
}