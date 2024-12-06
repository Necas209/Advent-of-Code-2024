using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day5;

[TestClass]
public class ChallengesTests
{
    private string _testFilePath = null!;

    [TestInitialize]
    public void Setup()
    {
        const string test = """
                            47|53
                            97|13
                            97|61
                            97|47
                            75|29
                            61|13
                            75|53
                            29|13
                            97|29
                            53|29
                            61|53
                            97|53
                            61|29
                            47|13
                            75|47
                            97|75
                            47|61
                            75|61
                            47|29
                            75|13
                            53|13

                            75,47,61,53,29
                            97,61,53,29,13
                            75,29,13
                            75,97,47,61,53
                            61,13,29
                            97,13,75,29,47
                            """;
        _testFilePath = Path.GetTempFileName();
        File.WriteAllText(_testFilePath, test);
    }

    [TestMethod]
    public void TestFirstChallenge()
    {
        var testData = Challenges.ProcessData(_testFilePath);
        Assert.AreEqual(143, Challenges.FirstChallenge(testData));
    }
    
    [TestMethod]
    public void TestSecondChallenge()
    {
        var testData = Challenges.ProcessData(_testFilePath);
        Assert.AreEqual(123, Challenges.SecondChallenge(testData));
    }
}