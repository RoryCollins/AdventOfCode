namespace Shared;

using System.Text;

public static class StringExtensions
{
    public static IEnumerable<int> AllIndexesOf(this string str, string searchstring)
    {
        int minIndex = str.IndexOf(searchstring, StringComparison.Ordinal);
        while (minIndex != -1)
        {
            yield return minIndex;
            minIndex = str.IndexOf(searchstring, minIndex + searchstring.Length, StringComparison.Ordinal);
        }
    }

    public static string Repeat(this string s, int n)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < n; i++)
        {
            sb.Append(s);
        }

        return sb.ToString();
    }
}