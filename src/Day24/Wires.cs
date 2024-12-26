using System.Collections.Immutable;

namespace Day24;

public class Wires
{
    public const char X = 'x';
    public const char Y = 'y';
    public const char Z = 'z';

    private readonly Dictionary<string, int> _wires;

    public Wires(string input)
    {
        _wires = input
            .Split(Environment.NewLine)
            .Select(x => x.Split(": "))
            .ToDictionary(x => x[0], x => int.Parse(x[1]));
    }

    public void ExecuteOperations(ImmutableArray<Operation> operations)
    {
        var queue = new Queue<Operation>(operations);
        while (queue.Count > 0)
        {
            var operation = queue.Dequeue();
            if (_wires.ContainsKey(operation.Output))
                continue;

            if (!_wires.TryGetValue(operation.Left, out var leftValue))
            {
                queue.Enqueue(operation);
                continue;
            }

            if (!_wires.TryGetValue(operation.Right, out var rightValue))
            {
                queue.Enqueue(operation);
                continue;
            }

            var result = operation.Gate switch
            {
                Gate.And => leftValue & rightValue,
                Gate.Or => leftValue | rightValue,
                Gate.Xor => leftValue ^ rightValue,
                _ => throw new InvalidOperationException()
            };
            _wires[operation.Output] = result;
        }
    }

    public long GetValue(char wire)
    {
        return _wires
            .Where(x => x.Key[0] == wire)
            .Select(kvp => (Bit: int.Parse(kvp.Key[1..]), kvp.Value))
            .Select(t => t.Value * (1L << t.Bit))
            .Sum();
    }
}