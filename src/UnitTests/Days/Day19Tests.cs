using Day19;

namespace UnitTests.Days;

[TestClass]
public class Day19Tests
{
    [TestMethod]
    public void TestPart1()
    {
        const string input = """
                             r, wr, b, g, bwu, rb, gb, br
                             
                             brwrr
                             bggr
                             gbbr
                             rrbgbr
                             ubwu
                             bwurrg
                             brgr
                             bbrgwb
                             """;
        
        Assert.AreEqual(6, Challenge.Part1(input));
    }
    
    [TestMethod]
    public void TestPart2()
    {
        const string input = """
                             r, wr, b, g, bwu, rb, gb, br

                             brwrr
                             bggr
                             gbbr
                             rrbgbr
                             ubwu
                             bwurrg
                             brgr
                             bbrgwb
                             """;
        
        Assert.AreEqual(16, Challenge.Part2(input));
    }
}