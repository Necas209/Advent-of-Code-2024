using Day7;

namespace UnitTests.Days;

[TestClass]
public class Day7Tests
{
    [TestMethod]
    public void TestPart1()
    {
        const string test = """
                            190: 10 19
                            3267: 81 40 27
                            83: 17 5
                            156: 15 6
                            7290: 6 8 6 15
                            161011: 16 10 13
                            192: 17 8 14
                            21037: 9 7 18 13
                            292: 11 6 16 20
                            """;

        Assert.AreEqual(3749, Challenges.Part1(test));
    }
    
    [TestMethod]
    public void TestPart2()
    {
        const string test = """
                            190: 10 19
                            3267: 81 40 27
                            83: 17 5
                            156: 15 6
                            7290: 6 8 6 15
                            161011: 16 10 13
                            192: 17 8 14
                            21037: 9 7 18 13
                            292: 11 6 16 20
                            """;

        Assert.AreEqual(11387, Challenges.Part2(test));
    }
}