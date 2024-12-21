using System.Diagnostics;
using Day20;

var input = File.ReadAllText("input.txt");

var raceTrack = new RaceTrack(input);

var start = Stopwatch.GetTimestamp();
var cheats = raceTrack.GetCheats(100);
var end = Stopwatch.GetTimestamp();

Console.WriteLine($"Part 1: {cheats}");
Console.WriteLine($"Elapsed time: {Stopwatch.GetElapsedTime(start, end).TotalMilliseconds}ms");

start = Stopwatch.GetTimestamp();
cheats = raceTrack.GetCheats(100, 20);
end = Stopwatch.GetTimestamp();

Console.WriteLine($"Part 2: {cheats}");
Console.WriteLine($"Elapsed time: {Stopwatch.GetElapsedTime(start, end).TotalMilliseconds}ms");