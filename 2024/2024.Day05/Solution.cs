namespace _2024.Day05;

using System.ComponentModel.Design;

public class Solution
{
    private readonly Dictionary<int, List<int>> precedents;
    private readonly List<List<int>> updates;
    private readonly HashSet<int> allPages;
    // private readonly List<int> orderedPages;

    public Solution(string input)
    {
        this.precedents = new Dictionary<int, List<int>>();
        this.allPages = [];

        var inputParts = input.Split("\r\n\r\n").Select(it => it.Split("\r\n")).ToList();

        foreach (var line in inputParts[0])
        {
            var order = line
                .Split("|")
                .Select(int.Parse)
                .ToArray();

            this.allPages.Add(order[0]);
            this.allPages.Add(order[1]);

            if (this.precedents.ContainsKey(order[1]))
            {
                this.precedents[order[1]].Add(order[0]);
            }
            else
            {
                this.precedents.Add(order[1], [order[0]]);
            }
        }

        // this.orderedPages = this.allPages.Except(this.precedents.Keys).ToList();
        // while (this.orderedPages.Count < this.allPages.Count)
        // {
        //     var next = this.precedents
        //         .Where(it => !it.Value.Except(this.orderedPages).Any())
        //         .Select(it => it.Key)
        //         .Except(this.orderedPages);
        //     this.orderedPages.AddRange(next);
        // }
        //

        this.updates = inputParts[1]
            .Select(line => line
                .Split(",")
                .Select(int.Parse)
                .ToList())
            .ToList();
    }

    public object PartOne()
    {
        return this.updates.Where(this.Valid)
            .Sum(pages => pages[pages.Count / 2]);
    }

    private bool Valid(List<int> pages)
    {
        for(int i = 0; i < pages.Count; i++)
        {
            if (pages[i..].Any(it => this.precedents[pages[i]].Contains(it)))
            {
                return false;
            }
        }

        return true;
        // return pages.OrderBy(it => this.orderedPages.IndexOf(it))
        //     .ToList().SequenceEqual(pages);
    }

    public object PartTwo()
    {
        return this.updates
            .Where(it => !this.Valid(it))
            .Select(this.Fix)
            .Sum(pages => pages[pages.Count / 2]);
    }

    private List<int> Fix(List<int> pages)
    {
        var toDos = pages.ToList();
        var sorted = new List<int>();

        while (sorted.Count < pages.Count)
        {
            var x = toDos.Single(it => !toDos.Except([it]).Any(s => this.precedents[it].Contains(s)));
            toDos.Remove(x);
            sorted.Add(x);
        }

        return sorted;

    }
}