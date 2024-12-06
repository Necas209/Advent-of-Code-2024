using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day4;

[TestClass]
public class WordSearchTests
{
    [TestMethod]
    public void TestCountWord()
    {
        const string test = """
                            MMMSXXMASM
                            MSAMXMSMSA
                            AMXSXMAAMM
                            MSAMASMSMX
                            XMASAMXAMM
                            XXAMMXXAMA
                            SMSMSASXSS
                            SAXAMASAAA
                            MAMMMXMMMM
                            MXMXAXMASX
                            """;
        var testResult = WordSearch.CountWord(test, "XMAS");
        Assert.AreEqual(18, testResult);
    }
    
    [TestMethod]
    public void TestCountWordX()
    {
        const string test = """
                            MMMSXXMASM
                            MSAMXMSMSA
                            AMXSXMAAMM
                            MSAMASMSMX
                            XMASAMXAMM
                            XXAMMXXAMA
                            SMSMSASXSS
                            SAXAMASAAA
                            MAMMMXMMMM
                            MXMXAXMASX
                            """;
        var testResult = WordSearch.CountWordX(test, "MAS");
        Assert.AreEqual(9, testResult);
    }
}