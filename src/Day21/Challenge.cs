using System.Collections.Immutable;
using System.Drawing;
using DotNetExtensions.Collections;

namespace Day21;

public static class Challenge
{
    public static long Part1(string input)
    {
        var codes = input.Split(Environment.NewLine).ToImmutableArray();
        return Solve(codes, 2);
    }

    public static long Part2(string input)
    {
        var codes = input.Split(Environment.NewLine).ToImmutableArray();
        return Solve(codes, 25);
    }

    private readonly record struct KeyState(char LastKey, char NewKey, int Level);

    private static readonly Dictionary<KeyState, long> Memo = new();

    private static readonly Keypad NumericPad = new(new Dictionary<char, Point>
    {
        ['7'] = new(0, 0),
        ['8'] = new(1, 0),
        ['9'] = new(2, 0),
        ['4'] = new(0, 1),
        ['5'] = new(1, 1),
        ['6'] = new(2, 1),
        ['1'] = new(0, 2),
        ['2'] = new(1, 2),
        ['3'] = new(2, 2),
        ['0'] = new(1, 3),
        ['A'] = new(2, 3)
    }.ToImmutableDictionary());

    private static readonly Keypad DirectionalPad = new(new Dictionary<char, Point>
    {
        ['^'] = new(1, 0),
        ['A'] = new(2, 0),
        ['<'] = new(0, 1),
        ['v'] = new(1, 1),
        ['>'] = new(2, 1)
    }.ToImmutableDictionary());

    private static long Solve(ImmutableArray<string> codes, int numDirectionalKeypads)
    {
        return codes
            .Select(code => (
                NumericValue: long.Parse(code[..^1]),
                Complexity: NumericPad.MovesFromString(code)
                    .Prepend('A')
                    .Pairwise()
                    .Select(p => DirectionalPadMoves(new KeyState(p.First, p.Second, numDirectionalKeypads)))
                    .Sum()))
            .Select(t => t.NumericValue * t.Complexity)
            .Sum();
    }

    private static long DirectionalPadMoves(KeyState keyState)
    {
        if (Memo.TryGetValue(keyState, out var count))
            return count;

        var todo = DirectionalPad.Transition(keyState.LastKey, keyState.NewKey);
        if (keyState.Level == 1)
        {
            count = todo.Length;
        }
        else
        {
            count = todo
                .Prepend('A')
                .Pairwise()
                .Select(t => DirectionalPadMoves(new KeyState(t.First, t.Second, keyState.Level - 1)))
                .Sum();
        }

        Memo[keyState] = count;
        return count;
    }
}