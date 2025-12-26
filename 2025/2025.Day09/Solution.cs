using Shared;

namespace _2025.Day09;

public class Solution
{
    private const bool UseTestInput = false;

    private readonly Coordinate2D[] input;

    public Solution(IEnumerable<string> testInput, IEnumerable<string> input)
    {
        this.input = (UseTestInput ? testInput : input).Select(line =>
        {
            var values = line.Split(',').Select(int.Parse).ToArray();
            return new Coordinate2D(values[0], values[1]);
        }).ToArray();
    }

    public object PartOne()
    {
        var areas = new Dictionary<(Coordinate2D a, Coordinate2D b), long>();
        for (int i = 0; i < this.input.Length - 1; i++)
        {
            var a = this.input[i];
            for (int j = i; j < this.input.Length; j++)
            {
                var b = this.input[j];
                var area = (Math.Abs(b.X - a.X) + 1L) * (Math.Abs(b.Y - a.Y) + 1L);
                areas.Add((a, b), area);
            }
        }

        return areas.MaxBy(x => x.Value).Value;
    }

    public object PartTwo()
    {
        var uniqueXs = this.input.Select(c => c.X).Distinct().OrderBy(x => x).Select((c, i) => (c, i)).ToDictionary();
        var uniqueYs = this.input.Select(c => c.Y).Distinct().OrderBy(x => x).Select((c, i) => (c, i)).ToDictionary();
        var xReverseLookup = uniqueXs.ToDictionary(c => c.Value, c => c.Key);
        var yReverseLookup = uniqueYs.ToDictionary(c => c.Value, c => c.Key);

        var condensedCoordinates = this.input.Select(c => new Coordinate2D(uniqueXs[c.X], uniqueYs[c.Y])).ToArray();

        var validTiles = condensedCoordinates.ToHashSet();
        for (int i = 0; i < condensedCoordinates.Length; i++)
        {
            var current = condensedCoordinates[i];
            var next = condensedCoordinates[(i + 1) % condensedCoordinates.Length];

            if (current.Y == next.Y)
            {
                var min = Math.Min(current.X, next.X);
                var max = Math.Max(current.X, next.X);
                for (int x = min; x <= max; x++)
                {
                    validTiles.Add(new(x, current.Y));
                }
            }
            else
            {
                var min = Math.Min(current.Y, next.Y);
                var max = Math.Max(current.Y, next.Y);
                for (int y = min; y <= max; y++)
                {
                    validTiles.Add(new(current.X, y));
                }
            }
        }
        
        var startingPoint = condensedCoordinates.OrderBy(c => c.Y).ThenBy(c => c.X).First().Add(new Coordinate2D(1, 1));

        var queue = new Queue<Coordinate2D>([startingPoint]);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (!validTiles.Add(current))
            {
                continue;
            }

            Direction[] directions = [Direction.North, Direction.East, Direction.South, Direction.West];
            foreach (var direction in directions)
            {
                queue.Enqueue(current.Move(direction));
            }
        }

        var areas = new Dictionary<(Coordinate2D a, Coordinate2D b), long>();
        for (int i = 0; i < condensedCoordinates.Length - 1; i++)
        {
            var a = condensedCoordinates[i];
            
            for (int j = i; j < condensedCoordinates.Length; j++)
            {
                var b = condensedCoordinates[j];

                var valid = true;
                for (int y = Math.Min(a.Y, b.Y); y <= Math.Max(a.Y, b.Y); y++)
                {
                    for (int x = Math.Min(a.X, b.X); x <= Math.Max(a.X, b.X); x++)
                    {
                        if (!validTiles.Contains(new Coordinate2D(x, y)))
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (!valid) break;
                }
                if (!valid) continue;
                
                var realA = new Coordinate2D(xReverseLookup[a.X], yReverseLookup[a.Y]);
                var realB = new Coordinate2D(xReverseLookup[b.X], yReverseLookup[b.Y]);
                
                var area = (Math.Abs(realB.X - realA.X) + 1L) * (Math.Abs(realB.Y - realA.Y) + 1L);
                areas.Add((a, b), area);
            }
        }
        
        return areas.MaxBy(x => x.Value).Value;
    }


}