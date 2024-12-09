using System.Text;

namespace Day9;

public readonly record struct DiskSegment(int Index, int Length)
{
    public int End => Index + Length;

    public bool IsEmpty => Length == 0;

    public static readonly DiskSegment Empty = new(0, 0);
}

public class Disk
{
    private const int Empty = -1;
    private readonly int[] _disk;

    public Disk(string input)
    {
        var disk = new List<int>();
        var fileIndex = 0;
        foreach (var (i, ch) in input.Index())
        {
            var file = i % 2 == 0 ? fileIndex++ : Empty;
            var fileSize = (int)char.GetNumericValue(ch);
            disk.AddRange(Enumerable.Repeat(file, fileSize));
        }

        _disk = disk.ToArray();
    }

    public int Size => _disk.Length;

    public bool IsEmpty(int index) => _disk[index] == Empty;

    public int EmptyIndex() => EmptyIndex(0);

    public int EmptyIndex(int start) => Array.IndexOf(_disk, Empty, start);

    public DiskSegment EmptySegment() => EmptySegment(0);

    public DiskSegment EmptySegment(int start)
    {
        var emptyIndex = EmptyIndex(start);
        if (emptyIndex == -1)
            return DiskSegment.Empty;

        var emptySize = 0;
        for (var i = emptyIndex; i < _disk.Length; i++)
        {
            if (_disk[i] != Empty)
                break;

            emptySize++;
        }

        return new DiskSegment(emptyIndex, emptySize);
    }

    public DiskSegment LastNonEmptySegment() => LastNonEmptySegment(_disk.Length - 1);

    public DiskSegment LastNonEmptySegment(int start)
    {
        var nonEmptyIndex = Array.FindLastIndex(_disk, start, x => x != Empty);
        if (nonEmptyIndex == -1)
            return DiskSegment.Empty;

        var file = _disk[nonEmptyIndex];
        var nonEmptySize = 0;
        for (var i = nonEmptyIndex; i >= 0; i--)
        {
            if (_disk[i] == Empty)
                break;

            if (_disk[i] == file)
                nonEmptySize++;
        }

        return new DiskSegment(nonEmptyIndex - nonEmptySize + 1, nonEmptySize);
    }

    public void Swap(int i, int j) => (_disk[i], _disk[j]) = (_disk[j], _disk[i]);

    public void Swap(int i, int j, int length)
    {
        for (var k = 0; k < length; k++)
        {
            Swap(i + k, j + k);
        }
    }

    public long GetChecksum() => _disk
        .Select(x => (long)x)
        .Index()
        .Where(t => t.Item != Empty)
        .Sum(t => t.Index * t.Item);

    public override string ToString() =>
        new StringBuilder()
            .AppendJoin("", _disk.Select(x => x == Empty ? "." : x.ToString()))
            .ToString();
}