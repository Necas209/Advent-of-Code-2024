using Day11;

const string input = "6563348 67 395 0 6 4425 89567 739318";

var firstChallenge = new Challenge(25);
Console.WriteLine($"First challenge: {firstChallenge.GetStoneCount(input)}");

var secondChallenge = new Challenge(75);
Console.WriteLine($"Second challenge: {secondChallenge.GetStoneCount(input)}");