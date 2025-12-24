namespace _2025.Day06;

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
        var operations = this.input.Last().Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var lines = new List<List<long>>();
        var results = new List<long>();
        foreach (var line in this.input.ToList()[..^1])
        {
            lines.Add(line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList());
        }

        for (int i = 0; i < operations.Length; i++)
        {
            var operands = lines.Select(l => l[i]);

            results.Add(operations[i] == "+" ? operands.Sum() : operands.Aggregate(1L, (a, x) => a * x));
        }

        return results.Sum();
    }

    public object PartTwo()
    {
        var operations = this.input.Last().Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var results = new List<long>();
        var operationIndex = 0;
        var operands = new List<long>();
        for (int i = 0; i < this.input.First().Length; i++)
        {
            var lines = this.input.ToList()[..^1];
            if (lines.All(l => l[i] == ' '))
            {
                results.Add(operations[operationIndex] == "+" ? operands.Sum() : operands.Aggregate(1L, (a, x) => a * x));
                operands = [];
                operationIndex++;
                continue;
            }
            operands.Add(long.Parse(string.Join("", lines.Select(l => l[i]))));
        }
        results.Add(operations[operationIndex] == "+" ? operands.Sum() : operands.Aggregate(1L, (a, x) => a * x));

        return results.Sum();
    }
}