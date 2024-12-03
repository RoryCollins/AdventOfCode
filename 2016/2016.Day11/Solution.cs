namespace _2016.Day11;

using System.Text.RegularExpressions;
using Shared;

abstract record Thing(string Name)
{

}

record Microchip(string Name) : Thing(Name);

record Generator(string Name) : Thing(Name);

record Floor(int level, List<Microchip> Microchips, List<Generator> Generators)
{
    public void Print()
    {
        Console.WriteLine($"FLOOR {level}");
        foreach (var generator in this.Generators)
        {
            Console.WriteLine(generator);
        }
        foreach (var microchip in this.Microchips)
        {
            Console.WriteLine(microchip);
        }
    }
};

public partial class Solution
{
    private List<Floor> floors = new List<Floor>();

    public Solution(IEnumerable<string> input)
    {
        var lines = input.ToArray();
        for (int i = 0; i < input.Count(); i++)
        {
            var line = lines[i];
            var generators = GeneratorRegex()
                .Matches(line)
                .Select(it => new Generator(it.Groups[1].Value))
                .ToList();

            var microchips = MicrochipRegex()
                .Matches(line)
                .Select(it => new Microchip(it.Groups[1].Value))
                .ToList();

            this.floors.Add(new Floor(i, microchips, generators));
        }
    }



    public object PartOne()
    {
        foreach (var floor in this.floors)
        {
            floor.Print();
        }

        return "Not yet implemented";
    }

    public object PartTwo()
    {
        return "Not yet implemented";
    }

    [GeneratedRegex(@"(\w+) generator")]
    private static partial Regex GeneratorRegex();

    [GeneratedRegex(@"(\w+)-compatible microchip")]
    private static partial Regex MicrochipRegex();
}