using Shared;

namespace _2024.Day16;

public class Solution
{
    private const bool UseTestInput = false;

    private readonly IEnumerable<string> input;
    private Coordinate2D[] walls;
    private Coordinate2D start;
    private Coordinate2D end;
    private Coordinate2D[] spaces;

    public Solution(IEnumerable<string> testInput, IEnumerable<string> input)
    {
        var grid = new Grid<char> (UseTestInput ? testInput : input);
        start = grid.FindAll('S').Single();
        end = grid.FindAll('E').Single();
        walls = grid.FindAll('#').ToArray();
        spaces = grid.FindAll('.').ToArray();
    }

    public object PartOne()
    {
        // var all = spaces.Select(it => (it, int.MaxValue)).ToDictionary(it => it.Item1, it => it.Item2);
        var all = new Dictionary<DirectionalCoordinate2D, int>();
        all.Add(new(start, Direction.East), 0);
        
        var queue = new PriorityQueue<DirectionalCoordinate2D, int>();
        queue.Enqueue(new DirectionalCoordinate2D(start, Direction.East), 0);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var ns = GetNeighbours(current);
            foreach (var (directionalCoordinate, cost) in ns)
            {
                
                var previousBestCost = all.TryGetValue(directionalCoordinate, out var foo) ? foo : int.MaxValue ;
                var currentCost = all[current] + cost;
                if (currentCost < previousBestCost)
                {
                    all[directionalCoordinate] = currentCost;
                    queue.Enqueue(directionalCoordinate, currentCost);
                }
            }
        }

        // for (int y = 0; y <= all.Keys.Max(it => it.Y); y++)
        // {
        //     for (int x = 0; x <= all.Keys.Max(it => it.X); x++)
        //     {
        //         if (all.TryGetValue(new Coordinate2D(x,y), out var result))
        //         {
        //             Console.Write(result%10);
        //         }
        //         else
        //         {
        //             Console.Write(' ');
        //         }
        //     }
        //
        //     Console.WriteLine();
        // }
        return new[] { Direction.North, Direction.East, Direction.South, Direction.West }.Min(it => all.TryGetValue(new(end, it), out var foo) ? foo : int.MaxValue);
    }

    public object PartTwo()
    {
        return "Not yet implemented";
    }

    private IEnumerable<(DirectionalCoordinate2D Coordinate, int Cost)> GetNeighbours(
        DirectionalCoordinate2D current)
    {
        var directionsWithCosts = new[]
        {
            (current.Direction.TurnLeft(), 1000),
            (current.Direction, 0),
            (current.Direction.TurnRight(), 1000)
        };

        var next = directionsWithCosts.Select(it => ((current with { Direction = it.Item1}).Move(), it.Item2+1)); ;

        return next.Where(it => !walls.Contains(it.Item1.Coordinate));
    }
}