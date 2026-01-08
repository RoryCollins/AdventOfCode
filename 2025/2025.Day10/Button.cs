namespace _2025.Day10;

internal class Button(int[] configuration, int max)
{
    public int IntegerRepresentation => Convert.ToInt32(string.Join("", Enumerable.Range(0, max).Select(i => configuration.Contains(i) ? "1" : "0")), 2);
    public int[] ArrayRepresentation => configuration;
}