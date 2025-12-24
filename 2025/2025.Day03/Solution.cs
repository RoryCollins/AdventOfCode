using System.Text;

namespace _2025.Day03;

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
        List<int> maxJoltages = [];
        foreach (var line in input)
        {
            var prefix = line[..^1].Select(c=> int.Parse(c.ToString())).ToArray().Max();
            var suffix = line[(line.IndexOf($"{prefix}", StringComparison.Ordinal)+1)..].Select(c => int.Parse(c.ToString())).ToArray().Max();

            maxJoltages.Add(int.Parse($"{prefix}{suffix}"));
        }
        return maxJoltages.Sum();
    }

    public object PartTwo()
    {
        List<long> maxJoltages = [];
        foreach (var line in input)
        {
            var sb = new StringBuilder();
            var index = 0;
            for (int i = 12; i > 0; i--)
            {
                var range = line[index..^(i-1)];
                var value = range.Select(c => int.Parse(c.ToString())).ToArray().Max();
                sb.Append(value);
                var offset = range.IndexOf(value.ToString(), StringComparison.Ordinal);
                index += offset+1;
            }
            Console.WriteLine(sb.ToString());

            maxJoltages.Add(long.Parse(sb.ToString()));
        }
        return maxJoltages.Sum();
    }
}