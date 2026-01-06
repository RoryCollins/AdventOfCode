using System.Text;
using System.Text.RegularExpressions;

namespace _2025.Day10;

public partial class Solution
{
    private class Machine(string lightTarget, IEnumerable<int[]> buttons, int[] joltageTarget)
    {
        private readonly IEnumerable<int> buttonsForLights = buttons
            .Select(b =>
            {
                var sb = new StringBuilder();
                for (int i = 0; i < lightTarget.Length; i++)
                {
                    sb.Append(b.Contains(i) ? '1' : '0');
                }
                return sb.ToString();
            })
            .Select(s => Convert.ToInt32(s, 2));


        public int ButtonPressesToReachLightTarget()
        {
            var targetDiagram = lightTarget
                .Replace('.', '0')
                .Replace('#', '1');
            var targetAsNumber = Convert.ToInt32(targetDiagram, 2);

            List<int> current = [0];
            var buttonPresses = 0;
            while (!current.Contains(targetAsNumber))
            {
                buttonPresses++;
                current = current.SelectMany(this.PressButtonForLights).ToList();
            }
            return buttonPresses;
        }

        public int ButtonPressesToReachJoltageTarget()
        {
            var buttonPresses = 0;
            List<int[]> current = [new int[joltageTarget.Length]];
            while (!current.Any(j => Enumerable.SequenceEqual(j, joltageTarget)))
            {
                buttonPresses++;
                var intsList = current.SelectMany(this.PressButtonForJoltage).Where(x => x.Length > 0).ToList();
                var foo = intsList.DistinctBy(x => string.Join("", x)).ToList();
                current = foo;
            }
            return buttonPresses;
        }

        private IEnumerable<int> PressButtonForLights(int current)
        {
            return this.buttonsForLights.Select(button => current ^ button);
        }

        private IEnumerable<int[]> PressButtonForJoltage(int[] current)
        {
            return buttons.Select(elementsToIncrement =>
            {
                var candidate = current.ToArray();
                foreach (var i in elementsToIncrement)
                {
                    candidate[i]++;
                }
                return this.IsValid(candidate) ? candidate : [];
            });
        }
        private bool IsValid(int[] candidate)
        {
            return !candidate.Where((t, i) => t > joltageTarget[i]).Any();
        }
    }


    private const bool UseTestInput = false;

    private readonly IEnumerable<Machine> machines;

    public Solution(IEnumerable<string> testInput, IEnumerable<string> input)
    {
        this.machines = (UseTestInput ? testInput : input).Select(line =>
            {
                var targetLightDiagram = TargetDiagramRegex().Match(line).Groups[1].Value;

                var buttons = ButtonRegex().Matches(line)
                    .Select(match => match.Groups[1].Value)
                    .Select(s => s.Split(',').Select(int.Parse).ToArray());
                
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