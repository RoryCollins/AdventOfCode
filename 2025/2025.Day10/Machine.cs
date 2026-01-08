namespace _2025.Day10;

internal class Machine
{
    private readonly Dictionary<int, IEnumerable<Button[]>> _patternButtonMap = [];
    private readonly int _lightTarget;
    private readonly int[] _joltageTarget;

    public Machine(string lightTarget, IEnumerable<Button> buttons, int[] joltageTarget)
    {
        var targetDiagram = lightTarget
            .Replace('.', '0')
            .Replace('#', '1');
        _lightTarget = Convert.ToInt32(targetDiagram, 2);
        _joltageTarget = joltageTarget;

        DetermineButtonCombinations(buttons.ToArray(), 0, []);
    }


    public int ButtonPressesToReachLightTarget()
    {
        return _patternButtonMap[_lightTarget].Min(b => b.Length);
    }

    public int ButtonPressesToReachJoltageTarget()
    {
        return FindMinimumForTarget(_joltageTarget);
    }

    private int FindMinimumForTarget(int[] joltages)
    {
        if (joltages.All(i => i == 0))
        {
            return 0;
        }

        var oddJoltagesToBinary = string.Join("", joltages.Select(x => x % 2 == 0 ? 0 : 1));
        var joltageTarget = Convert.ToInt32(oddJoltagesToBinary, 2);

        if (!_patternButtonMap.TryGetValue(joltageTarget, out var options))
        {
            return 1_000_000;
        }

        return options.Min(option =>
        {
            var remainingJoltage = joltages.Select((j, i) => j - option.Count(b => b.ArrayRepresentation.Contains(i))).ToArray();
            if (remainingJoltage.Any(x => x < 0))
            {
                return 1_000_000;
            }
            var nxt = remainingJoltage.Select(j => j / 2).ToArray();
            return (2 * FindMinimumForTarget(nxt)) + option.Length;
        });
    }

    private void DetermineButtonCombinations(Button[] buttonsRemaining, int current, Button[] buttonsSelected)
    {
        if (buttonsRemaining.Length == 0)
        {
            if (_patternButtonMap.TryGetValue(current, out var l))
            {
                _patternButtonMap[current] = l.Append(buttonsSelected);
            }
            else
            {
                _patternButtonMap[current] = [buttonsSelected];
            }
        }
        else
        {
            var nextButtons = buttonsRemaining[1..];
            DetermineButtonCombinations(nextButtons, current ^ buttonsRemaining[0].IntegerRepresentation, [..buttonsSelected, buttonsRemaining[0]]);
            DetermineButtonCombinations(nextButtons, current, buttonsSelected);
        }
    }
}