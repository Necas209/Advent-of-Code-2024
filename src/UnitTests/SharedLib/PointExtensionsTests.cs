using System.Collections.Immutable;
using System.Drawing;
using SharedLib;

namespace UnitTests.SharedLib;

[TestClass]
public class PointExtensionsTests
{
    [TestMethod]
    public void TestGetNeighbors()
    {
        var point = new Point(0, 0);
        var neighbors = point.GetNeighbors().ToImmutableArray();

        Assert.AreEqual(4, neighbors.Length);
        Assert.IsTrue(neighbors.Contains(new Point(-1, 0)));
        Assert.IsTrue(neighbors.Contains(new Point(1, 0)));
        Assert.IsTrue(neighbors.Contains(new Point(0, -1)));
        Assert.IsTrue(neighbors.Contains(new Point(0, 1)));
    }
}