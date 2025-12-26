using Shared;

namespace _2025.Day07;

public class Solution
{
    private const bool UseTestInput =  false;

    private readonly IEnumerable<Coordinate2D> splitters;
    private int splittersHit = 0;
    private readonly Grid<char> grid;
    private readonly Coordinate2D startPosition;
    private Dictionary<Coordinate2D, long> splitterTimelineCount = [];

    public Solution(IEnumerable<string> testInput, IEnumerable<string> input)
    {
        this.grid = new Grid<char>(UseTestInput ? testInput : input);
        this.startPosition = this.grid.FindAll('S').Single();
        this.splitters = this.grid.FindAll('^');
    }

    public object PartOne()
    {
        HashSet<Coordinate2D> current = [startPosition];
        while (current.First().Y <= grid.Height)
        {
            current = Progress(current);
        }

        return this.splittersHit;
    }
    
    public object PartTwo()
    {
        return ProgressRecursion(this.startPosition);
    }

    private HashSet<Coordinate2D> Progress(HashSet<Coordinate2D> current)
    {
        return current.SelectMany<Coordinate2D, Coordinate2D>(c =>
        {
            var down = new Coordinate2D(0, 1);
            var candidate = c.Add(down);
            if (this.splitters.Contains(candidate))
            {
                this.splittersHit++;
                return [candidate.Move(Direction.East), candidate.Move(Direction.West)];
            }
            return [candidate];
        }).ToHashSet();
    }

    
    private long ProgressRecursion(Coordinate2D current)
    {
        if (current.Y == this.grid.Height) return 1L;
        var nextPositions = Progress([current]);
        var currentCount = nextPositions.Sum(p => this.splitterTimelineCount.TryGetValue(p, out var count) ? count : ProgressRecursion(p));
        this.splitterTimelineCount.Add(current, currentCount);
        return currentCount;
    }
}