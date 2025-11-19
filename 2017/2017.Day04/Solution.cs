namespace _2017.Day04;

public class Solution
{
    private const bool USE_TEST_INPUT = false;

    private readonly IEnumerable<string> input;

    public Solution(IEnumerable<string> testInput, IEnumerable<string> input)
    {
        this.input = USE_TEST_INPUT ? testInput : input;
    }

    public object PartOne()
    {
        return input.Sum(line =>
        {
            var words = line.Split(" ");
            return words.Length == words.Distinct().Count() ? 1 : 0;
        });
    }

    public object PartTwo()
    {
        return input.Sum(line =>
        {
            var words = line.Split(" ").Select(w => string.Join("", w.Order())).ToList();
            return words.Count == words.Distinct().Count() ? 1 : 0;
        });
    }
}