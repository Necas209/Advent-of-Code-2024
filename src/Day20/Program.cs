using Day20;

var input = File.ReadAllText("input.txt");

var raceTrack = new RaceTrack(input);

Console.WriteLine($"Part 1: {raceTrack.GetCheats(100)}");

Console.WriteLine($"Part 2: {raceTrack.GetCheats(100, 20)}");