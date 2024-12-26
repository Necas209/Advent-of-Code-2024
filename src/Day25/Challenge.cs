namespace Day25;

public static class Challenge
{
    public static int Part1(string input)
    {
        var keys = new List<Schematic>();
        var locks = new List<Schematic>();

        foreach (var block in input.Split("\n\n"))
        {
            var schematic = new Schematic(block);
            switch (schematic.Type)
            {
                case SchematicType.Key:
                    keys.Add(schematic);
                    break;
                case SchematicType.Lock:
                    locks.Add(schematic);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        var count = 0;
        foreach (var key in keys)
        {
            foreach (var sLock in locks)
            {
                if (key.IsMatch(sLock))
                {
                    count++;
                }
            }
        }

        return count;
    }
}

public enum SchematicType
{
    Key,
    Lock
}

public class Schematic
{
    private readonly bool[,] _map = new bool[Width, Height];
    private const int Width = 5;
    private const int Height = 5;

    public Schematic(string input)
    {
        var lines = input.Split('\n');
        // Check if the schematic is a key or a lock
        // If the first row is all #, it's a lock
        Type = lines[0].All(c => c == '#') ? SchematicType.Lock : SchematicType.Key;

        var relevantLines = lines[1..^1];
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                _map[x, y] = relevantLines[y][x] == '#';
            }
        }
    }

    public SchematicType Type { get; }

    public bool IsMatch(Schematic other)
    {
        if (Type == other.Type)
            throw new InvalidOperationException("Cannot compare two schematics of the same type");

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (_map[x, y] && other._map[x, y])
                {
                    return false;
                }
            }
        }

        return true;
    }
}