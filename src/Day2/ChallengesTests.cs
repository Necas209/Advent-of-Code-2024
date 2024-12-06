using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day2;

[TestClass]
public class ChallengesTests
{
    private string _testFilePath = null!;

    [TestInitialize]
    public void Setup()
    {
        const string test = """
                            7 6 4 2 1
                            1 2 7 8 9
                            9 7 6 2 1
                            1 3 2 4 5
                            8 6 4 4 1
                            1 3 6 7 9
                            """;
        _testFilePath = Path.GetTempFileName();
        File.WriteAllText(_testFilePath, test);
    }

    [TestMethod]
    public void TestFirstChallenge()
    {
        var testData = Challenges.ProcessData(_testFilePath);
        Assert.AreEqual(2, Challenges.FirstChallenge(testData));
    }

    [TestMethod]
    public void TestSecondChallenge()
    {
        var testData = Challenges.ProcessData(_testFilePath);
        Assert.AreEqual(4, Challenges.SecondChallenge(testData));
    }
}