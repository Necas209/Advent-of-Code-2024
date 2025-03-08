using System.Collections.Immutable;
using System.Drawing;
using System.Text;
using DotNetExtensions.Collections;

namespace Day21;

public class Keypad
{
    private readonly Point _forbidden;
    private readonly ImmutableDictionary<char, Point> _keyMap;

    private static readonly ImmutableArray<(char Char, Size Direction)> Directions =
    [
        .."<v^>"
            .Select(ch => (ch, ch switch
            {
                '<' => new Size(-1, 0),
                'v' => new Size(0, 1),
                '^' => new Size(0, -1),
                '>' => new Size(1, 0),
                _ => throw new InvalidOperationException()
            }))
    ];

    public Keypad(ImmutableDictionary<char, Point> keyMap)
    {
        _keyMap = keyMap;
        _forbidden = _keyMap['A'] with { X = 0 };
    }

    public string MovesFromString(string input)
    {
        return string.Concat(input
            .Prepend('A')
            .Pairwise()
            .Select(t => Transition(t.First, t.Second))
        );
    }

    public string Transition(char from, char to)
    {
        var sb = new StringBuilder();

        var target = _keyMap[to];
        var current = _keyMap[from];

        var delta = new Size(target.X - current.X, target.Y - current.Y);
        var d = 0;

        while (delta != Size.Empty)
        {
            var (dirChar, direction) = Directions[d++ % Directions.Length];
            var amount = direction.Width == 0 ? delta.Height / direction.Height : delta.Width / direction.Width;
            if (amount <= 0)
                continue;

            var dest = current + direction * amount;
            if (dest == _forbidden)
                continue;

            current = dest;
            delta -= direction * amount;
            sb.Append(new string(dirChar, amount));
        }

        sb.Append('A');
        return sb.ToString();
    }
}