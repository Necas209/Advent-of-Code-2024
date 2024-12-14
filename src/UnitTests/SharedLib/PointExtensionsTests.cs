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
        var (left, right, top, bottom) = point.GetNeighbors();
        
        Assert.AreEqual(new Point(-1, 0), left);
        Assert.AreEqual(new Point(1, 0), right);
        Assert.AreEqual(new Point(0, -1), top);
        Assert.AreEqual(new Point(0, 1), bottom);
    }
}