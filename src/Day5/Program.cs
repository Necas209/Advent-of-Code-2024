using System.Diagnostics;
using Day5;

const string test = """
                    47|53
                    97|13
                    97|61
                    97|47
                    75|29
                    61|13
                    75|53
                    29|13
                    97|29
                    53|29
                    61|53
                    97|53
                    61|29
                    47|13
                    75|47
                    97|75
                    47|61
                    75|61
                    47|29
                    75|13
                    53|13

                    75,47,61,53,29
                    97,61,53,29,13
                    75,29,13
                    75,97,47,61,53
                    61,13,29
                    97,13,75,29,47
                    """;
var tempFile = Path.GetTempFileName();
File.WriteAllText(tempFile, test);

var testData = Challenges.ProcessData(tempFile);
Debug.Assert(Challenges.FirstChallenge(testData) == 143, "Test 1 Failed");
Debug.Assert(Challenges.SecondChallenge(testData) == 123, "Test 2 Failed");

Console.WriteLine("All tests passed");

var realData = Challenges.ProcessData("input.txt");
Console.WriteLine($"First Challenge: {Challenges.FirstChallenge(realData)}");
Console.WriteLine($"Second Challenge: {Challenges.SecondChallenge(realData)}");