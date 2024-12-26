namespace Shared;

public static class DirectionExtensions
{
    public static Direction TurnLeft(this Direction current)
    {
        return current switch
        {
            Direction.North => Direction.West,
            Direction.East => Direction.North,
            Direction.South => Direction.East,
            Direction.West => Direction.South,
            _ => throw new ArgumentOutOfRangeException(nameof(current), current, null)
        };
    }
    
    public static Direction TurnRight(this Direction current)
    {
        return current switch
        {
            Direction.North => Direction.East,
            Direction.East => Direction.South,
            Direction.South => Direction.West,
            Direction.West => Direction.North,
            _ => throw new ArgumentOutOfRangeException(nameof(current), current, null)
        };
    }
    
    public static Direction TurnBack(this Direction current)
    {
        return current switch
        {
            Direction.North => Direction.South,
            Direction.East => Direction.West,
            Direction.South => Direction.North,
            Direction.West => Direction.East,
            _ => throw new ArgumentOutOfRangeException(nameof(current), current, null)
        };
    }
}