using System.Drawing;
using Day18;

var input = File.ReadAllText("input.txt");

var challenge = new Challenge(input, new Size(71, 71), 1024);

Console.WriteLine($"Part 1: {challenge.Part1()}");

Console.WriteLine($"Part 2: {challenge.Part2()}");