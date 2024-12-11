using Day11;

namespace UnitTests.Days;

[TestClass]
public class Day11Tests
{
    [TestMethod]
    public void TestChallenge()
    {
        const string test = "125 17";

        var testChallenge = new Challenge(6);
        Assert.AreEqual(22, testChallenge.GetStoneCount(test));

        var realChallenge = new Challenge(25);
        Assert.AreEqual(55312, realChallenge.GetStoneCount(test));
    }
}