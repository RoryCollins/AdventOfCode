namespace Shared;

public record DirectionalCoordinate2D(Coordinate2D Coordinate, Direction Direction)
{
    public DirectionalCoordinate2D Move()
    {
        var d = this.Direction switch
        {
            Direction.North => Coordinate2D.North,
            Direction.West => Coordinate2D.West,
            Direction.South => Coordinate2D.South,
            Direction.East => Coordinate2D.East,
            _ => throw new ProgrammerException()
        };

        return new(this.Coordinate.Add(d), this.Direction);
    }

    public static DirectionalCoordinate2D Origin => new DirectionalCoordinate2D(Coordinate2D.Origin, Direction.North);
}