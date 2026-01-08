namespace _2025.Day12;

using Shared;

internal class Region
{
    public int Area;
    public Dictionary<int, int> BoxCounts;

    public Region(string description)
    {
        var parts = description.Split(':');
        var areaParts = parts[0].Split('x');
        var _width = int.Parse(areaParts[0]);
        var _height = int.Parse(areaParts[1]);
        Area = _height * _width;

        BoxCounts = parts[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .Select((n, i) => (box: i, count: n))
            .ToDictionary();
    }
}

internal class Box
{
    private readonly List<Coordinate2D> _boxParts = [];
    public int Area => _boxParts.Count;
    public Box(string[] description)
    {
        var maxX = description[1].Length;
        for (int y = 1; y < description.Length; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                if(description[y][x] == '#') _boxParts.Add(new Coordinate2D(x, y));
            }
        }
    }
}

public class Solution
{
    private readonly IEnumerable<Region> _regions;
    private Dictionary<int, Box> _boxes;
    private const bool UseTestInput = false;

    public Solution(string testInput, string input)
    {
        var text = UseTestInput ? testInput : input;
        var sections = text.Split(["\r\n\r\n", "\n\n"], StringSplitOptions.None);

        _boxes = sections[..^1]
            .Select(section => section.Split(["\r\n", "\n"], StringSplitOptions.None))
            .Select(b => new Box(b))
            .Select((b, i) => (i, b))
            .ToDictionary();

        _regions = sections[^1].Split(["\r\n", "\n"], StringSplitOptions.None)
            .Select(r => new Region(r));
    }

    public object PartOne()
    {
        return _regions.Count(r =>
        {
            var sum = r.BoxCounts.Sum(bc =>
            {
                var boxArea = _boxes[bc.Key].Area;
                return boxArea * bc.Value;
            });
            return r.Area >= sum;
        });
    }

    public object PartTwo()
    {
        return "Not yet implemented";
    }
}