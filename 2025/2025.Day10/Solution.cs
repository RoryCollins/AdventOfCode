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

            //translate buttons to matrix in Row Echelon Form
            var rows = joltageTarget.Length;
            var cols = buttons.Count()+1;
            var buttonsArr = buttons.ToArray();
            var m = new List<int[]>();
            for (int y = 0; y < rows; y++)
            {
                var r = new List<int>();
                for (int x = 0; x < cols-1; x++)
                {
                    r.Add(buttonsArr[x].Contains(y) ? 1 : 0);
                }
                r.Add(joltageTarget[y]);
                m.Add(r.ToArray());
            }

            var matrix = m.ToArray().OrderByDescending(x => int.Parse(string.Join("", x[..^1]))).ToArray();

            // identify remaining free variables
            var freeVariables= Enumerable.Range(0, buttons.Count()).ToList();
            var buttonCounter = 0;
            foreach (var row in matrix)
            {
                if (buttonCounter > 0 && row[..buttonCounter].Any(x => x != 0))
                {
                    continue;
                }
                while (buttonCounter < buttons.Count())
                {
                    if (row[buttonCounter] == 1)
                    {
                        freeVariables.Remove(buttonCounter);
                        buttonCounter++;
                        break;
                    }
                    buttonCounter++;
                }
            }

            // find their max values
            var freeVariableValues = freeVariables
                .Select(i => (i, matrix
                    .Where(r => r[i] == 1)
                    .Select(r => r.Last()).ToArray()))
                .Select(fv => (fv.i, fv.Item2.Min()))
                .ToDictionary();

            matrix = matrix.OrderBy(x => int.Parse(string.Join("", x[..^1]))).ToArray();

            return DoItRx(matrix, freeVariableValues, new Dictionary<int, int>());
        }

        private static int DoItRx(int[][] matrix, Dictionary<int, int> freeVariableValues, Dictionary<int, int> currentValues)
        {
            if (freeVariableValues.Count == 0) return DoIt(matrix, currentValues);

            var (ix, max) = freeVariableValues.First();
            var results = Enumerable.Range(0, max).Select(r =>
            {
                var foo = freeVariableValues.ToDictionary();
                var nextVals = currentValues.ToDictionary();
                nextVals.Add(ix, r);
                foo.Remove(ix);
                return DoItRx(matrix, foo, nextVals);
            });

            return results.Min();
        }

        private static int DoIt(int[][] matrix, Dictionary<int, int> currentValues)
        {
            var valid = true;
            foreach (var row in matrix)
            {
                var sln = row[^1];
                var unknown = -1;
                for (var i = 0; i < row.Length - 1; i++)
                {
                    if (row[i] == 0) continue;

                    if (!currentValues.TryGetValue(i, out var value))
                    {
                        unknown = i;
                    };

                    sln -= value;
                    if (sln < 0)
                    {
                        valid = false;
                        break;
                    }
                }
                switch (unknown)
                {
                    case -1 when sln != 0:
                        valid = false;
                        break;

                    case >= 0:
                        currentValues[unknown] = sln;
                        break;
                }
                if (!valid) { break; }
            }
            return valid ? currentValues.Sum(it => it.Value) : int.MaxValue;
        }

        private IEnumerable<int> PressButtonForLights(int current)
        {
            return this.buttonsForLights.Select(button => current ^ button);
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
        return "skipped";
        return this.machines.Select(m => m.ButtonPressesToReachLightTarget()).Sum();
    }

    public object PartTwo()
    {
        return this.machines.Select(m => m.ButtonPressesToReachJoltageTarget()).Select(s => (long)s).Sum();
    }

    [GeneratedRegex(@"\[(.*)\]")]
    private static partial Regex TargetDiagramRegex();
    
    [GeneratedRegex(@"\((.*?)\)")]
    private static partial Regex ButtonRegex();
    
    [GeneratedRegex(@"\{(.*?)\}")]
    private static partial Regex JoltageRegex();
}