namespace _2024.Day07;

using Shared;

public class Solution
{
    private readonly IEnumerable<(long testValue, List<int> remainingNumbers)> input;

    public Solution(IEnumerable<string> input)
    {
        this.input = input.Select(line =>
        {
            var parts = line.Split(":");
            var testValue = long.Parse(parts[0]);
            var remainders = parts[1]
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToList();
            return (testValue, remainders);
        });
    }

    public object PartOne()
    {
        return this.input
            .Where(it =>
                IsValid(
                    it.testValue,
                    it.remainingNumbers.First(),
                    it.remainingNumbers[1..],
                    Part.One))
            .Sum(it => it.testValue);
    }

    public object PartTwo()
    {
        return this.input
            .Where(it =>
                IsValid(
                    it.testValue,
                    it.remainingNumbers.First(),
                    it.remainingNumbers[1..],
                    Part.Two))
            .Sum(it => it.testValue);
    }

    private static bool IsValid(long targetValue, long currentValue, List<int> remainders, Part part)
    {
        if (currentValue > targetValue) return false;

        if(remainders.Count == 0)
        {
            return targetValue == currentValue;
        }

        var nextValues = new List<long> { currentValue * remainders.First(), currentValue + remainders.First() };
        if (part == Part.Two)
        {
            nextValues.Add(long.Parse($"{currentValue}{remainders.First()}"));
        }

        return nextValues.Any(it => IsValid(targetValue, it, remainders[1..], part));
    }
}