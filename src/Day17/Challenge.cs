using System.Collections.Immutable;
using System.Numerics;

namespace Day17;

public static class Challenge
{
    public static string Part1(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        var a = long.Parse(lines[0].Split(": ")[1]);

        var program = lines[^1].Split(": ")[1]
            .Split(',')
            .Select(byte.Parse)
            .ToImmutableArray();

        var computer = new Computer(program);
        var output = computer.Run(a);

        return string.Join(',', output);
    }

    public static long Part2(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        var program = lines[^1].Split(": ")[1]
            .Split(',')
            .Select(byte.Parse)
            .ToImmutableArray();

        var computer = new Computer(program);
        return computer.Reverse();
    }
}