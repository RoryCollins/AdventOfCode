using System.Text;
using System.Text.RegularExpressions;

namespace _2025.Day10;

public partial class Solution
{
    private record Machine(int Target, IEnumerable<int> Buttons)
    {
        public override string ToString()
        {
            return $"{Convert.ToString(Target, 2)} | {string.Join(" ~ ", Buttons.Select(b => Convert.ToString(b, 2)))}";
        }

        public int ButtonPressesToTarget()
        {
            List<int> current = [0];
            var buttonPresses = 0;
            while (!current.Contains(Target))
            {
                buttonPresses++;
                current = current.SelectMany(this.OneButtonPress).ToList();
            }
            return buttonPresses;
        }

        public IEnumerable<int> OneButtonPress(int current)
        {
            return this.Buttons.Select(button => current ^ button);
        }

    }


    private const bool UseTestInput = false;

    private readonly IEnumerable<Machine> machines;

    public Solution(IEnumerable<string> testInput, IEnumerable<string> input)
    {
        this.machines = (UseTestInput ? testInput : input).Select(line =>
            {
                var value = TargetDiagramRegex().Match(line).Groups[1].Value;
                var targetDiagram = value
                    .Replace('.', '0')
                    .Replace('#', '1');
                var targetAsNumber = Convert.ToInt32(targetDiagram, 2);

                var buttons = ButtonRegex().Matches(line)
                    .Select(match => match.Groups[1].Value)
                    .Select(s => s.Split(',').Select(int.Parse).ToArray())
                    .Select(b =>
                    {
                        var sb = new StringBuilder();
                        for (int i = 0; i < targetDiagram.Length; i++)
                        {
                            sb.Append(b.Contains(i) ? '1' : '0');
                        }
                        return sb.ToString();
                    })
                    .Select(s => Convert.ToInt32(s, 2));

                return new Machine(targetAsNumber, buttons);
            }
        );
    }

    public object PartOne()
    {
        return this.machines.Select(m => m.ButtonPressesToTarget()).Sum();
    }

    public object PartTwo()
    {
        return "Not yet implemented";
    }

    [GeneratedRegex(@"\[(.*)\]")]
    private static partial Regex TargetDiagramRegex();
    [GeneratedRegex(@"\((.*?)\)")]
    private static partial Regex ButtonRegex();
}