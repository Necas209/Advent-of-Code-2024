namespace Day12;

public static class Challenges
{
    public static int Part1(string input)
    {
        var gardenPlotMap = new GardenPlotMap(input);

        return gardenPlotMap.GetGardenPlots().Sum(plot => plot.Area * plot.GetPerimeter());
    }

    public static int Part2(string input)
    {
        var gardenPlotMap = new GardenPlotMap(input);

        return gardenPlotMap.GetGardenPlots().Sum(plot => plot.Area * plot.GetNumberOfSides());
    }
}