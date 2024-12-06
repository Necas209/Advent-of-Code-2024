using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day1;

[TestClass]
public class ChallengesTests
{
    private string _testFilePath = null!;

    [TestInitialize]
    public void Setup()
    {
        const string test = """
                            3   4
                            4   3
                            2   5
                            1   3
                            3   9
                            3   3
                            """;
        _testFilePath = Path.GetTempFileName();
        File.WriteAllText(_testFilePath, test);
    }
    
    [TestMethod]
    public void TestFirstChallenge()
    {
        var data = Challenges.ProcessData(_testFilePath);
        Assert.AreEqual(11, Challenges.FirstChallenge(data));
    }
    
    [TestMethod]
    public void TestSecondChallenge()
    {
        var data = Challenges.ProcessData(_testFilePath);
        Assert.AreEqual(31, Challenges.SecondChallenge(data));
    }
}