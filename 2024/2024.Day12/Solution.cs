namespace _2024.Day12;

using Shared;

public class Solution
{
    private const bool UseTestInput = false;

    private readonly Grid<char> grid;
    private readonly HashSet<Coordinate2D> visited = [];

    public Solution(IEnumerable<string> testInput, IEnumerable<string> input)
    {
        this.grid = new Grid<char>(UseTestInput ? testInput : input);
    }

    public object PartOne()
    {
        var cost = 0;
        for (int y = 0; y < this.grid.Height; y++)
        {
            for (int x = 0; x < this.grid.Width; x++)
            {
                var current = new Coordinate2D(x,y);
                if (this.visited.Contains(current)) continue;

                var fields = this.CostByPerimeter(current);
                foreach (var c in fields.cs)
                {
                    this.visited.Add(c);
                }
                cost += fields.cost;
            }
        }

        return cost;
    }

    public object PartTwo()
    {
        return "Not yet implemented";
    }

    private (HashSet<Coordinate2D> cs, int cost) CostByPerimeter(Coordinate2D f)
    {
        var produce = this.grid.At(f);
        var field = new HashSet<Coordinate2D> {f};
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
}