namespace Shared;

public class Grid<T> where T : IEquatable<T>
{
    public readonly int Width;
    public readonly int Height;
    private readonly List<List<T>> contents;

    public Grid(IEnumerable<IEnumerable<T>> input)
    {
        var rows = input.ToList();

        Height = rows.Count;
        Width = rows.First().Count();
        contents = rows.Select(it => it.ToList()).ToList();
    }
    
    // public void Reversed() => this.contents.Reverse();

    // public Coordinate2D TopLeft() => new(0, Height - 1);
    // public Coordinate2D BottomRight() => new(Width - 1, 0);

    public IEnumerable<Coordinate2D> GetNeighbours(Coordinate2D centre, bool includeDiagonals)
    {
        var directions = new[]
        {
            Coordinate2D.North,
            Coordinate2D.South,
            Coordinate2D.East,
            Coordinate2D.West,
        };

        if (includeDiagonals)
        {
            directions = directions.Concat(new[]
                    {
                        Coordinate2D.NorthEast,
                        Coordinate2D.NorthWest,
                        Coordinate2D.SouthEast,
                        Coordinate2D.SouthWest,
                    }
                )
                .ToArray();
        }

        return directions
            .Select(centre.Add)
            .Where(IsOnGrid);
    }

    public bool IsOnGrid(Coordinate2D coordinate)
    {
        return (coordinate.X >= 0 && coordinate.X < Width)
               && (coordinate.Y >= 0 && coordinate.Y < Height);
    }

    public T At(Coordinate2D coordinate)
    {
        return contents[coordinate.Y][coordinate.X];
    }

    // ReSharper disable once UnusedMember.Global
    // Useful for debugging
    public void Print()
    {
        foreach (var line in this.contents)
        {
            Console.WriteLine(string.Join("", line));
        }
    }

    public IEnumerable<Coordinate2D> FindAll(T c)
    {
        for (int y = 0; y < this.Height; y++)
        {
            for (int x = 0; x < this.Width; x++)
            {
                var coordinate = new Coordinate2D(y, x);
                if (this.At(coordinate).Equals(c)) yield return coordinate;
            }
        }
    }
}