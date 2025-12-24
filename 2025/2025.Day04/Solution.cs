using Shared;

namespace _2025.Day04;

public class Solution
{
    private const bool UseTestInput = false;

    private readonly IEnumerable<string> input;
    private readonly Dictionary<Coordinate2D, HashSet<Coordinate2D>> allRolls;

    public Solution(IEnumerable<string> testInput, IEnumerable<string> input)
    {
        this.input = UseTestInput ? testInput : input;
        var grid = new Grid<char>(input);
        this.allRolls = BuildRollRelationshipMap(grid);
    }

    public object PartOne()
    {
        return this.allRolls.Count(x => x.Value.Count < 4);
    }

    public object PartTwo()
    {
        var original = allRolls.Count;
        
        while (allRolls.Any(x => x.Value.Count < 4))
        {
            var roll =  allRolls.First(x => x.Value.Count < 4);
            var neighbours = roll.Value;
            foreach (var neighbour in neighbours)
            {
                allRolls[neighbour].Remove(roll.Key);
            }
            allRolls.Remove(roll.Key);
        }
        
        return original - allRolls.Count;

    }
    
    private static Dictionary<Coordinate2D, HashSet<Coordinate2D>> BuildRollRelationshipMap(Grid<char> grid)
    {

        var allRolls = grid.FindAll('@').ToDictionary(x => x, x => new HashSet<Coordinate2D>());

        foreach (var roll in allRolls.Keys)
        {
            var neighbours = roll.GetNeighbors();
            foreach (var neighbour in neighbours)
            {
                if (allRolls.TryGetValue(neighbour, out var neighbourRelationships))
                {
                    allRolls[roll].Add(neighbour);
                    neighbourRelationships.Add(roll);
                }
            }
        }
        return allRolls;
    }
}