namespace Day24;

public enum Gate
{
    And,
    Or,
    Xor
}

public class Operation
{
    public static Operation Parse(string input)
    {
        var parts = input.Split(' ');
        var gate = parts[1] switch
        {
            "AND" => Gate.And,
            "OR" => Gate.Or,
            "XOR" => Gate.Xor,
            _ => throw new InvalidOperationException()
        };
        return new Operation
        {
            Left = parts[0],
            Right = parts[2],
            Output = parts[4],
            Gate = gate
        };
    }

    public required string Left { get; init; }
    public required string Right { get; init; }
    public required string Output { get; set; }
    public required Gate Gate { get; init; }
}