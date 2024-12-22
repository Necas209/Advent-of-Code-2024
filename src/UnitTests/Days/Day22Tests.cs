using Day22;

namespace UnitTests.Days;

[TestClass]
public class Day22Tests
{
    [TestMethod]
    public void TestPart1()
    {
        const string input = """
                             1
                             10
                             100
                             2024
                             """;

        Assert.AreEqual(37327623, Challenge.Part1(input));
    }

    [TestMethod]
    public void TestPart2()
    {
        const string input = """
                             1
                             2
                             3
                             2024
                             """;

        Assert.AreEqual(23, Challenge.Part2(input));
    }
}