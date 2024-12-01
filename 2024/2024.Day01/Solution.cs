namespace _2024.Day01;

using System.Text.RegularExpressions;

public partial class Solution
{
    private readonly IEnumerable<int> leftList;
    private readonly IEnumerable<int> rightList;

    public Solution(IEnumerable<string> input)
    {
        var rx = InputRegex();
        var parsed = input.Select(line =>
        {
           var gs = rx.Match(line).Groups;
           return (int.Parse(gs[1].Value), int.Parse(gs[2].Value));
        }).ToList();
        this.leftList = parsed.Select(it => it.Item1);
        this.rightList = parsed.Select(it => it.Item2);
    }

    public object PartOne()
    {
        return this.leftList.OrderBy(i => i)
            .Zip((this.rightList.OrderBy(i => i)))
            .Sum(it => Math.Abs(it.Second - it.First));
    }

    public object PartTwo()
    {
        return this.leftList.Sum(source => source * this.rightList.Count(it => it == source));
    }

    [GeneratedRegex(@"(\d+)\s+(\d+)")]
    private static partial Regex InputRegex();
}