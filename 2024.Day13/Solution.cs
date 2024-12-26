using System.Text.RegularExpressions;
using Shared;

namespace _2024.Day13;

record ClawMachine((int, int) ButtonA, (int, int) ButtonB, (long, long) Prize)
{
    public long TokenCost()
    {
        var A1 = ButtonA.Item1;
        var A2 = ButtonA.Item2;
        var B1 = ButtonB.Item1;
        var B2 = ButtonB.Item2;
        var C1 = Prize.Item1;
        var C2 = Prize.Item2;

        double det = A1 * B2 - A2 * B1;
        if (det == 0)
        {
            return 0;
        } else
        {
            var x = (B2 * C1 - B1 * C2) / det;
            var y = (A1 * C2 - A2 * C1) / det;
            if (x % 1 != 0 || y % 1 != 0)
            {
                return 0;
            }

            // if (x > 100 || y > 100)
            // {
            //     return 0;
            // }
            
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
        const long offset = 10000000000000;

        machines = this.input.Select(line =>
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

    public object PartOne()
    {
        return machines.Sum(machine => machine.TokenCost());
    }

    public object PartTwo()
    {
        return "Not yet implemented";
    }

    [GeneratedRegex(@"Button A: X\+(\d+), Y\+(\d+)")]
    private static partial Regex ButtonARegex();
    
    [GeneratedRegex(@"Button B: X\+(\d+), Y\+(\d+)")]
    private static partial Regex ButtonBRegex();
    
    [GeneratedRegex(@"Prize: X=(\d+), Y=(\d+)")]
    private static partial Regex PrizeRegex();
}