using Day6;

namespace UnitTests.Days;

[TestClass]
public class Day6Tests
{
    [TestMethod]
    public void TestPart1()
    {
        const string map = """
                           ....#.....
                           .........#
                           ..........
                           ..#.......
                           .......#..
                           ..........
                           .#..^.....
                           ........#.
                           #.........
                           ......#...
                           """;

        Assert.AreEqual(41, Challenges.Part1(map));
    }

    [TestMethod]
    public void TestPart2()
    {
        const string map = """
                           ....#.....
                           .........#
                           ..........
                           ..#.......
                           .......#..
                           ..........
                           .#..^.....
                           ........#.
                           #.........
                           ......#...
                           """;

        Assert.AreEqual(6, Challenges.Part2(map));
    }
}