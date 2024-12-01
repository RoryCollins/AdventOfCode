namespace _2016.Day05;

using System.Collections.Concurrent;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

public partial class Solution
{
    private readonly string secretKey = "uqwqemis";

    public object PartOne()
    {
        return this.Timed(this.FindHash);
    }

    public object PartTwo()
    {
        var foo = (ConcurrentDictionary<int, char>)this.Timed(this.FindHash2);
        var result = new StringBuilder();
        for (int i = 0; i < 8; i++)
        {
            result.Append(foo[i]);
        }

        return result.ToString();
    }

    private object Timed(Func<object> f)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        var result = f();
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
        return result;
    }

    private object FindHash()
    {
        var q = new ConcurrentQueue<char>();

        var range = Enumerable.Range(1, int.MaxValue).AsParallel();
        Parallel.ForEach(
            range,
            MD5.Create,
            (i, state, md5) =>
            {
                var hashBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(this.secretKey + i));
                var hash = string.Join("", hashBytes.Select(b => b.ToString("x2")));
                if (hash.StartsWith("00000"))
                {
                    q.Enqueue(hash[5]);
                    if (q.Count == 8)
                    {
                        state.Stop();
                    }
                }

                return md5;
            },
            _ => { });
        return string.Join("", q);
    }

    private ConcurrentDictionary<int, char> FindHash2()
    {
        var q = new ConcurrentDictionary<int, char>();
        var rx = MyRegex();

        var range = Enumerable.Range(1, int.MaxValue).AsParallel();
        Parallel.ForEach(
            range,
            MD5.Create,
            (i, state, md5) =>
            {
                var hashBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(this.secretKey + i));
                var hash = string.Join("", hashBytes.Select(b => b.ToString("x2")));
                var result = rx.Match(hash);
                if (result.Success)
                {
                    var gs = result.Groups;
                    var index = int.Parse(gs[1].Value);
                    var value = gs[2].Value.Single();
                    q.TryAdd(index, value);
                    if (q.Count == 8)
                    {
                        state.Stop();
                    }
                }

                return md5;
            },
            _ => { });
        return q;
    }

    [GeneratedRegex(@"^00000([0-7])(.).*")]
    private static partial Regex MyRegex();
}