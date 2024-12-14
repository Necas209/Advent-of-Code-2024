using Day3;

namespace UnitTests.Days;

[TestClass]
public class Day3Tests
{
    [TestMethod]
    public void TestPart1()
    {
        const string test = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
        Assert.AreEqual(161, Challenges.Part1(test));
    }

    [TestMethod]
    public void TestPart2()
    {
        const string doDontTest = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
        Assert.AreEqual(48, Challenges.Part2(doDontTest));
    }
}