using Day20;

namespace UnitTests.Days;

[TestClass]
public class Day20Tests
{
    [TestMethod]
    public void TestPart1()
    {
        const string input = """
                             ###############
                             #...#...#.....#
                             #.#.#.#.#.###.#
                             #S#...#.#.#...#
                             #######.#.#.###
                             #######.#.#...#
                             #######.#.###.#
                             ###..E#...#...#
                             ###.#######.###
                             #...###...#...#
                             #.#####.#.###.#
                             #.#...#.#.#...#
                             #.#.#.#.#.#.###
                             #...#...#...###
                             ###############
                             """;

        var raceTrack = new RaceTrack(input);
        var shortestPath = raceTrack.FindShortestPath();
        Assert.AreEqual(84, shortestPath.Length - 1);

        Assert.AreEqual(44, raceTrack.GetCheats(2));
        Assert.AreEqual(30, raceTrack.GetCheats(4));
        Assert.AreEqual(16, raceTrack.GetCheats(6));
        Assert.AreEqual(14, raceTrack.GetCheats(8));
        Assert.AreEqual(10, raceTrack.GetCheats(10));
        Assert.AreEqual(8, raceTrack.GetCheats(12));
        Assert.AreEqual(5, raceTrack.GetCheats(20));
        Assert.AreEqual(4, raceTrack.GetCheats(36));
        Assert.AreEqual(3, raceTrack.GetCheats(38));
        Assert.AreEqual(2, raceTrack.GetCheats(40));
        Assert.AreEqual(1, raceTrack.GetCheats(64));
    }

    [TestMethod]
    public void TestPart2()
    {
        const string input = """
                             ###############
                             #...#...#.....#
                             #.#.#.#.#.###.#
                             #S#...#.#.#...#
                             #######.#.#.###
                             #######.#.#...#
                             #######.#.###.#
                             ###..E#...#...#
                             ###.#######.###
                             #...###...#...#
                             #.#####.#.###.#
                             #.#...#.#.#...#
                             #.#.#.#.#.#.###
                             #...#...#...###
                             ###############
                             """;

        // Initialize the racetrack
        var raceTrack = new RaceTrack(input);

        // Test the number of cheats for specific save amounts
        Assert.AreEqual(285, raceTrack.GetCheats(50, 20));
        Assert.AreEqual(253, raceTrack.GetCheats(52, 20));
        Assert.AreEqual(222, raceTrack.GetCheats(54, 20));
        Assert.AreEqual(193, raceTrack.GetCheats(56, 20));
        Assert.AreEqual(154, raceTrack.GetCheats(58, 20));
        Assert.AreEqual(129, raceTrack.GetCheats(60, 20));
        Assert.AreEqual(106, raceTrack.GetCheats(62, 20));
        Assert.AreEqual(86, raceTrack.GetCheats(64, 20));
        Assert.AreEqual(67, raceTrack.GetCheats(66, 20));
        Assert.AreEqual(55, raceTrack.GetCheats(68, 20));
        Assert.AreEqual(41, raceTrack.GetCheats(70, 20));
        Assert.AreEqual(29, raceTrack.GetCheats(72, 20));
        Assert.AreEqual(7, raceTrack.GetCheats(74, 20));
        Assert.AreEqual(3, raceTrack.GetCheats(76, 20));
    }
}