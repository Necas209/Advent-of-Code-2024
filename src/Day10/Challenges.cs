namespace Day10;

public static class Challenges
{
    public static int Part1(string input)
    {
        var map = new TopographicMap(input);

        var trailheads = map.FindTrailheads();
        var totalScore = trailheads.Select(trailhead => map.GetScore(trailhead, false)).Sum();

        return totalScore;
    }

    public static int Part2(string input)
    {
        var map = new TopographicMap(input);

        var trailheads = map.FindTrailheads();
        var totalScore = trailheads.Select(trailhead => map.GetScore(trailhead, true)).Sum();

        return totalScore;
    }
}