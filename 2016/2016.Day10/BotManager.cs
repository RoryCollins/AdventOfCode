namespace _2016.Day10;

using System.Text.RegularExpressions;
using Shared;

internal class BotManager
{
    private Dictionary<int, Bot> bots = new();
    public Dictionary<int, int> outputs = new();

    public void ProcessInstructions(IEnumerable<string> input)
    {
        foreach (var line in input)
        {
            var rx = new Regex(@"value (\d+) goes to bot (\d+)").Match(line);
            var handoverMatch = new Regex(@"bot (\d+) gives low to (bot|output) (\d+) and high to (bot|output) (\d+)").Match(line);

            if (rx.Success)
            {
                var bot = int.Parse(rx.Groups[2].Value);
                var value = int.Parse(rx.Groups[1].Value);
                this.bots.TryAdd(bot, new Bot(this, bot));

                this.bots[bot]
                    .Add(value);
            }
            else if (handoverMatch.Success)
            {
                var fromBot = int.Parse(handoverMatch.Groups[1].Value);
                var lowDestinationType = handoverMatch.Groups[2].Value == "bot" ? TargetType.Bot : TargetType.Output;
                var lowDestinationTarget = int.Parse(handoverMatch.Groups[3].Value);
                var highDestinationType = handoverMatch.Groups[4].Value == "bot" ? TargetType.Bot : TargetType.Output;
                var highDestinationTarget = int.Parse(handoverMatch.Groups[5].Value);

                this.bots.TryAdd(fromBot, new Bot(this, fromBot));
                this.bots[fromBot]
                    .SetLowTarget((lowDestinationType, lowDestinationTarget));
                this.bots[fromBot]
                    .SetHighTarget((highDestinationType, highDestinationTarget));
            }
            else
            {
                throw new ProgrammerException(line);
            }
        }
    }

    public void ProcessValues((TargetType type, int id) target, int value)
    {
        if (target.type == TargetType.Output)
        {
            this.outputs.TryAdd(target.id, value);
        }
        else
        {
            this.bots.TryAdd(target.id, new Bot(this, target.id));
            this.bots[target.id].Add(value);
        }
    }

    public int Find(int low, int high)
    {
        return this.bots.Single(it => it.Value.LowValue == low && it.Value.HighValue == high).Key;
    }
}