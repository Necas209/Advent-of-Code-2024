using System.Diagnostics;
using Day4;

const string test = """
                    MMMSXXMASM
                    MSAMXMSMSA
                    AMXSXMAAMM
                    MSAMASMSMX
                    XMASAMXAMM
                    XXAMMXXAMA
                    SMSMSASXSS
                    SAXAMASAAA
                    MAMMMXMMMM
                    MXMXAXMASX
                    """;

var testResult = WordSearch.CountWord(test, "XMAS");
Debug.Assert(testResult == 18, $"Expected 18 but got {testResult}");

var content = File.ReadAllText("input.txt");
var result = WordSearch.CountWord(content, "XMAS");
Console.WriteLine($"Word XMAS appears {result} times");

var testResult2 = WordSearch.CountWordX(test, "MAS");
Debug.Assert(testResult2 == 9, $"Expected 9 but got {testResult2}");

var result2 = WordSearch.CountWordX(content, "MAS");
Console.WriteLine($"Word MAS appears in an X (X-MAS) {result2} times");