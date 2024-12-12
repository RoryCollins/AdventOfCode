namespace _2024.Day06;

using Shared;

public class Solution
{
    private readonly Grid<char> grid;
    private readonly IEnumerable<Coordinate2D> obstacles;
    private readonly Coordinate2D origin;
    private readonly HashSet<Coordinate2D> visitedCoordinates;

    public Solution(IEnumerable<string> input)
    {
        this.grid = new Grid<char>(input.ToList());
        this.obstacles = this.grid.FindAll('#').ToList();
        this.origin = this.grid.FindAll('^').Single();
        this.visitedCoordinates = new HashSet<Coordinate2D>();

        var current = new DirectionalCoordinate2D(this.origin, Direction.North);
        while (true)
        {
            this.visitedCoordinates.Add(current.Coordinate);

            var candidate = current.Move();

            if (!this.grid.IsOnGrid(candidate.Coordinate)) break;
            if (this.obstacles.Contains(candidate.Coordinate))
            {
                current = current with { Direction = TurnRight(current.Direction) };
            }
            else
            {
                current = candidate;
            }
        }
    }

    public object PartOne()
    {
        return this.visitedCoordinates.Count;
    }

    private static Direction TurnRight(Direction direction)
    {
        return direction switch
        {
            Direction.North => Direction.East,
            Direction.West => Direction.North,
            Direction.South => Direction.West,
            Direction.East => Direction.South,
            _ => throw new ProgrammerException($"direction is {direction}")
        };
    }

    public object PartTwo()
    {
        var newObstructionCounter = 0;
        foreach (var newObstruction in this.visitedCoordinates)
        {
            var visited = new HashSet<DirectionalCoordinate2D>();
            var obstructions = this.obstacles.ToList();
            obstructions.Add(newObstruction);

            var current = new DirectionalCoordinate2D(this.origin, Direction.North);
            while (true)
            {
                if (!visited.Add(current))
                {
                    newObstructionCounter++;
                    break;
                }

                var candidate = current.Move();

                if (!this.grid.IsOnGrid(candidate.Coordinate)) break;

                if (obstructions.Contains(candidate.Coordinate))
                {
                    current = current with { Direction = TurnRight(current.Direction) };
                }
                else
                {
                    current = candidate;
                }
            }
        }

        return newObstructionCounter;

    }
}