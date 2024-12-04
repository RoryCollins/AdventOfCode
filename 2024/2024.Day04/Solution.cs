namespace _2024.Day04;

using System.Runtime.CompilerServices;
using System.Text;
using Shared;

public class Solution
{
    private readonly Grid grid;

    public Solution(IEnumerable<string> input)
    {
        this.grid = new Grid(input.ToList());
    }

    public object PartOne()
    {
        var xLocations = this.grid.FindAll('X');

        return xLocations.Sum(this.CountXmas);
    }

    public object PartTwo()
    {
        var aLocations = this.grid.FindAll('A')
            .Where(c =>
                (c.X >= 1 && c.X <= this.grid.Width - 2) &&
                (c.Y >= 1 && c.Y <= this.grid.Height - 2));

        return aLocations.Count(this.IsXMas);
    }

    private int CountXmas(Coordinate2D origin)
    {
        List<Coordinate2D> directions =
        [
            Coordinate2D.North,
            Coordinate2D.NorthEast,
            Coordinate2D.East,
            Coordinate2D.SouthEast,
            Coordinate2D.South,
            Coordinate2D.SouthWest,
            Coordinate2D.West,
            Coordinate2D.NorthWest,
        ];

        return directions
            .Where(d => this.grid.IsOnGrid(origin.Add(d * 3)))
            .Select(d =>
            {
                var sb = new StringBuilder();
                for (var i = 0; i < 4; i++)
                {
                    var next = origin.Add(d * i);
                    sb.Append(this.grid.At(next));
                }

                return sb.ToString();
            })
            .Count(it => it == "XMAS");
    }

    private bool IsXMas(Coordinate2D origin)
    {
        var aDiag = string.Join("", new[] { Coordinate2D.NorthEast, Coordinate2D.Origin, Coordinate2D.SouthWest }
            .Select(it => it.Add(origin))
            .Select(this.grid.At));

        var bDiag = string.Join("", new[] { Coordinate2D.SouthEast, Coordinate2D.Origin, Coordinate2D.NorthWest }
            .Select(it => it.Add(origin))
            .Select(this.grid.At));

        var masVariants = new[] { "SAM", "MAS" };
        return (masVariants.Contains(aDiag) && masVariants.Contains(bDiag));
    }
}