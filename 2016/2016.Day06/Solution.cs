namespace _2016.Day06;

using Shared;

public class Solution
{
    private readonly IEnumerable<string> transposed;

    public Solution(IEnumerable<string> input)
    {
        this.transposed = input.Transpose();
    }

    public object PartOne()
    {
        var result = string.Join("", this.transposed.Select(x =>
            x.GroupBy(it => it)
                .MaxBy(it => it.Count())!
                .Key));

        return result;
    }

    public object PartTwo()
    {
        var result = string.Join("", this.transposed.Select(x =>
            x.GroupBy(it => it)
                .MinBy(it => it.Count())!
                .Key));

        return result;
    }
}