using Day14;

var input = File.ReadAllText("input.txt");

var challenge = new Challenge(101, 103);

Console.WriteLine($"Part 1: {challenge.Part1(input)}");
Console.WriteLine($"Part 2: {challenge.Part2(input, true)}");