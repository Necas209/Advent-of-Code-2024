namespace Day12;

public static class Challenges
{
    public static int FirstChallenge(string input)
    {
        var gardenPlotMap = new GardenPlotMap(input);

        return gardenPlotMap.GetGardenPlots().Sum(plot => plot.Area * plot.GetPerimeter());
    }

    public static int SecondChallenge(string input)
    {
        var gardenPlotMap = new GardenPlotMap(input);

        return gardenPlotMap.GetGardenPlots().Sum(plot => plot.Area * plot.GetNumberOfSides());
    }
}