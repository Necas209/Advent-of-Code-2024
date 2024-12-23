namespace Day23;

public static class Challenge
{
    public static int Part1(ComputerNetwork network)
    {
        // Find the number of triangles with a computer starting with 't'
        return network.NumberOfTriangles('t');
    }

    public static string Part2(ComputerNetwork network)
    {
        // Find the largest clique
        var largestClique = network.LargestClique();

        // Sort the clique and generate the password
        var password = string.Join(",", largestClique);
        return password;
    }
}