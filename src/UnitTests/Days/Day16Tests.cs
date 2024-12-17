
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
        
        var maze = new Maze(input);
        Assert.AreEqual(7036, Challenge.Part1(maze));
        Assert.AreEqual(45, Challenge.Part2(maze));
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
        
        var maze = new Maze(input);
        Assert.AreEqual(11048, Challenge.Part1(maze));
        Assert.AreEqual(64, Challenge.Part2(maze));
    }
}