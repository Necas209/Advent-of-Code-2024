using Day10;

namespace UnitTests.Days;

[TestClass]
public class Day10Tests
{
    [TestMethod]
    public void TestPart1()
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

        Assert.AreEqual(36, Challenges.Part1(test));
    }
    
    [TestMethod]
    public void TestPart2()
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

        Assert.AreEqual(81, Challenges.Part2(test));
    }
}