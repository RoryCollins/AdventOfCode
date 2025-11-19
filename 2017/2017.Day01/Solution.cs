namespace _2017.Day01;

using Shared;

public class Solution
{
    private const bool UseTestInput = false;

    private readonly string _input;

    public Solution(string testInput, string input)
    {
        _input = UseTestInput ? testInput : input;
    }

    public object PartOne()
    {
        var partOneInput = _input + _input[0];
        var windows = partOneInput.Select(c => int.Parse(c.ToString())).Windowed(2);
        return windows.Where(i => i[0] == i[1]).Sum(i => i[0]);
    }

    public object PartTwo()
    {
        var length = _input.Length;
        var halfLength = length / 2;
        var windows = _input.Select(c => int.Parse(c.ToString())).Windowed(halfLength+1);
        return 2*windows.Where(i => i.First() == i.Last()).Sum(i => i[0]);
    }
}