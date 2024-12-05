namespace _2016.Day11;

using System.Collections;
using System.Text;

internal class State : IEquatable<State>
{
    public State(int elevator, List<GmPair> gmPairs)
    {
        this.GmPairs = gmPairs;
        this.Elevator = elevator;
    }

    public override int GetHashCode()
    {
        return this.Elevator.GetHashCode() + this.GmPairs.Sum(it => it.GetHashCode() / 17);
    }

    public List<GmPair> GmPairs { get; init; }
    public int Elevator { get; init; }

    public bool Equals(State? other)
    {
        if (this.Elevator != other.Elevator) return false;

        foreach (var combination in this.GmPairs.Distinct())
        {
            var thisCount = this.GmPairs.Count(it => it == combination);
            var otherCount = other.GmPairs.Count(it => it == combination);
            if (thisCount != otherCount) return false;
        }

        return true;
    }

    public void Print()
    {
        for (int y = 3; y >= 0; y--)
        {
            var line = new StringBuilder();
            line.Append($"{y}: ");
            line.Append(this.Elevator == y ? "E" : ".");
            foreach (var pair in this.GmPairs)
            {
                line.Append("|");
                line.Append(pair.Generator == y ? "G" : ".");
                line.Append(pair.Microchip == y ? "M" : ".");
            }

            Console.WriteLine(line.ToString());
        }
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as State);
    }
}