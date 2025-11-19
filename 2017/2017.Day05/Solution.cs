namespace _2017.Day05;

public class Solution
{
    private const bool USE_TEST_INPUT = false;

    private readonly IEnumerable<int> instructions;

    public Solution(IEnumerable<string> testInput, IEnumerable<string> input)
    {
        this.instructions = (USE_TEST_INPUT ? testInput : input).Select(int.Parse);
    }

    public object PartOne()
    {
        var partOneInstructions = this.instructions.ToList();
        var currentIndex = 0;
        var count = 0;
        while (true)
        {
            if(currentIndex < 0 || currentIndex >= instructions.Count())
            {
                break;
            }
            var jump = partOneInstructions[currentIndex];
            partOneInstructions[currentIndex]++;
            currentIndex += jump;

            count++;
        }
        return count;
    }

    public object PartTwo()
    {
        var partTwoInstructions = this.instructions.ToList();
        var currentIndex = 0;
        var count = 0;
        while (true)
        {
            if(currentIndex < 0 || currentIndex >= instructions.Count())
            {
                break;
            }
            var jump = partTwoInstructions[currentIndex];
            Print(partTwoInstructions, currentIndex);
            if (jump >= 3)
            {
                partTwoInstructions[currentIndex]--;
            }
            else
            {
                partTwoInstructions[currentIndex]++;
            }
            currentIndex += jump;

            count++;
        }
        return count;
    }

    private void Print(List<int> instructions, int currentIndex)
    {
        Console.WriteLine($"{string.Join(" ", instructions[..currentIndex])} ({instructions[currentIndex]}) {string.Join(" ", instructions[(currentIndex+1)..])}");
    }
}