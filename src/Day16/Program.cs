using Day16;

var input = File.ReadAllText("input.txt");

var maze = new Maze(input);

Console.WriteLine($"Part 1: {Challenge.Part1(maze)}");
Console.WriteLine($"Part 2: {Challenge.Part2(maze)}");