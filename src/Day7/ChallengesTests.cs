using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day7;

[TestClass]
public class ChallengesTests
{
    [TestMethod]
    public void TestFirstChallenge()
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

        Assert.AreEqual(3749, Challenges.FirstChallenge(test));
    }
    
    [TestMethod]
    public void TestSecondChallenge()
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

        Assert.AreEqual(11387, Challenges.SecondChallenge(test));
    }
}