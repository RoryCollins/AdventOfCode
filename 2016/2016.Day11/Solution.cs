namespace _2016.Day11;

using System.Text.RegularExpressions;
using Shared;

internal record GmPair(int Microchip, int Generator);

public partial class Solution
{
    // private List<GmPair> combinations = [];
    private readonly State initialState;

    public Solution(IEnumerable<string> input)
    {
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

        var pairs = generators
            .Select((g => new GmPair(
                microchips.Single(it => it.name == g.name)
                    .floor,
                g.floor)))
            .ToList();

        this.initialState = new State(0, pairs);
    }

    public object PartOne()
    {
        var endState = new State(2,
            [
                new GmPair(1, 0),
                new GmPair(1, 0),
                new GmPair(2, 2),
                new GmPair(0, 0 ),
                new GmPair(0, 0)
            ]
        );

        var bfs = new BreadthFirstSearch<State>(this.initialState, endState, this.GetNeighbours);
        return bfs.Search();
    }

    public object PartTwo()
    {
        return "Not yet implemented";
    }

    private IEnumerable<State> GetNeighbours(State state)
    {
        var neighbourOptions = new List<State>();
        var currentFloor = state.Elevator;
        var nextFloorOptions = new List<int> { -1, 1 }
            .Select(it => currentFloor + it)
            .Where(it => it is >= 0 and <= 3);


        // return nextFloorOptions.Select(it => new State(it, state.GmPairs));

        foreach (var nextFloor in nextFloorOptions)
        {
            for (int i = 0; i < state.GmPairs.Count; i++)
            {
                var head = state.GmPairs[..i];
                var tail = state.GmPairs[(i+1)..];
                var p = state.GmPairs[i];

                if (!new[] { p.Generator, p.Microchip }.Contains(currentFloor)) continue;
                if (p.Generator == p.Microchip)
                {
                    var newPairs = new List<GmPair>();
                    newPairs.AddRange(head);
                    newPairs.Add(new (nextFloor, nextFloor));
                    newPairs.AddRange(tail);
                    neighbourOptions.Add(new State(nextFloor,newPairs));
                }

                for (int j = i; j < state.GmPairs.Count; j++)
                {
                }

                //
                //     if (state.GmPairs[i].Generator == currentFloor)
                //     {
                //         var carriedGi = state.GmPairs[i] with { Generator = nextFloor };
                //     }
                //
                // if (state.GmPairs[i].Microchip == currentFloor)
                // {
                //     var carriedMi = state.GmPairs[i] with { Microchip = nextFloor };
                // }
                //
                // var carriedGm = new GmPair(Generator: nextFloor, Microchip: nextFloor);
                //
                // for (int j = i; j < state.GmPairs.Count; j++)
                // {
                //     if (state.GmPairs[j].Generator == currentFloor)
                //     {
                //         var carriedGj = state.GmPairs[i] with { Generator = nextFloor };
                //     }
                //
                //     if (state.GmPairs[i].Microchip == currentFloor)
                //     {
                //         var carriedMj = state.GmPairs[i] with { Microchip = nextFloor };
                //     }
                //
                //     // yield return new State()
                // }
            }
        }

        return neighbourOptions;
    }

    [GeneratedRegex(@"(\w+) generator")]
    private static partial Regex GeneratorRegex();

    [GeneratedRegex(@"(\w+)-compatible microchip")]
    private static partial Regex MicrochipRegex();
}