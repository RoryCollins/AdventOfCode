namespace _2024.Day02;

using Shared;

public class Solution
{
    private enum Direction
    {
        Ascending,
        Descending
    }

    private readonly IEnumerable<List<int>> records;

    public Solution(IEnumerable<string> input)
    {
        this.records = input.Select(line => line.Split(" ").Select(int.Parse).ToList());
    }

    public object PartOne()
    {
        return this.records.Count(IsSafe);
    }

    public object PartTwo()
    {
        var absolutelyDisgustingWayOfDoingThisWithACounter = 0;
        foreach (var record in this.records)
        {
            if (IsSafe(record))
            {
                absolutelyDisgustingWayOfDoingThisWithACounter++;
                continue;
            }

            for (var i = 0; i < record.Count; i++)
            {
                var check = record.ToList();
                check.RemoveAt(i);
                if (IsSafe(check))
                {
                    absolutelyDisgustingWayOfDoingThisWithACounter++;
                    break;
                }
            }
        }

        return absolutelyDisgustingWayOfDoingThisWithACounter;
    }

    private static bool IsSafe(IEnumerable<int> record)
    {
        var windows = record.Windowed(2).ToList();
        var direction = windows[0][1] > windows[0][0]
            ? Direction.Ascending
            : Direction.Descending;
        return windows.All(window => IsValid(window, direction));
    }

    private static bool IsValid(int[] window, Direction direction)
    {
        if (Math.Abs(window.Last() - window.First()) > 3) return false;
        return direction == Direction.Ascending
            ? window.Last() > window.First()
            : window.Last() < window.First();
    }

}