using Day12;

namespace UnitTests.Days;

[TestClass]
public class Day12Tests
{
    [TestMethod]
    public void TestFirstChallenge()
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

        Assert.AreEqual(1930, Challenges.FirstChallenge(input));
    }
    
    [TestMethod]
    public void TestSecondChallenge()
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

        Assert.AreEqual(1206, Challenges.SecondChallenge(input));
    }
}