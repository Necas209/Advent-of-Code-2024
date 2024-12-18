using Day17;

namespace UnitTests.Days;

[TestClass]
public class Day17Tests
{
    [TestMethod]
    public void TestPart1()
    {
        const string input = """
                             Register A: 729
                             Register B: 0
                             Register C: 0

                             Program: 0,1,5,4,3,0
                             """;

        Assert.AreEqual("4,6,3,5,6,3,5,2,1,0", Challenge.Part1(input));
    }

    [TestMethod]
    public void TestPart2()
    {
        const string input = """
                             Register A: 2024
                             Register B: 0
                             Register C: 0

                             Program: 0,3,5,4,3,0
                             """;

        Assert.AreEqual(117440, Challenge.Part2(input));
    }
}