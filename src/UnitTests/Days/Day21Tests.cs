using Day21;

namespace UnitTests.Days;

[TestClass]
public class Day21Tests
{
    [TestMethod]
    public void TestPart1()
    {
        const string input = """
                             029A
                             980A
                             179A
                             456A
                             379A
                             """;
        
        Assert.AreEqual(126384, Challenge.Part1(input));
    }
}