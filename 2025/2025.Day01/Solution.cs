namespace _2025.Day01;

using System.Text.RegularExpressions;
using Shared;

public partial class Solution
{
    private const bool UseTestInput = false;

    private record Instruction(char Direction, int Clicks);

    private record State(int Position, int ZeroCounter)
    {
        public State Add(int clicks, Part part)
        {
            if (part == Part.One)
            {
                return AddPartOne(clicks);
            }

            else
            {
                return AddPartTwo(clicks);
            }
        }

        private State AddPartOne(int clicks)
        {
            var rawPosition = (this.Position + clicks);
            var newPosition = ((rawPosition % 100) + 100) % 100;
            var newCounter = ZeroCounter + (newPosition == 0 ? 1 : 0);

            return new State(newPosition, newCounter);
        }

        private State AddPartTwo(int clicks)
        {
            var count = Math.Abs(clicks) / 100 + ZeroCounter;
            var rawPosition = (this.Position + clicks % 100);

            var newPosition = ((rawPosition % 100) + 100) % 100;

            if (this.Position == 0)
            {
                return new State(newPosition, count);
            }

            if (newPosition == 0 || rawPosition is < 0 or > 100)
            {
                count++;
            }


            return new State(newPosition, count);
        }
    }

    private readonly IEnumerable<Instruction> input;

    public Solution(IEnumerable<string> testInput, IEnumerable<string> input)
    {
        this.input = (UseTestInput ? testInput : input).Select(line =>
        {
            var match = MyRegex().Match(line);
            var direction = match.Groups[1].Value[0];
            var count = int.Parse(match.Groups[2].Value);
            return new Instruction(direction, count);
        });
    }

    public object PartOne()
    {
        var state = new State(50, 0);
        foreach (var instruction in input)
        {
            var multiplier = instruction.Direction == 'L' ? -1 : 1;
            state = state.Add(instruction.Clicks * multiplier, Part.One);
        }
        return state.ZeroCounter;
    }

    public object PartTwo()
    {
        var state = new State(50, 0);
        foreach (var instruction in input)
        {
            var multiplier = instruction.Direction == 'L' ? -1 : 1;
            state = state.Add(instruction.Clicks * multiplier, Part.Two);
        }
        return state.ZeroCounter;
    }

    [GeneratedRegex(@"(\w)(\d*)")]
    private static partial Regex MyRegex();
}