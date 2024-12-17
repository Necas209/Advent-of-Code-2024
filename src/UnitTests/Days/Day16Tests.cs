
using Day16;

namespace UnitTests.Days;

[TestClass]
public class Day16Tests
{
    [TestMethod]
    public void TestWithSmallMaze()
    {
        const string input = """
                             ###############
                             #.......#....E#
                             #.#.###.#.###.#
                             #.....#.#...#.#
                             #.###.#####.#.#
                             #.#.#.......#.#
                             #.#.#####.###.#
                             #...........#.#
                             ###.#.#####.#.#
                             #...#.....#.#.#
                             #.#.#.###.#.#.#
                             #.....#...#.#.#
                             #.###.#.#.#.#.#
                             #S..#.....#...#
                             ###############
                             """;
        
        Assert.AreEqual(7036, Challenge.Part1(input));
        Assert.AreEqual(45, Challenge.Part2(input));
    }

    [TestMethod]
    public void TestWithLargeMaze()
    {
        const string input = """
                             #################
                             #...#...#...#..E#
                             #.#.#.#.#.#.#.#.#
                             #.#.#.#...#...#.#
                             #.#.#.#.###.#.#.#
                             #...#.#.#.....#.#
                             #.#.#.#.#.#####.#
                             #.#...#.#.#.....#
                             #.#.#####.#.###.#
                             #.#.#.......#...#
                             #.#.###.#####.###
                             #.#.#...#.....#.#
                             #.#.#.#####.###.#
                             #.#.#.........#.#
                             #.#.#.#########.#
                             #S#.............#
                             #################
                             """;

        Assert.AreEqual(11048, Challenge.Part1(input));
        Assert.AreEqual(64, Challenge.Part2(input));
    }
}