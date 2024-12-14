using Day3;

var allText = File.ReadAllText("input.txt");
Console.WriteLine($"First Challenge: {Challenges.Part1(allText)}");
Console.WriteLine($"Second Challenge: {Challenges.Part2(allText)}");