namespace _2016.Day01;

using System.Text.RegularExpressions;
using Shared;

internal enum Turn
{
    Left,
    Right
}

internal record Instruction(Turn Direction, int Distance);

public class Solution
{
    private readonly IEnumerable<Instruction> input;

    public Solution(IEnumerable<string> input)
    {
        var rx = new Regex(@"([LR])(\d+)");
        this.input = input.Select(s =>
        {
            var match = rx.Match(s);
            var turn = match.Groups[1].Value == "R" ? Day01.Turn.Right : Day01.Turn.Left;
            var distance = int.Parse(match.Groups[2].Value);
            return new Instruction(turn, distance);
        });
    }

    private static Direction Turn(Direction direction, Turn turn)
    {
        return turn switch
        {
            Day01.Turn.Left => direction switch
            {
                Direction.North => Direction.West,
                Direction.West => Direction.South,
                Direction.South => Direction.East,
                Direction.East => Direction.North,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            },
            Day01.Turn.Right => direction switch
            {
                Direction.North => Direction.East,
                Direction.West => Direction.North,
                Direction.South => Direction.West,
                Direction.East => Direction.South,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            },
            _ => throw new ArgumentOutOfRangeException(nameof(turn), turn, null)
        };
    }

    public object PartOne()
    {
        var o = DirectionalCoordinate2D.Origin;
        var final = this.input.Aggregate(o, (n, i) =>
        {
            var newDirection = Turn(n.Direction, i.Direction);
            var newCoordinate = n.Coordinate.Move(newDirection, i.Distance);
            return new DirectionalCoordinate2D(newCoordinate, newDirection);
        });
        return final.Coordinate.ManhattanDistanceTo(o.Coordinate);
    }

    public object PartTwo()
    {
        var result = this.FindCrossing();
        return result.ManhattanDistanceTo(Coordinate2D.Origin);
    }

    private Coordinate2D FindCrossing()
    {
        var current = DirectionalCoordinate2D.Origin;
        var history = new List<Line>();

        foreach (var instruction in this.input)
        {
            var nextDirection = Turn(current.Direction, instruction.Direction);
            var nextCoordinate = current.Coordinate.Move(nextDirection, instruction.Distance);
            var currentLine = new Line(current.Coordinate, nextCoordinate);
            foreach (var line in history)
            {
                if(currentLine.Crosses(line, out var crossingPoint)) return crossingPoint!;
            }

            history.Add(currentLine);
            current = new DirectionalCoordinate2D(nextCoordinate, nextDirection);
        }

        throw new ProgrammerException();
    }
}