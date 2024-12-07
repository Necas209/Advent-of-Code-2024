using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day6;

[TestClass]
public class ChallengesTests
{
    [TestMethod]
    public void TestFirstChallenge()
    {
        const string map = """
                           ....#.....
                           .........#
                           ..........
                           ..#.......
                           .......#..
                           ..........
                           .#..^.....
                           ........#.
                           #.........
                           ......#...
                           """;
        
        Assert.AreEqual(41, Challenges.FirstChallenge(map));
    }
    
    [TestMethod]
    public void TestSecondChallenge()
    {
        const string map = """
                           ....#.....
                           .........#
                           ..........
                           ..#.......
                           .......#..
                           ..........
                           .#..^.....
                           ........#.
                           #.........
                           ......#...
                           """;
        
        Assert.AreEqual(6, Challenges.SecondChallenge(map));
    }
}