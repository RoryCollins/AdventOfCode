namespace _2016.Day11;

using System.Text.RegularExpressions;
using Shared;

internal record GmPair(int Microchip, int Generator);

internal class State : IEquatable<State>
{
    public State(int elevator, List<GmPair> gmPairs)
    {
        this.GmPairs = gmPairs;
        this.Elevator = elevator;
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
}

public partial class Solution
{
    // private List<GmPair> combinations = [];
    private readonly State initialState;

    public Solution(IEnumerable<string> input)
    {
        this.initialState = new State(0, []);

        var lines = input.ToArray();
        var generators = new List<(string name, int floor)>();
        var microchips = new List<(string name, int floor)>();
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            generators.AddRange(GeneratorRegex()
                .Matches(line)
                .Select(it => (it.Groups[1].Value, i)));

            microchips.AddRange(MicrochipRegex()
                .Matches(line)
                .Select(it => (it.Groups[1].Value, i)));
        }

        foreach (var (name, floor) in generators)
        {
            this.initialState.GmPairs.Add(new GmPair(microchips.Single(it => it.name == name)
                .floor, floor));
        }
    }

    public object PartOne()
    {
        var endState = new State(3,
            [
                new GmPair(3, 3),
                new GmPair(3, 3),
                new GmPair(3, 3),
                new GmPair(3, 3),
                new GmPair(3, 3)
            ]
        );

        Console.WriteLine($"INITIAL STATE");
        Console.WriteLine($"Elevator: {this.initialState.Elevator}");
        Console.WriteLine("---");
        foreach (var pair in this.initialState.GmPairs)
        {
            Console.WriteLine($"Microchip: {pair.Microchip}");
            Console.WriteLine($"Generator: {pair.Generator}");
            Console.WriteLine();
        }

        // var bfs = new BreadthFirstSearch<State>(this.initialState, endState, this.GetNeighbours);
        // return bfs.Search();
        return "Not yet implemented";
    }

    public object PartTwo()
    {
        return "Not yet implemented";
    }

    private IEnumerable<State> GetNeighbours(State state)
    {
        var neighbours = new List<State>();
        var possibleLevels = new List<int> { -1, 1 }
            .Select(it => state.Elevator + it)
            .Where(it => it is >= 0 and <= 3);

        foreach (var newLevel in possibleLevels)
        {
            for (int i = 0; i < state.GmPairs.Count; i++)
            {
                if (state.GmPairs[i].Generator == state.Elevator)
                {
                    var carriedGi = state.GmPairs[i] with { Generator = newLevel };
                }

                if (state.GmPairs[i].Microchip == state.Elevator)
                {
                    var carriedMi = state.GmPairs[i] with { Microchip = newLevel };
                }

                var carriedGm = new GmPair(Generator: newLevel, Microchip: newLevel);

                for (int j = i; j < state.GmPairs.Count; j++)
                {
                    if (state.GmPairs[j].Generator == state.Elevator)
                    {
                        var carriedGj = state.GmPairs[i] with { Generator = newLevel };
                    }

                    if (state.GmPairs[i].Microchip == state.Elevator)
                    {
                        var carriedMj = state.GmPairs[i] with { Microchip = newLevel };
                    }

                    // yield return new State()
                }
            }
        }
    }

    [GeneratedRegex(@"(\w+) generator")]
    private static partial Regex GeneratorRegex();

    [GeneratedRegex(@"(\w+)-compatible microchip")]
    private static partial Regex MicrochipRegex();
}