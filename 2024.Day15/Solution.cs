using Shared;

namespace _2024.Day15;

public class Solution
{
    private const bool UseTestInput = false;

    private readonly string input;
    private Grid<char> grid;
    private Coordinate2D start;
    private List<Coordinate2D> boulders;
    private IEnumerable<Coordinate2D> walls;
    private IEnumerable<Direction> instructions;
    
    public Solution(string testInput, string input)
    {
        var parts = (UseTestInput ? testInput : input).Split("\n\n");
        grid = new Grid<char>(parts[0].Split("\n"));
        instructions = parts[1].ReplaceLineEndings("").Select(c =>
        {
            return c switch
            {
                'v' => Direction.North,
                '>' => Direction.East,
                '<' => Direction.West,
                '^' => Direction.South,
                _ => throw new ArgumentException($"Invalid direction: {c}")
            };
        });
        
        boulders = grid.FindAll('O').ToList();
        walls = grid.FindAll('#');
        start = grid.FindAll('@').Single();
    }

    public object PartOne()
    {
        var end = instructions.Aggregate(start, (c, instruction) => DoIt(instruction, c));
        return boulders.Sum(it => it.Y * 100 + it.X);
    }
    public object PartTwo()
    {
        return "Not yet implemented";
    }

    private Coordinate2D DoIt(Direction direction, Coordinate2D current)
    {
        var proposal = current.Move(direction);
        if (walls.Contains(proposal))
        {
            return current;
        }

        if (boulders.Contains(proposal))
        {
            var boulderTarget = proposal.Move(direction);
            while (boulders.Contains(boulderTarget))
            {
                boulderTarget = boulderTarget.Move(direction);
            }
            if (walls.Contains(boulderTarget))
            {
                return current;
            }
            boulders.Remove(proposal);
            boulders.Add(boulderTarget);
            return proposal;
        }
        return proposal;
    }


    private void PrintGrid(Coordinate2D end)
    {
        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                var current = new Coordinate2D(x, y);
                var symbol = current == end
                    ? '@'
                    : walls.Contains(current)
                        ? '#'
                        : boulders.Contains(current)
                            ? 'O'
                            : '.';
                Console.Write(symbol);
            }

            Console.WriteLine();
        }
    }
}