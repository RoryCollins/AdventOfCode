using System.Text;
using System.Text.RegularExpressions;

namespace _2025.Day10;

public partial class Solution
{
    private const bool USE_TEST_INPUT = false;

    private readonly IEnumerable<Machine> machines;

    public Solution(IEnumerable<string> testInput, IEnumerable<string> input)
    {
        this.machines = (USE_TEST_INPUT ? testInput : input).Select(line =>
            {
                var targetLightDiagram = TargetDiagramRegex().Match(line).Groups[1].Value;

                var buttons = ButtonRegex().Matches(line)
                    .Select(match => match.Groups[1].Value)
                    .Select(s => s.Split(',').Select(int.Parse).ToArray())
                    .Select(b => new Button(b, targetLightDiagram.Length));

                var joltages = JoltageRegex().Match(line).Groups[1].Value
                    .Split(',')
                    .Select(int.Parse)
                    .ToArray();

                return new Machine(targetLightDiagram, buttons, joltages);
            }
        );
    }

    public object PartOne()
    {
        return this.machines.Select(m => m.ButtonPressesToReachLightTarget()).Sum();
    }

    public object PartTwo()
    {
        return this.machines.Select(m => m.ButtonPressesToReachJoltageTarget()).Sum();
    }

    [GeneratedRegex(@"\[(.*)\]")]
    private static partial Regex TargetDiagramRegex();

    [GeneratedRegex(@"\((.*?)\)")]
    private static partial Regex ButtonRegex();

    [GeneratedRegex(@"\{(.*?)\}")]
    private static partial Regex JoltageRegex();
}