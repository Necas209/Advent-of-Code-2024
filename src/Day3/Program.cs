using Day3;

var allText = File.ReadAllText("input.txt");
Console.WriteLine($"First Challenge: {Challenges.FirstChallenge(allText)}");
Console.WriteLine($"Second Challenge: {Challenges.SecondChallenge(allText)}");