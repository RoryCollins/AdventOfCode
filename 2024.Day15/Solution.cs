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
    private IEnumerable<Coordinate2D> instructions;
    private IEnumerable<(Coordinate2D, Coordinate2D)> newBoulders;
    private IEnumerable<Coordinate2D> newWalls;

    public Solution(string testInput, string input)
    {
        var parts = (UseTestInput ? testInput : input).Split("\n\n");
        grid = new Grid<char>(parts[0].Split("\n"));

        instructions = parts[1].ReplaceLineEndings("").Select(c =>
        {
            return c switch
            {
                'v' => new Coordinate2D(0, 1),
                '>' => new Coordinate2D(1, 0),
                '<' => new Coordinate2D(-1, 0),
                '^' => new Coordinate2D(0, -1),
                _ => throw new ArgumentException($"Invalid direction: {c}")
            };
        });

        boulders = grid.FindAll('O').ToList();
        walls = grid.FindAll('#');
        start = grid.FindAll('@').Single();
    }

    public object PartOne()
    {
        var state = instructions.Aggregate(new GridStatePartOne(start, boulders, walls),
            (c, instruction) => ProcessInstructionPartOne(instruction, c));

        return state.Boulders.Sum(it => it.Y * 100 + it.X);
    }

    public object PartTwo()
    {
        newBoulders = boulders.Select(c => (c with { X = c.X * 2 }, c with { X = c.X * 2 + 1 }));
        newWalls = walls.SelectMany(c => new[] { c with { X = c.X * 2 }, c with { X = c.X * 2 + 1 } });
        var newStart = start with { X = start.X * 2 };

        var state = instructions.Aggregate(new GridStatePartTwo(newStart, newBoulders, newWalls),
            (c, instruction) => ProcessInstructionPartTwo(instruction, c));

        return state.Boulders.Sum(it => it.Left.Y * 100 + it.Left.X);
    }

    private record GridStatePartOne(
        Coordinate2D Robot,
        IEnumerable<Coordinate2D> Boulders,
        IEnumerable<Coordinate2D> Walls);

    private static GridStatePartOne ProcessInstructionPartOne(Coordinate2D direction, GridStatePartOne state)
    {
        var proposal = state.Robot.Add(direction);
        var walls = state.Walls.ToList();
        var boulders = state.Boulders.ToList();

        if (walls.Contains(proposal))
        {
            return state;
        }

        if (boulders.Contains(proposal))
        {
            var boulderTarget = proposal.Add(direction);
            while (boulders.Contains(boulderTarget))
            {
                boulderTarget = boulderTarget.Add(direction);
            }

            if (walls.Contains(boulderTarget))
            {
                return state;
            }

            boulders.Remove(proposal);
            boulders.Add(boulderTarget);
        }

        return state with { Boulders = boulders, Robot = proposal };
    }

    private record GridStatePartTwo(
        Coordinate2D Robot,
        IEnumerable<(Coordinate2D Left, Coordinate2D Right)> Boulders,
        IEnumerable<Coordinate2D> Walls);

    private static GridStatePartTwo ProcessInstructionPartTwo(Coordinate2D direction, GridStatePartTwo state)
    {
        var proposal = state.Robot.Add(direction);
        var walls = state.Walls.ToList();

        if (walls.Contains(proposal))
        {
            return state;
        }

        var boulders = state.Boulders.ToList();
        var bouldersToMove = new HashSet<(Coordinate2D, Coordinate2D)>();

        var queue = new Queue<Coordinate2D>();

        var boulderEncountered = boulders.FirstOrDefault(b => new[] { b.Left, b.Right }.Contains(proposal));
        if (boulderEncountered != default)
        {
            bouldersToMove.Add(boulderEncountered);
            queue.Enqueue(boulderEncountered.Left);
            queue.Enqueue(boulderEncountered.Right);
        }

        while (queue.Count > 0)
        {
            var p = queue.Dequeue();
            var target = p.Add(direction);
            if (walls.Contains(target))
            {
                return state;
            }

            var found = boulders.FirstOrDefault(b => new[] { b.Left, b.Right }.Contains(target));
            if (found != default)
            {
                var added = bouldersToMove.Add(found);
                if (added)
                {
                    queue.Enqueue(found.Left);
                    queue.Enqueue(found.Right);
                }
            }
        }


        foreach (var boulder in bouldersToMove)
        {
            boulders.Remove(boulder);
            boulders.Add((boulder.Item1.Add(direction), boulder.Item2.Add(direction)));
        }

        return state with { Boulders = boulders, Robot = proposal };
    }

    private static void PrintGrid(GridStatePartOne state)
    {
        for (int y = 0; y <= state.Walls.Max(it => it.Y); y++)
        {
            for (int x = 0; x <= state.Walls.Max(it => it.X); x++)
            {
                var cell = new Coordinate2D(x, y);
                var symbol = cell == state.Robot
                    ? '@'
                    : state.Walls.Contains(cell)
                        ? '#'
                        : state.Boulders.Contains(cell)
                            ? 'O'
                            : '.';
                Console.Write(symbol);
            }

            Console.WriteLine();
        }
    }

    private static void PrintGrid(GridStatePartTwo state)
    {
        for (int y = 0; y <= state.Walls.Max(it => it.Y); y++)
        {
            for (int x = 0; x <= state.Walls.Max(it => it.X); x++)
            {
                var cell = new Coordinate2D(x, y);
                var symbol = cell == state.Robot
                    ? '@'
                    : state.Walls.Contains(cell)
                        ? '#'
                        : state.Boulders.Select(it => it.Item1).Contains(cell)
                            ? '['
                            : state.Boulders.Select(it => it.Item2).Contains(cell)
                                ? ']'
                                : '.';
                Console.Write(symbol);
            }

            Console.WriteLine();
        }
    }
}