namespace _2016.Day02;

using System.Text;
using Shared;

public class Solution
{
    private readonly IEnumerable<List<Direction>> input;

    public Solution(IEnumerable<string> input)
    {
        this.input = input.Select(line =>
                line.Select(c =>
                        c switch
                        {
                            'U' => Direction.North,
                            'L' => Direction.West,
                            'R' => Direction.East,
                            'D' => Direction.South,
                            _ => throw new ProgrammerException()
                        })
                    .ToList()
            )
            .ToList();
    }

    public object PartOne()
    {
        var keypad = new Grid(["123", "456", "789"]);
        return this.Solve(keypad);
    }

    public object PartTwo()
    {
        var keypad = new Grid(["  1  ", " 234 ", "56789", " ABC ", "  D  "]);
        return this.Solve(keypad);
    }

    private string Solve(Grid keypad)
    {
        var current = new Coordinate2D(1, 1);
        Coordinate2D proposal;
        var sb = new StringBuilder();
        foreach (var path in this.input)
        {
            foreach (var direction in path)
            {
                proposal = current.Move(direction);
                if (keypad.IsOnGrid(proposal) && keypad.At(proposal) != ' ')
                {
                    current = proposal;
                }
            }

            sb.Append(keypad.At(current));
        }
        return sb.ToString();
    }
}