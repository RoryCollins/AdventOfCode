namespace App1;

public class Solution
{
    private const bool UseTestInput = false;

    private readonly IEnumerable<string> input;

    public Solution(IEnumerable<string> testInput, IEnumerable<string> input)
    {
        this.input = UseTestInput ? testInput : input;
    }

    public object PartOne()
    {
        return "Not yet implemented";
    }

    public object PartTwo()
    {
        return "Not yet implemented";
    }
}