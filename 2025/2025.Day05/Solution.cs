using System.Text.RegularExpressions;

namespace _2025.Day05;

public partial class Solution
{
    private record FreshRange(long Start, long End)
    {
        public bool Contains(long ingredient)
        {
            return ingredient >= Start && ingredient <= End;
        }

        public bool CanCombine(FreshRange other)
        {
            return (other.Start >= Start && other.Start <= End) ||  (other.End >= Start && other.End <= End);
        }

        public FreshRange Combine(FreshRange other)
        {
            return new FreshRange(new[] { this.Start, other.Start }.Min(), new[] { this.End, other.End }.Max());
        }
        public long CountIngredients()
        {
            return (End - Start) + 1;
        }
    }
    
    private const bool UseTestInput = false;

    private readonly IEnumerable<long> _ingredients;
    private readonly List<FreshRange> _freshRanges;

    public Solution(string testInput, string input)
    {
        var _input = UseTestInput ? testInput : input;
        var foo = _input.Split(["\r\n\r\n", "\n\n"], StringSplitOptions.None);
        this._freshRanges = foo.First().Split("\n").Select(line =>
        {
            var r = MyRegex().Match(line);
            var range = new FreshRange(long.Parse(r.Groups[1].Value), long.Parse(r.Groups[2].Value));
            return range;
        }).ToList();
        this._ingredients = foo.Last().Split("\n").Select(long.Parse);
    }

    public object PartOne()
    {
        var freshIngredientCount = 0;
        foreach (var ingredient in this._ingredients)
        {
            foreach (var freshRange in this._freshRanges)
            {
                if (freshRange.Contains(ingredient))
                {
                    freshIngredientCount++;
                    break;
                }
            }
        }
        return freshIngredientCount;
    }

    public object PartTwo()
    {
        var sortedRanges = this._freshRanges.OrderBy(x => x.Start).ToList();
        for (int i = 0; i < sortedRanges.Count() - 1; i++)
        {
            var current = sortedRanges[i];
            var next = sortedRanges[i + 1];

            if (!current.CanCombine(next))
            {
                continue;
            }
            
            sortedRanges.Remove(current);
            sortedRanges.Remove(next);
            sortedRanges.Insert(i, current.Combine(next));
            i = 0;
        }
        
        return sortedRanges.Sum(i => i.CountIngredients());
    }

    [GeneratedRegex(@"(\d+)-(\d+)")]
    private static partial Regex MyRegex();
}