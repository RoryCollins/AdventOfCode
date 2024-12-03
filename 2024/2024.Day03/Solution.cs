namespace _2024.Day03;

using System.Text.RegularExpressions;

public partial class Solution
{
    private readonly string memory;

    public Solution(IEnumerable<string> input)
    {
        this.memory = string.Join("", input);
    }

    public object PartOne()
    {
        return MulRegex()
            .Matches(this.memory)
            .Sum(match =>
                int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value)
            );
    }

    public object PartTwo()
    {
        return this.memory
            .Split("do()")
            .Select(GetValidSection)
            .Sum(section => MulRegex()
                .Matches(section)
                .Sum(match =>
                    int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value)
                ));
    }

    private static string GetValidSection(string section)
    {
        var stopIndex = section.IndexOf("don't()", StringComparison.Ordinal);
        var validSection = stopIndex > -1
            ? section[..stopIndex]
            : section;
        return validSection;
    }

    [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
    private static partial Regex MulRegex();
}