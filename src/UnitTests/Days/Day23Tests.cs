using Day23;

namespace UnitTests.Days;

[TestClass]
public class Day23Tests
{
    private ComputerNetwork _network = null!;

    [TestInitialize]
    public void Initialize()
    {
        const string input = """
                             kh-tc
                             qp-kh
                             de-cg
                             ka-co
                             yn-aq
                             qp-ub
                             cg-tb
                             vc-aq
                             tb-ka
                             wh-tc
                             yn-cg
                             kh-ub
                             ta-co
                             de-co
                             tc-td
                             tb-wq
                             wh-td
                             ta-ka
                             td-qp
                             aq-cg
                             wq-ub
                             ub-vc
                             de-ta
                             wq-aq
                             wq-vc
                             wh-yn
                             ka-de
                             kh-ta
                             co-tc
                             wh-qp
                             tb-vc
                             td-yn
                             """;

        _network = new ComputerNetwork(input);
    }

    [TestMethod]
    public void TestPart1()
    {
        Assert.AreEqual(7, Challenge.Part1(_network));
    }

    [TestMethod]
    public void TestPart2()
    {
        Assert.AreEqual("co,de,ka,ta", Challenge.Part2(_network));
    }
}