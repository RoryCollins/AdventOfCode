namespace Shared;

public static class IntExtensions
{
    public static bool IsBetween(this int i, int i1, int i2)
    {
        return Math.Min(i1, i2) < i && i < Math.Max(i1, i2);
    }
}