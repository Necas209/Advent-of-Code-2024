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

    [TestMethod]
    public void TestManhattanDistance()
    {
        var point = new Point(0, 0);
        var other = new Point(3, 4);

        Assert.AreEqual(7, point.ManhattanDistance(other));
    }
    
    [TestMethod]
    public void TestManhattanDistance_SamePoint()
    {
        var point = new Point(0, 0);
        var other = new Point(0, 0);

        Assert.AreEqual(0, point.ManhattanDistance(other));
    }
    
    [TestMethod]
    public void TestManhattanDistance_Negative()
    {
        var point = new Point(0, 0);
        var other = new Point(-3, -4);

        Assert.AreEqual(7, point.ManhattanDistance(other));
    }
}