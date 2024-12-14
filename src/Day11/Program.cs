using Day11;

const string input = "6563348 67 395 0 6 4425 89567 739318";

var Part1 = new Challenge(25);
Console.WriteLine($"First challenge: {Part1.GetStoneCount(input)}");

var Part2 = new Challenge(75);
Console.WriteLine($"Second challenge: {Part2.GetStoneCount(input)}");