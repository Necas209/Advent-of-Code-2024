using System.Drawing;
using Day18;

var input = File.ReadAllText("input.txt");

var memorySize = new Size(71, 71);
const int numFallenBytes = 1024;

Console.WriteLine($"Part 1: {Challenge.Part1(input, memorySize, numFallenBytes)}");

Console.WriteLine($"Part 2: {Challenge.Part2(input, memorySize)}");