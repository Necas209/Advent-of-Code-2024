using System.Drawing;

namespace Day4;

public static class WordSearch
{
    public static int CountWordX(string content, string word)
    {
        if (word.Length % 2 == 0)
            throw new ArgumentException("Word length must be odd");

        var characters = content.Split(Environment.NewLine)
            .Select(line => line.ToCharArray())
            .ToArray();

        var count = 0;
        foreach (var (y, line) in characters.Index())
        {
            foreach (var (x, ch) in line.Index())
            {
                if (ch != word[word.Length / 2]) continue;

                // Diagonal Down
                var isDiagonalDown = false;
                var start = new Point(x - word.Length / 2, y - word.Length / 2);
                var end = new Point(x + word.Length / 2, y + word.Length / 2);
                if (start is { X: >= 0, Y: >= 0 } && end.X < line.Length && end.Y < characters.Length)
                {
                    var diagonal = new string(Enumerable.Range(0, word.Length)
                        .Select(i => characters[start.Y + i][start.X + i])
                        .ToArray());
                    if (diagonal == word)
                        isDiagonalDown = true;
                }

                // Diagonal Up
                var isDiagonalUp = false;
                start = new Point(x - word.Length / 2, y + word.Length / 2);
                end = new Point(x + word.Length / 2, y - word.Length / 2);
                if (start.X >= 0 && start.Y < characters.Length && end.X < line.Length && end.Y >= 0)
                {
                    var diagonal = new string(Enumerable.Range(0, word.Length)
                        .Select(i => characters[start.Y - i][start.X + i])
                        .ToArray());
                    if (diagonal == word)
                        isDiagonalUp = true;
                }

                // Diagonal Reverse Down
                var isDiagonalReverseDown = false;
                if (!isDiagonalUp)
                {
                    start = new Point(x + word.Length / 2, y - word.Length / 2);
                    end = new Point(x - word.Length / 2, y + word.Length / 2);
                    if (start.X < line.Length && start.Y >= 0 && end.X >= 0 && end.Y < characters.Length)
                    {
                        var diagonal = new string(Enumerable.Range(0, word.Length)
                            .Select(i => characters[start.Y + i][start.X - i])
                            .ToArray());
                        if (diagonal == word)
                            isDiagonalReverseDown = true;
                    }
                }

                // Diagonal Reverse Up
                var isDiagonalReverseUp = false;
                if (!isDiagonalDown)
                {
                    start = new Point(x + word.Length / 2, y + word.Length / 2);
                    end = new Point(x - word.Length / 2, y - word.Length / 2);
                    if (start.X < line.Length && start.Y < characters.Length && end is { X: >= 0, Y: >= 0 })
                    {
                        var diagonal = new string(Enumerable.Range(0, word.Length)
                            .Select(i => characters[start.Y - i][start.X - i])
                            .ToArray());
                        if (diagonal == word)
                            isDiagonalReverseUp = true;
                    }
                }

                if ((isDiagonalDown || isDiagonalReverseUp) && (isDiagonalUp || isDiagonalReverseDown))
                    count++;
            }
        }

        return count;
    }

    public static int CountWord(string content, string word)
    {
        var characters = content.Split(Environment.NewLine)
            .Select(line => line.ToCharArray())
            .ToArray();

        var count = 0;
        foreach (var (y, line) in characters.Index())
        {
            foreach (var (x, ch) in line.Index())
            {
                if (ch != word[0]) continue;

                // Horizontal
                var end = x + word.Length - 1;
                if (end <= line.Length - 1 && line[x..(end + 1)].SequenceEqual(word))
                    count++;

                // Horizontal Reverse
                var start = x - word.Length + 1;
                if (start >= 0 && line[start..(x + 1)]
                        .Reverse()
                        .SequenceEqual(word))
                    count++;

                // Vertical
                end = y + word.Length - 1;
                if (end <= characters.Length - 1 && characters[y..(end + 1)]
                        .Select(row => row[x])
                        .SequenceEqual(word))
                    count++;

                // Vertical Reverse
                start = y - word.Length + 1;
                if (start >= 0 && characters[start..(y + 1)]
                        .Reverse()
                        .Select(row => row[x])
                        .SequenceEqual(word))
                    count++;

                // Diagonal Down
                end = Math.Min(line.Length - x, characters.Length - y);
                if (end >= word.Length &&
                    Enumerable.Range(0, word.Length).All(i => characters[y + i][x + i] == word[i]))
                    count++;

                // Diagonal Up
                end = Math.Min(line.Length - x, y + 1);
                if (end >= word.Length &&
                    Enumerable.Range(0, word.Length).All(i => characters[y - i][x + i] == word[i]))
                    count++;

                // Diagonal Reverse Down
                end = Math.Min(x + 1, characters.Length - y);
                if (end >= word.Length &&
                    Enumerable.Range(0, word.Length).All(i => characters[y + i][x - i] == word[i]))
                    count++;

                // Diagonal Reverse Up
                end = Math.Min(x + 1, y + 1);
                if (end >= word.Length &&
                    Enumerable.Range(0, word.Length).All(i => characters[y - i][x - i] == word[i]))
                    count++;
            }
        }

        return count;
    }
}