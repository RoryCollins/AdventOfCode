using System.Text.RegularExpressions;
using Shared;

namespace _2024.Day13;

record ClawMachine((int X, int Y) ButtonA, (int X, int Y) ButtonB, (long X, long Y) Prize)
{
    public long TokenCost(Part part)
    {
        var a1 = ButtonA.X;
        var a2 = ButtonA.Y;
        var b1 = ButtonB.X;
        var b2 = ButtonB.Y;
        var c1 = Prize.X;
        var c2 = Prize.Y;

        double det = a1 * b2 - a2 * b1;
        if (det == 0)
        {
            return 0;
        } else
        {
            var x = (b2 * c1 - b1 * c2) / det;
            var y = (a1 * c2 - a2 * c1) / det;
            if (x % 1 != 0 || y % 1 != 0)
            {
                return 0;
            }

            if ( (part == Part.One) && (x > 100 || y > 100))
            {
                return 0;
            }
            
            return (long)(3 * x + y);
        }
        
    }
}

public partial class Solution
{
    private const bool UseTestInput = false;

    private readonly IEnumerable<string> input;
    private IEnumerable<ClawMachine> machines;

    public Solution(string testInput, string input)
    {
        this.input = (UseTestInput ? testInput : input).Split("\n\n").ToList();
    }

    public object PartOne()
    {
        return InitialiseMachines(0).Sum(machine => machine.TokenCost(Part.One));
    }

    public object PartTwo()
    {
        return InitialiseMachines(10000000000000).Sum(machine => machine.TokenCost(Part.Two));
    }

    private IEnumerable<ClawMachine> InitialiseMachines(long offset)
    {
        return this.input.Select(line =>
        {
            var buttonA = ButtonARegex().Match(line);
            var buttonB = ButtonBRegex().Match(line);
            var prize = PrizeRegex().Match(line);
            return new ClawMachine(
                (int.Parse(buttonA.Groups[1].Value), int.Parse(buttonA.Groups[2].Value)),
                (int.Parse(buttonB.Groups[1].Value), int.Parse(buttonB.Groups[2].Value)),
                (int.Parse(prize.Groups[1].Value) + offset, int.Parse(prize.Groups[2].Value) + offset));
        });
    }

    [GeneratedRegex(@"Button A: X\+(\d+), Y\+(\d+)")]
    private static partial Regex ButtonARegex();
    
    [GeneratedRegex(@"Button B: X\+(\d+), Y\+(\d+)")]
    private static partial Regex ButtonBRegex();
    
    [GeneratedRegex(@"Prize: X=(\d+), Y=(\d+)")]
    private static partial Regex PrizeRegex();
}