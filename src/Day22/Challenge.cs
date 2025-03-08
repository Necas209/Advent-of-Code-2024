using System.Collections.Immutable;
using DotNetExtensions.Collections;

namespace Day22;

public static class Challenge
{
    private const int Iterations = 2000;

    public static long Part1(string input)
    {
        var secrets = input
            .Split(Environment.NewLine)
            .Select(long.Parse)
            .ToImmutableArray();

        var sum = 0L;
        foreach (var secret in secrets)
        {
            var currentSecret = secret;

            var i = 0;
            while (i++ < Iterations)
            {
                currentSecret = NextSecret(currentSecret);
            }

            sum += currentSecret;
        }

        return sum;
    }

    public static long Part2(string input)
    {
        var secrets = input
            .Split(Environment.NewLine)
            .Select(long.Parse)
            .ToImmutableArray();

        var totalScores = new Dictionary<int, int>();
        var prices = new byte[Iterations + 1];

        foreach (var secret in secrets)
        {
            prices[0] = (byte)(secret % 10);
            var currentSecret = secret;

            var i = 0;
            while (i++ < Iterations)
            {
                currentSecret = NextSecret(currentSecret);
                prices[i] = (byte)(currentSecret % 10);
            }

            var seen = new HashSet<int>();
            foreach (var (first, second, third, fourth, fifth) in prices.Quintuples())
            {
                // Encode the sequence into a single integer using base 19
                // Each change in the sequence is shifted by 9 to avoid negative numbers
                var sequence = (second - first + 9) * 19 * 19 * 19
                               + (third - second + 9) * 19 * 19
                               + (fourth - third + 9) * 19
                               + (fifth - fourth + 9) * 1;

                if (seen.Add(sequence))
                {
                    totalScores[sequence] = totalScores.GetValueOrDefault(sequence) + fifth;
                }
            }
        }

        return totalScores.Values.Max();
    }

    private static long NextSecret(long secret)
    {
        secret = Mix(secret, secret << 6); // Multiply by 64
        secret = Prune(secret);
        secret = Mix(secret, secret >> 5); // Divide by 32
        secret = Prune(secret);
        secret = Mix(secret, secret << 11); // Multiply by 2048
        secret = Prune(secret);
        return secret;

        static long Mix(long secret, long value)
        {
            return secret ^ value;
        }

        static long Prune(long secret)
        {
            // Equivalent to secret % 16777216
            return secret & ((1 << 24) - 1);
        }
    }
}