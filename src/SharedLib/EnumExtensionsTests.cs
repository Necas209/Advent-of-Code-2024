using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharedLib;

[TestClass]
public class EnumExtensionsTests
{
    [Flags]
    private enum ExampleFlags
    {
        Flag1 = 1 << 0,
        Flag2 = 1 << 1,
        Flag3 = 1 << 2,
        Flag4 = 1 << 3
    }

    [TestMethod]
    public void TestHasAnyFlag()
    {
        const ExampleFlags flags = ExampleFlags.Flag1 | ExampleFlags.Flag2 | ExampleFlags.Flag4;

        Assert.IsTrue(flags.HasAnyFlag(ExampleFlags.Flag1));
        Assert.IsTrue(flags.HasAnyFlag(ExampleFlags.Flag2));
        Assert.IsTrue(flags.HasAnyFlag(ExampleFlags.Flag4));
        Assert.IsFalse(flags.HasAnyFlag(ExampleFlags.Flag3));
    }
}