using Day10;

namespace UnitTests.Days;

[TestClass]
public class Day10Tests
{
    [TestMethod]
    public void TestFirstChallenge()
    {
        const string test = """
                            89010123
                            78121874
                            87430965
                            96549874
                            45678903
                            32019012
                            01329801
                            10456732
                            """;

        Assert.AreEqual(36, Challenges.FirstChallenge(test));
    }
    
    [TestMethod]
    public void TestSecondChallenge()
    {
        const string test = """
                            89010123
                            78121874
                            87430965
                            96549874
                            45678903
                            32019012
                            01329801
                            10456732
                            """;

        Assert.AreEqual(81, Challenges.SecondChallenge(test));
    }
}