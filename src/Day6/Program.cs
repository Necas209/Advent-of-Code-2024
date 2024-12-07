using System.Diagnostics;
using Day6;

var input = File.ReadAllText("input.txt");

Console.WriteLine($"First challenge: {Challenges.FirstChallenge(input)}");

var start = Stopwatch.GetTimestamp();
var result = Challenges.SecondChallenge(input);
var end = Stopwatch.GetTimestamp();

Console.WriteLine($"Second challenge: {result}");
Console.WriteLine($"Time: {Stopwatch.GetElapsedTime(start, end).TotalSeconds:F3} seconds");