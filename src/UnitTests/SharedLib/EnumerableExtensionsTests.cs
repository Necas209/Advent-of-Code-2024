using System.Collections.Immutable;
using SharedLib;

namespace UnitTests.SharedLib;

[TestClass]
public class EnumerableExtensionsTests
{
    [TestMethod]
    public void TestPairwiseWithElements()
    {
        var source = new[] { 1, 2, 3, 4, 5 };
        var result = source.Pairwise().ToImmutableArray();

        Assert.AreEqual(4, result.Length);
        Assert.AreEqual((1, 2), result[0]);
        Assert.AreEqual((2, 3), result[1]);
        Assert.AreEqual((3, 4), result[2]);
        Assert.AreEqual((4, 5), result[3]);
    }

    [TestMethod]
    public void TestPairwiseWithOneElement()
    {
        var source = new[] { 1 };
        var result = source.Pairwise().ToImmutableArray();

        Assert.AreEqual(0, result.Length);
    }

    [TestMethod]
    public void TestPairwiseWithNoElements()
    {
        var source = Array.Empty<int>();
        var result = source.Pairwise().ToImmutableArray();

        Assert.AreEqual(0, result.Length);
    }

    [TestMethod]
    public void TestSkipAtWithValidIndex()
    {
        var source = new[] { 1, 2, 3, 4, 5 };
        var result = source.SkipAt(4).ToImmutableArray();

        Assert.AreEqual(4, result.Length);
        Assert.AreEqual(1, result[0]);
        Assert.AreEqual(2, result[1]);
        Assert.AreEqual(3, result[2]);
        Assert.AreEqual(4, result[3]);
    }
    
    [TestMethod]
    public void TestSkipAtWithInvalidIndex()
    {
        var source = new[] { 1, 2, 3, 4, 5 };
        var result = source.SkipAt(5).ToImmutableArray();

        Assert.AreEqual(5, result.Length);
        Assert.AreEqual(1, result[0]);
        Assert.AreEqual(2, result[1]);
        Assert.AreEqual(3, result[2]);
        Assert.AreEqual(4, result[3]);
        Assert.AreEqual(5, result[4]);
    }
}