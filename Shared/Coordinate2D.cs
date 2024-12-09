namespace Shared;

public enum Direction
{
    North,
    West,
    South,
    East
}

public record Coordinate2D(int X, int Y)
{
    public Coordinate2D Add(Coordinate2D other)
    {
        return new Coordinate2D(X + other.X, Y + other.Y);
    }

    public Coordinate2D Move(Direction direction, int steps = 1)
    {
        var d =  direction switch
        {
            Direction.North => North,
            Direction.West => West,
            Direction.South => South,
            Direction.East => East,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
        return this.Add(d*steps);
    }

    public int ManhattanDistanceTo(Coordinate2D other)
    {
        return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
    }

    public Coordinate2D ShiftTo(Coordinate2D other)
    {
        return new(other.X - this.X, other.Y - this.Y);
    }

    public static Coordinate2D Origin => new(0, 0);
    public static Coordinate2D North => new(0, 1);
    public static Coordinate2D South => new(0, -1);
    public static Coordinate2D East => new(1, 0);
    public static Coordinate2D West => new(-1, 0);
    public static Coordinate2D SouthEast => new(1, -1);
    public static Coordinate2D SouthWest => new(-1, -1);
    public static Coordinate2D NorthEast => new(1, 1);
    public static Coordinate2D NorthWest => new(-1, 1);

    public static Coordinate2D operator *(Coordinate2D a, int scalar)
    {
        return new Coordinate2D(a.X * scalar, a.Y * scalar);
    }
}