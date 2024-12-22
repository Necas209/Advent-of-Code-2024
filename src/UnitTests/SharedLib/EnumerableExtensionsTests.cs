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
   public void TestAdjacentWithValidLength()
    {
         var source = new[] { 1, 2, 3, 4, 5 };
         var result = source.Adjacent(3).ToArray();
    
         Assert.AreEqual(3, result.Length);
         Assert.IsTrue(result[0] is [1, 2, 3]);
         Assert.IsTrue(result[1] is [2, 3, 4]);
         Assert.IsTrue(result[2] is [3, 4, 5]);
    }
   
    [TestMethod]
    public void TestAdjacentWithInvalidLength()
    {
        var source = new[] { 1, 2, 3, 4, 5 };
        var result = source.Adjacent(6).ToArray();

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

    [TestMethod]
    public void TestCombinationsWithZeroLength()
    {
        var source = new[] { 1, 2, 3, 4, 5 };
        var result = source.Combinations(0)
            .Select(x => x.ToImmutableArray())
            .ToImmutableArray();
        
        Assert.AreEqual(1, result.Length);
        Assert.IsTrue(result[0] is []);
    }

    [TestMethod]
    public void TestCombinationsWithOneLength()
    {
        var source = new[] { 1, 2, 3, 4, 5 };
        var result = source.Combinations(1)
            .Select(x => x.ToImmutableArray())
            .ToImmutableArray();

        Assert.AreEqual(5, result.Length);
        Assert.IsTrue(result[0] is [1]);
        Assert.IsTrue(result[1] is [2]);
        Assert.IsTrue(result[2] is [3]);
        Assert.IsTrue(result[3] is [4]);
        Assert.IsTrue(result[4] is [5]);
    }
    
    [TestMethod]
    public void TestCombinationsWithTwoLength()
    {
        var source = new[] { 1, 2, 3, 4, 5 };
        var result = source.Combinations(2)
            .Select(x => x.ToImmutableArray())
            .ToImmutableArray();

        Assert.AreEqual(10, result.Length);
        Assert.IsTrue(result[0] is [1, 2]);
        Assert.IsTrue(result[1] is [1, 3]);
        Assert.IsTrue(result[2] is [1, 4]);
        Assert.IsTrue(result[3] is [1, 5]);
        Assert.IsTrue(result[4] is [2, 3]);
        Assert.IsTrue(result[5] is [2, 4]);
        Assert.IsTrue(result[6] is [2, 5]);
        Assert.IsTrue(result[7] is [3, 4]);
        Assert.IsTrue(result[8] is [3, 5]);
        Assert.IsTrue(result[9] is [4, 5]);
    }
    
    [TestMethod]
    public void TestCombinationsWithThreeLength()
    {
        var source = new[] { 1, 2, 3, 4, 5 };
        var result = source.Combinations(3)
            .Select(x => x.ToImmutableArray())
            .ToImmutableArray();

        Assert.AreEqual(10, result.Length);
        Assert.IsTrue(result[0] is [1, 2, 3]);
        Assert.IsTrue(result[1] is [1, 2, 4]);
        Assert.IsTrue(result[2] is [1, 2, 5]);
        Assert.IsTrue(result[3] is [1, 3, 4]);
        Assert.IsTrue(result[4] is [1, 3, 5]);
        Assert.IsTrue(result[5] is [1, 4, 5]);
        Assert.IsTrue(result[6] is [2, 3, 4]);
        Assert.IsTrue(result[7] is [2, 3, 5]);
        Assert.IsTrue(result[8] is [2, 4, 5]);
        Assert.IsTrue(result[9] is [3, 4, 5]);
    }
}