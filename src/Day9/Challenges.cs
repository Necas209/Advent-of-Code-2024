namespace Day9;

public static class Challenges
{
    public static long Part1(string input)
    {
        var disk = new Disk(input);

        var emptyFileIdx = disk.EmptyIndex();
        foreach (var i in Enumerable.Range(0, disk.Size).Where(i => !disk.IsEmpty(i)).Reverse())
        {
            if (emptyFileIdx == -1 || emptyFileIdx > i)
                break;

            disk.Swap(emptyFileIdx, i);
            emptyFileIdx = disk.EmptyIndex(emptyFileIdx);
        }

        return disk.GetChecksum();
    }

    public static long Part2(string input)
    {
        var disk = new Disk(input);

        var nonEmptySegment = disk.LastNonEmptySegment();
        while (!nonEmptySegment.IsEmpty)
        {
            var emptySegment = disk.EmptySegment();
            if (emptySegment.IsEmpty)
                break;

            var (nonEmptyOffset, nonEmptySize) = nonEmptySegment;
            if (emptySegment.Index >= nonEmptyOffset)
                break;

            while (!emptySegment.IsEmpty && emptySegment.Index < nonEmptySegment.Index)
            {
                if (emptySegment.Length >= nonEmptySize)
                {
                    disk.Swap(nonEmptyOffset, emptySegment.Index, nonEmptySize);
                    break;
                }

                emptySegment = disk.EmptySegment(emptySegment.End + 1);
            }

            nonEmptySegment = disk.LastNonEmptySegment(nonEmptyOffset - 1);
        }

        return disk.GetChecksum();
    }
}