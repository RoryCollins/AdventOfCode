namespace _2024.Day11;

public class Solution
{
    private const bool UseTestInput = false;

    private readonly IEnumerable<long> stones;
    private readonly Dictionary<long, IEnumerable<long>> oneSplitDictionary = new();

    public Solution(string testInput, string input)
    {
        this.stones = (UseTestInput ? testInput : input)
            .Split(" ")
            .Select(long.Parse);
    }

    public object PartOne()
    {
        var final = Enumerable.Range(1, 25)
            .Aggregate(this.stones, (acc, _) => acc.SelectMany(this.BlinkSingleStone))
            .ToList();
        return final.Count;
    }

    public object PartTwo()
    {
        var dictionary = this.stones.Select(it => (it, 1L))
            .ToDictionary();

        var results = Enumerable.Range(1, 75)
            .Aggregate(dictionary, (acc, _) => this.Blink(acc));

        return results.Sum(it => it.Value);
    }

    private IEnumerable<long> BlinkSingleStone(long stone)
    {
        if (this.oneSplitDictionary.TryGetValue(stone, out var next)) return next;

        List<long> afterBlink;
        if (stone == 0)
        {
            afterBlink = [1];
        }
        else
        {
            var stoneString = stone.ToString();

            if (stoneString.Length % 2 == 1)
            {
                afterBlink = [stone * 2024];
            }
            else
            {
                var half = stoneString.Length / 2;
                afterBlink = [long.Parse(stoneString[..half]), long.Parse(stoneString[half..])];
            }
        }

        this.oneSplitDictionary.Add(stone, afterBlink);
        return afterBlink;
    }

    private Dictionary<long, long> Blink(Dictionary<long, long> stones)
    {
        return stones
            .SelectMany(stoneCount => this.BlinkSingleStone(stoneCount.Key)
                .Select(n => (nextStone: n, count: stoneCount.Value)))
            .GroupBy(it => it.nextStone)
            .Select(it => (it.Key, it.Sum(g => g.count)))
            .ToDictionary(d => d.Key, d => d.Item2);
    }
}