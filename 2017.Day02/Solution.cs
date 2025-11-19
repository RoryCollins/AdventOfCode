namespace _2017.Day02;

using System.Text.RegularExpressions;

public class Solution
{
    private const bool UseTestInput = false;

    private readonly IEnumerable<string> _input;
    private readonly IEnumerable<List<int>> _lines;

    public Solution(IEnumerable<string> testInput, IEnumerable<string> input)
    {
        this._input = UseTestInput ? testInput : input;
        var regex = new Regex(@"\s+");
        _lines = _input.Select(line => regex.Split(line).Select(int.Parse)).Select(line => line.Order().ToList());
    }

    public object PartOne()
    {
        return _lines.Sum(it => it.Last() - it.First());
    }

    public object PartTwo()
    {
        return _lines.Sum(line =>
        {
            foreach (var i in line)
            {
                foreach (var j in line.Where(f => f <= i/2))
                {
                    if (i != j && i % j == 0) return i / j;
                }
            }

            throw new Exception("No divisible numbers found");
        });
    }
}