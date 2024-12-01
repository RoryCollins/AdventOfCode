namespace _2016.Day08;

using System.Text;
using System.Text.RegularExpressions;
using Shared;

public class Solution
{
    private HashSet<Coordinate2D> coordinates = new();
    private readonly IEnumerable<string> input;
    private const int Height = 6;
    private const int Width = 50;

    public Solution(IEnumerable<string> input)
    {
        this.input = input;
    }

    public object PartOne()
    {
        foreach (var s in this.input)
        {
            var rectRegex = new Regex(@"rect (\d+)x(\d+)");
            var match = rectRegex.Match(s);
            if (match.Success)
            {
                for (int y = 0; y < int.Parse(match.Groups[2].Value); y++)
                {
                    for (int x = 0; x < int.Parse(match.Groups[1].Value); x++)
                    {
                        this.coordinates.Add(new(x, y));
                    }
                }
            }

            var rotateXRegex = new Regex(@"rotate column x=(\d+) by (\d+)");
            match = rotateXRegex.Match(s);
            if (match.Success)
            {
                var rotatedCoordinates = this.coordinates.Where(coordinate => coordinate.X == int.Parse(match.Groups[1].Value)).ToList();
                this.ShiftCoordinates(rotatedCoordinates, new Coordinate2D(0, int.Parse(match.Groups[2].Value)));
            }

            var rotateYRegex = new Regex(@"rotate row y=(\d+) by (\d+)");
            match = rotateYRegex.Match(s);
            if (match.Success)
            {
                var rotatedCoordinates = this.coordinates.Where(coordinate => coordinate.Y == int.Parse(match.Groups[1].Value)).ToList();
                this.ShiftCoordinates(rotatedCoordinates, new Coordinate2D(int.Parse(match.Groups[2].Value), 0));
            }
        }

        return this.coordinates.Count;
    }

    private void ShiftCoordinates(List<Coordinate2D> rotatedCoordinates, Coordinate2D offset)
    {
        this.coordinates.RemoveWhere(it => rotatedCoordinates.Contains(it));
        var newCoordinates = rotatedCoordinates.Select(it => this.AddWithWrap(it, offset));
        this.coordinates.UnionWith(newCoordinates);
    }

    public object PartTwo()
    {
        for (int y = 0; y < Height; y++)
        {
            var sb = new StringBuilder();
            for (int x = 0; x < Width ; x++)
            {
                var foo = this.coordinates.Contains(new(x, y)) ? "#" : " ";
                sb.Append(foo);
            }

            Console.WriteLine(sb.ToString());
        }
        return "(as above)";
    }

    private Coordinate2D AddWithWrap(Coordinate2D a, Coordinate2D b)
    {
        var proposal = a.Add(b);
        return new Coordinate2D(proposal.X % Width, proposal.Y % Height);
    }
}