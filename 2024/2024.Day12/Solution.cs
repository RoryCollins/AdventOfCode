using Shared;

namespace _2024.Day12;

public class Solution
{
    private const bool UseTestInput = false;

    private readonly Grid<char> grid;
    private List<(HashSet<Coordinate2D> cs, int cost)> fields = [];

    public Solution(IEnumerable<string> testInput, IEnumerable<string> input)
    {
        this.grid = new Grid<char>(UseTestInput ? testInput : input);
        GetPerimeterCounts();
    }

    private void GetPerimeterCounts()
    {
        HashSet<Coordinate2D> visited = [];
        for (int y = 0; y < this.grid.Height; y++)
        {
            for (int x = 0; x < this.grid.Width; x++)
            {
                var current = new Coordinate2D(x, y);
                if (visited.Contains(current)) continue;

                var currentField = this.CostByPerimeter(current);
                this.fields.Add(currentField);
                foreach (var c in currentField.cs)
                {
                    visited.Add(c);
                }
            }
        }
    }

    public object PartOne()
    {
        return this.fields.Sum(field => field.cost);
    }

    public object PartTwo()
    {
        return this.fields.Sum(field => CostBySides(field.cs));
    }

    private (HashSet<Coordinate2D> cs, int cost) CostByPerimeter(Coordinate2D f)
    {
        var produce = this.grid.At(f);
        var field = new HashSet<Coordinate2D> { f };
        var queue = new Queue<Coordinate2D>();
        var perimeter = 0;
        queue.Enqueue(f);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var sameFieldNeighbours = this.grid.GetNeighbours(current, false)
                .Where(n => this.grid.At(n) == produce)
                .ToList();

            var neighbours = sameFieldNeighbours
                .Where(c => !field.Contains(c))
                .ToArray();

            perimeter += 4 - sameFieldNeighbours.Count;

            foreach (var neighbour in neighbours)
            {
                field.Add(neighbour);
                queue.Enqueue(neighbour);
            }
        }

        return (field, field.Count * perimeter);
    }

    private int CostBySides(HashSet<Coordinate2D> field)
    {
        var corners = new List<(Coordinate2D, Coordinate2D)>
        {
            (Coordinate2D.North, Coordinate2D.East),
            (Coordinate2D.East, Coordinate2D.South),
            (Coordinate2D.South, Coordinate2D.West),
            (Coordinate2D.West, Coordinate2D.North),
        };

        var numberOfCorners = field.Sum(location => corners.Count(corner =>
            (!field.Contains(location.Add(corner.Item1)) && !field.Contains(location.Add(corner.Item2))) ||
            (field.Contains(location.Add(corner.Item1)) && field.Contains(location.Add(corner.Item2)) && !field.Contains(location.Add(corner.Item2).Add(corner.Item1)))));
        
        return field.Count * numberOfCorners;
    }
}