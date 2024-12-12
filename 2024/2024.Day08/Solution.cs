namespace _2024.Day08;

using Shared;

public class Solution
{
    private const bool UseTestInput = false;

    private readonly List<string> input;
    private HashSet<char> beacons;
    private Grid<char> grid;
    private HashSet<Coordinate2D> antinodes = [];


    public Solution(IEnumerable<string> testInput, IEnumerable<string> input)
    {
        this.input = (UseTestInput ? testInput : input).ToList();
        this.grid = new Grid<char>(this.input);
        this.beacons = this.input.SelectMany(line => line.Distinct()
                .Except(['.']))
            .ToHashSet();
    }

    public object PartOne()
    {
        foreach (var beacon in this.beacons)
        {
            var instances = this.grid
                .FindAll(beacon)
                .ToArray();
            for (int i = 0; i < instances.Length - 1; i++)
            {
                for (int j = i + 1; j < instances.Length; j++)
                {
                    var distance = instances[i]
                        .ShiftTo(instances[j]);

                    this.AddAntinodes(instances[j], distance, Part.One);
                    this.AddAntinodes(instances[i], distance * -1, Part.One);
                }
            }
        }

        return this.antinodes
            .Count(it => this.grid.IsOnGrid(it));
    }

    public object PartTwo()
    {
        foreach (var beacon in this.beacons)
        {
            var antennae = this.grid
                .FindAll(beacon)
                .ToArray();
            for (int i = 0; i < antennae.Length - 1; i++)
            {
                this.antinodes.Add(antennae[i]);

                for (int j = i + 1; j < antennae.Length; j++)
                {
                    this.antinodes.Add(antennae[j]);
                    var distance = antennae[i]
                        .ShiftTo(antennae[j]);

                    this.AddAntinodes(antennae[j], distance, Part.Two);
                    this.AddAntinodes(antennae[i], distance * -1, Part.Two);
                }
            }
        }

        return this.antinodes.Count;
    }

    private void AddAntinodes(Coordinate2D from, Coordinate2D shift, Part part)
    {
        var next = from.Add(shift);
        if (!this.grid.IsOnGrid(next)) return;
        this.antinodes.Add(next);
        if (part == Part.Two)
        {
            this.AddAntinodes(next, shift, part);
        }
    }
}