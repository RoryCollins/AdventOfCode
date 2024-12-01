namespace Shared;

public record Line(Coordinate2D Start, Coordinate2D End)
{
    public bool Crosses(Line other, out Coordinate2D? crossingPoint)
    {
        bool crosses;
        if (this.Start.Y == this.End.Y)
        {
            crossingPoint = new Coordinate2D(other.Start.X, this.Start.Y);
            crosses = other.Start.X.IsBetween(this.Start.X, this.End.X)
                   && this.Start.Y.IsBetween(other.Start.Y, other.End.Y);
        }

        else
        {
            crossingPoint = new Coordinate2D(this.Start.X, other.Start.Y);
            crosses = this.Start.X.IsBetween(other.Start.X, other.End.X)
                      && other.Start.Y.IsBetween(this.Start.Y, this.End.Y);
        }

        if (!crosses)
        {
            crossingPoint = null;
        }

        return crosses;
    }
}