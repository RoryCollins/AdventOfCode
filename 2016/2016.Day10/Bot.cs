namespace _2016.Day10;

using Shared;

internal class Bot
{
    private readonly List<int> values = new();
    private readonly BotManager manager;
    private readonly int id;

    private (TargetType, int)? HighTarget;
    private (TargetType, int)? LowTarget;
    public int LowValue => this.values.Min();
    public int HighValue => this.values.Max();
    private int count = 0;

    public Bot(BotManager manager, int id)
    {
        this.manager = manager;
        this.id = id;
    }

    public void Add(int value)
    {
        if (count > 2)
        {
            throw new ProgrammerException();
        }
        this.values.Add(value);
        this.Process();
    }

    private void Process()
    {
        if (this.HighTarget.HasValue && this.LowTarget.HasValue && this.values.Count == 2)
        {
            this.manager.ProcessValues(this.HighTarget.Value, this.values.Max());
            this.manager.ProcessValues(this.LowTarget.Value, this.values.Min());
        }
    }

    public override string ToString()
    {
        var hv = this.values.Count > 0 ? this.values.Max().ToString() : "<empty>";
        var lv = this.values.Count > 0 ? this.values.Min().ToString() : "<empty>";
        return $"Bot {this.id} : ({hv} -> {this.HighTarget}) & ({lv} -> {this.LowTarget})";
    }

    public void SetLowTarget((TargetType , int ) destination)
    {
        if (LowTarget != null)
        {
            throw new ProgrammerException();
        }
        this.LowTarget = destination;
        this.Process();
    }
    public void SetHighTarget((TargetType , int ) destination)
    {
        if (HighTarget != null)
        {
            throw new ProgrammerException();
        }
        this.HighTarget = destination;
        this.Process();
    }
}