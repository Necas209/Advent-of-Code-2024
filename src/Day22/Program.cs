using System.Diagnostics;
using Day22;

var input = File.ReadAllText("input.txt");

Time(() => Console.WriteLine($"Part 1: {Challenge.Part1(input)}"));

Time(() => Console.WriteLine($"Part 2: {Challenge.Part2(input)}"));
return;

static void Time(Action action)
{
    var start = Stopwatch.GetTimestamp();
    action();
    var end = Stopwatch.GetTimestamp();
    Console.WriteLine($"Elapsed time: {Stopwatch.GetElapsedTime(start, end).TotalMilliseconds}ms");
}