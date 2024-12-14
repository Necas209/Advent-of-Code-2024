using Day9;

namespace UnitTests.Days;

[TestClass]
public class Day9Tests
{
    [TestMethod]
    public void TestPart1()
    {
        const string test = "2333133121414131402";
        var disk = new Disk(test);

        Assert.AreEqual("00...111...2...333.44.5555.6666.777.888899", disk.ToString());
        Assert.AreEqual(1928, Challenges.Part1(test));
    }
    
    [TestMethod]
    public void TestPart2()
    {
        const string test = "2333133121414131402";

        Assert.AreEqual(2858, Challenges.Part2(test));
    }
}