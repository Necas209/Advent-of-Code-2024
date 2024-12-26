using Day25;

namespace UnitTests.Days;

[TestClass]
public class Day25Tests
{
    [TestMethod]
    public void TestPart1()
    {
        const string input = """
                             #####
                             .####
                             .####
                             .####
                             .#.#.
                             .#...
                             .....

                             #####
                             ##.##
                             .#.##
                             ...##
                             ...#.
                             ...#.
                             .....

                             .....
                             #....
                             #....
                             #...#
                             #.#.#
                             #.###
                             #####

                             .....
                             .....
                             #.#..
                             ###..
                             ###.#
                             ###.#
                             #####

                             .....
                             .....
                             .....
                             #....
                             #.#..
                             #.#.#
                             #####
                             """;

        Assert.AreEqual(3, Challenge.Part1(input));
    }
}