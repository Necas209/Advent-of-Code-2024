using System.Diagnostics;
using Day24;

var input = File.ReadAllText("input.txt");

Time(() => Challenge.Part1(input), "Part 1: {0}");

Time(() => Challenge.Part2(input), "Part 2: {0}");

return;

static void Time<T>(Func<T> func, string? format = null)
{
    var start = Stopwatch.GetTimestamp();
    var result = func();
    var end = Stopwatch.GetTimestamp();
    if (format is not null) Console.WriteLine(format, result);
    Console.WriteLine($"Elapsed time: {Stopwatch.GetElapsedTime(start, end).TotalMilliseconds}ms");
}