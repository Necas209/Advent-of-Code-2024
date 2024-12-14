using System.Diagnostics;
using Day6;

var input = File.ReadAllText("input.txt");

Console.WriteLine($"First challenge: {Challenges.Part1(input)}");

var start = Stopwatch.GetTimestamp();
var result = Challenges.Part2(input);
var end = Stopwatch.GetTimestamp();

Console.WriteLine($"Second challenge: {result}");
Console.WriteLine($"Time: {Stopwatch.GetElapsedTime(start, end).TotalSeconds:F3} seconds");