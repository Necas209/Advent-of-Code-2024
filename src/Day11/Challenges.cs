namespace Day11;

public class Challenge(int maxBlinks)
{
    private readonly Dictionary<(long stone, int blinks), long> _memo = new();

    public long GetStoneCount(string input)
    {
        var stones = input.Split(' ').Select(long.Parse).ToList();

        return stones.Sum(stone => GetStoneCount(stone, 0));
    }

    private long GetStoneCount(long stone, int blinks)
    {
        blinks++;
        // Check if we already computed this
        if (_memo.TryGetValue((stone, blinks), out var result))
            return result;

        if (blinks > maxBlinks)
            return 1;

        var stoneCount = 0L;
        if (stone == 0)
        {
            stoneCount += GetStoneCount(1, blinks);
            return stoneCount;
        }

        var numDigits = (int)Math.Floor(Math.Log10(stone) + 1);
        if (numDigits % 2 == 0)
        {
            var (left, right) = SplitNumber(stone, numDigits);
            stoneCount += GetStoneCount(left, blinks);
            stoneCount += GetStoneCount(right, blinks);
            return stoneCount;
        }

        stoneCount += GetStoneCount(stone * 2024, blinks);
        // Memoize the result
        _memo[(stone, blinks)] = stoneCount;
        return stoneCount;
    }
    
    private static (long Left, long Right) SplitNumber(long number, int numDigits)
    {
        var halfDigits = numDigits / 2;
        var divisor = (long)Math.Pow(10, halfDigits);
        
        var leftPart = number / divisor;
        var rightPart = number % divisor;
        return (leftPart, rightPart);
    }
}