using System.Collections.Immutable;

namespace Day15;

public static class Challenge
{
    public static int Part1(string input)
    {
        var lines = input.Split("\n\n");

        var map = lines[0].Split("\n")
            .Select(x => x.ToCharArray())
            .ToArray();
        var moves = lines[1].Split("\n")
            .SelectMany(x => x).Select(x => (Move)x)
            .ToImmutableArray();

        var warehouse = new Warehouse(map, moves);
        warehouse.Run();

        var boxes = warehouse.FindBoxes();
        return boxes.Sum(Warehouse.GetGpsCoordinate);
    }

    public static int Part2(string input)
    {
        var lines = input.Split("\n\n");

        var map = lines[0].Split("\n")
            .Select(x => x.ToCharArray())
            .ToArray();
        var moves = lines[1].Split("\n")
            .SelectMany(x => x).Select(x => (Move)x)
            .ToImmutableArray();

        var newMap = new char[map.Length][];
        for (var i = 0; i < map.Length; i++)
        {
            newMap[i] = new char[map[0].Length * 2];
            for (var j = 0; j < map[0].Length; j++)
            {
                switch (map[i][j])
                {
                    case Warehouse.Box:
                        newMap[i][j * 2] = Warehouse.WideBoxLeft;
                        newMap[i][j * 2 + 1] = Warehouse.WideBoxRight;
                        break;
                    case Warehouse.Robot:
                        newMap[i][j * 2] = Warehouse.Robot;
                        newMap[i][j * 2 + 1] = Warehouse.Empty;
                        break;
                    default:
                        newMap[i][j * 2] = map[i][j];
                        newMap[i][j * 2 + 1] = map[i][j];
                        break;
                }
            }
        }

        var warehouse = new Warehouse(newMap, moves);
        warehouse.Run();

        var boxes = warehouse.FindBoxes();
        return boxes.Sum(Warehouse.GetGpsCoordinate);
    }
}