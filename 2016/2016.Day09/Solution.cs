namespace _2016.Day09;

using System.Text.RegularExpressions;
using Shared;
using static Shared.Part;

public partial class Solution
{
    private readonly IEnumerable<string> input;

    public Solution(IEnumerable<string> input)
    {
        this.input = input;
    }

    public object PartOne()
    {
        return this.input.Sum(it => Process(it, One));
    }

    public object PartTwo()
    {
        return this.input.Sum(it => Process(it, Two));
    }

    private static long Process(string s, Part part)
    {
        var preambleLength = s.IndexOf('(');
        switch (preambleLength)
        {
            case -1:
                return s.Length;
            case > 0:
                return preambleLength + Process(s[preambleLength..], part);
        }

        var f = MyRegex().Match(s);
        var offset = s.IndexOf(')')+1;
        var selector = int.Parse(f.Groups[1].Value);
        var repetition = int.Parse(f.Groups[2].Value);

        var processedRepetition = part == Two
            ? Process(s[offset..(offset + selector)], part)
            : selector;
        return (processedRepetition * repetition) + Process(s[(offset+selector)..], part);
    }

    [GeneratedRegex(@"^\((\d+)x(\d+)\).*")]
    private static partial Regex MyRegex();
}