using System.Collections.Immutable;
using System.Numerics;

namespace Day24;

public static class Challenge
{
    public static long Part1(string input)
    {
        var parts = input.Split("\n\n");

        var wires = new Wires(parts[0]);
        var operations = parts[1]
            .Split(Environment.NewLine)
            .Select(Operation.Parse)
            .ToImmutableArray();

        wires.ExecuteOperations(operations);

        return wires.GetValue(Wires.Z);
    }

    public static string Part2(string input)
    {
        var parts = input.Split("\n\n");

        var wires = new Wires(parts[0]);
        var operations = parts[1]
            .Split(Environment.NewLine)
            .Select(Operation.Parse)
            .ToImmutableArray();

        const string lastZ = "z45";
        var zOutputNonXor = operations
            .Where(x => x.Output[0] is Wires.Z && x.Output is not lastZ && x.Gate is not Gate.Xor)
            .ToImmutableArray();

        var nonXyzXor = operations
            .Where(x => x.Left[0] is not (Wires.X or Wires.Y)
                        && x.Right[0] is not (Wires.X or Wires.Y)
                        && x.Output[0] is not Wires.Z
                        && x.Gate is Gate.Xor)
            .ToImmutableArray();

        foreach (var operation in nonXyzXor)
        {
            var right = zOutputNonXor.First(x => x.Output == FirstZThatUsesOutput(operations, operation.Output));
            (operation.Output, right.Output) = (right.Output, operation.Output);
        }

        var xValue = wires.GetValue(Wires.X);
        var yValue = wires.GetValue(Wires.Y);

        wires.ExecuteOperations(operations);
        var zValue = wires.GetValue(Wires.Z);
        var falseCarry = BitOperations.TrailingZeroCount(xValue + yValue ^ zValue).ToString();

        var swap = zOutputNonXor
            .Concat(nonXyzXor)
            .Concat(operations
                .Where(operation => operation.Left.EndsWith(falseCarry) && operation.Right.EndsWith(falseCarry)))
            .Select(operation => operation.Output)
            .Order();

        return string.Join(",", swap);
    }

    private static string? FirstZThatUsesOutput(ImmutableArray<Operation> operations, string output)
    {
        var x = operations
            .Where(op => op.Left == output || op.Right == output)
            .ToImmutableArray();

        var y = x.FirstOrDefault(op => op.Output[0] == Wires.Z);
        if (y is not null)
            return Wires.Z + (int.Parse(y.Output[1..]) - 1).ToString().PadLeft(2, '0');

        return x
            .Select(op => FirstZThatUsesOutput(operations, op.Output))
            .FirstOrDefault(s => s is not null);
    }
}