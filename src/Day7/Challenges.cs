using System.Collections.Immutable;

namespace Day7;

public static class Challenges
{
    public static long Part1(string input) => Challenge(input, false);

    public static long Part2(string input) =>
        Challenge(input, true);

    private static long Challenge(string input, bool concatenate)
    {
        var equations = input
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split(": "))
            .Select(line =>
            (
                Value: long.Parse(line[0]),
                Numbers: line[1].Split(' ').Select(long.Parse).ToImmutableArray()
            ))
            .ToImmutableArray();

        var calibrationNumber = 0L;
        foreach (var (value, numbers) in equations)
        {
            var currentValues = new Queue<long>();
            foreach (var number in numbers)
            {
                if (currentValues.Count == 0)
                {
                    currentValues.Enqueue(number);
                }
                else
                {
                    var newValues = new Queue<long>();
                    while (currentValues.Count > 0)
                    {
                        var currentValue = currentValues.Dequeue();
                        if (currentValue > value)
                            continue;

                        var sum = currentValue + number;
                        if (sum <= value)
                        {
                            newValues.Enqueue(sum);
                        }

                        var product = currentValue * number;
                        if (product <= value)
                        {
                            newValues.Enqueue(product);
                        }

                        if (!concatenate) continue;
                        var concat = long.Parse($"{currentValue}{number}");
                        if (concat <= value)
                        {
                            newValues.Enqueue(concat);
                        }
                    }

                    currentValues = newValues;
                }
            }

            if (currentValues.Contains(value))
            {
                calibrationNumber += value;
            }
        }

        return calibrationNumber;
    }
}