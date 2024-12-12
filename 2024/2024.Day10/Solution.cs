namespace _2024.Day10;

using Shared;

public class Solution
{
    private const bool UseTestInput = false;

    private readonly List<string> input;
    private HashSet<(Coordinate2D peak, Coordinate2D trailHead)> heads;
    private Grid<int> grid;

    public Solution(IEnumerable<string> testInput, IEnumerable<string> input)
    {
        this.input = (UseTestInput ? testInput : input).ToList();
        this.grid = new Grid<int>(this.input.Select(line => line.Select(c => int.Parse(c.ToString()))));
        this.heads = new HashSet<(Coordinate2D peak, Coordinate2D trailHead)>();
    }

    public object PartOne()
    {
        var peaks = this.grid.FindAll(9);
        foreach (var peak in peaks)
        {
            foreach (var trailhead in FindTrailheads(peak))
            {
                this.heads.Add((peak, trailhead));
            }
        }

        return this.heads.Count;

    }

    public object PartTwo()
    {
        var peaks = this.grid.FindAll(9);

        return peaks.Sum(it => FindTrailheads(it)
            .Count());

    }

    private IEnumerable<Coordinate2D> FindTrailheads(Coordinate2D peak)
    {
        if (this.grid.At(peak) == 0)
        {
            return new[] { peak };
        }

        return this.grid
            .GetNeighbours(peak, false)
            .Where(it => this.grid.At(it) == this.grid.At(peak)-1)
            .SelectMany(FindTrailheads);
    }
}