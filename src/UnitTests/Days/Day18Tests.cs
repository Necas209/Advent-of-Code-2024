using System.Drawing;
using Day18;

namespace UnitTests.Days;

[TestClass]
public class Day18Tests
{
    [TestMethod]
    public void Test()
    {
        const string input = """
                             5,4
                             4,2
                             4,5
                             3,0
                             2,1
                             6,3
                             2,4
                             1,5
                             0,6
                             3,3
                             2,6
                             5,1
                             1,2
                             5,5
                             2,5
                             6,5
                             1,4
                             0,4
                             6,4
                             1,1
                             6,1
                             1,0
                             0,5
                             1,6
                             2,0
                             """;

        var memorySize = new Size(7, 7);
        const int numFallenBytes = 12;
        var challenge = new Challenge(input, memorySize, numFallenBytes);

        Assert.AreEqual(22, challenge.Part1());

        var expected = new Point(6, 1);
        Assert.AreEqual(expected, challenge.Part2());
    }
}