using Day12;

namespace UnitTests.Days;

[TestClass]
public class Day12Tests
{
    [TestMethod]
    public void TestPart1()
    {
        const string input = """
                             RRRRIICCFF
                             RRRRIICCCF
                             VVRRRCCFFF
                             VVRCCCJFFF
                             VVVVCJJCFE
                             VVIVCCJJEE
                             VVIIICJJEE
                             MIIIIIJJEE
                             MIIISIJEEE
                             MMMISSJEEE
                             """;

        Assert.AreEqual(1930, Challenges.Part1(input));
    }
    
    [TestMethod]
    public void TestPart2()
    {
        const string input = """
                             RRRRIICCFF
                             RRRRIICCCF
                             VVRRRCCFFF
                             VVRCCCJFFF
                             VVVVCJJCFE
                             VVIVCCJJEE
                             VVIIICJJEE
                             MIIIIIJJEE
                             MIIISIJEEE
                             MMMISSJEEE
                             """;

        Assert.AreEqual(1206, Challenges.Part2(input));
    }
}