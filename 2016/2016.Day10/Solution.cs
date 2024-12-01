namespace _2016.Day10;

public class Solution
{
    private BotManager botManager;

    public Solution(IEnumerable<string> input)
    {
        this.botManager = new BotManager();
        this.botManager.ProcessInstructions(input);
    }

    public object PartOne()
    {
        return this.botManager.Find(17, 61);
    }

    public object PartTwo()
    {
        return this.botManager.outputs
            .OrderBy(it => it.Key)
            .Take(3)
            .Aggregate(1, (i, pair) => i * pair.Value);
    }
}