using Day4;

var content = File.ReadAllText("input.txt");

var result = WordSearch.CountWord(content, "XMAS");
Console.WriteLine($"Word XMAS appears {result} times");

var result2 = WordSearch.CountWordX(content, "MAS");
Console.WriteLine($"Word MAS appears in an X (X-MAS) {result2} times");