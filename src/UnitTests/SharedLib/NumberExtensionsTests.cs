using SharedLib;

namespace UnitTests.SharedLib;

[TestClass]
public class NumberExtensionsTests
{
    [TestMethod]
    public void TestNumberOfDigits()
    {
        Assert.AreEqual(1, 0.NumberOfDigits());
        Assert.AreEqual(1, 1.NumberOfDigits());
        Assert.AreEqual(2, 10.NumberOfDigits());
        Assert.AreEqual(1, (-1).NumberOfDigits());
        Assert.AreEqual(2, (-10).NumberOfDigits());
    }

    [TestMethod]
    public void TestSplitNumber()
    {
        Assert.AreEqual((0, 0), 0.SplitNumber());
        Assert.AreEqual((0, 1), 1.SplitNumber());
        Assert.AreEqual((1, 0), 10.SplitNumber());
        Assert.AreEqual((12, 34), 1234.SplitNumber());
        Assert.AreEqual((123, 456), 123456.SplitNumber());
        Assert.AreEqual((1234, 567), 1234567.SplitNumber());
    }
}