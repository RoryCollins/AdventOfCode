namespace _2025.Day02;

using System.Globalization;
using Shared;

public class Solution
{
    private const bool UseTestInput = false;

    private record IdRange(long Start, long End)
    {
        public IEnumerable<long> GetInvalidIds()
        {
            var newStart = TakeHalf(this.Start);
            var newEnd = TakeHalf(this.End);

            if (newStart > newEnd)
            {
                newStart /= 10;
            }

            for (long i = newStart; i <= newEnd; i++)
            {
                var s = i.ToString();
                var target = long.Parse(string.Join("", [s, s]));
                if (target >= this.Start && target <= this.End)
                {
                    yield return target;
                }
            }
        }

        public IEnumerable<long> GetInvalidIdsPartTwo()
        {
            return GetCandidates(Start, End).ToHashSet();
        }

        private long TakeHalf(long i)
        {
            var halfwayLength = (i.ToString().Length + 1) / 2;
            return long.Parse(i.ToString()[..halfwayLength]);
        }

        private static IEnumerable<long> GetCandidates(long start, long end)
        {
            var s = start.ToString();
            var e = end.ToString();

            var l = int.Parse(e[..((e.Length + 1) / 2)]);
            for (int i = 1; i <= l; i++)
            {
                var sequence = i.ToString();
                for (int n = s.Length; n <= e.Length; n++)
                {
                    var times = n / sequence.Length;
                    var candidate = long.Parse(sequence.Repeat(times));

                    if (candidate >= start && candidate <= end && candidate > 10)
                    {
                        yield return candidate;
                    }
                }
            }
        }
    };

    private readonly IEnumerable<IdRange> ranges;

    public Solution(string testInput, string input)
    {
        this.ranges = (UseTestInput ? testInput : input)
            .Split(',')
            .Select(line =>
            {
                var range = line.Split('-');
                return new IdRange(long.Parse(range[0]), long.Parse(range[1]));
            });
    }

    public object PartOne()
    {
        return ranges.Sum(r => r.GetInvalidIds().Sum());


        foreach (var range in ranges)
        {
            Console.WriteLine($"{range.Start}-{range.End}");
            foreach (var id in range.GetInvalidIds())
            {
                Console.Write($"|{id}");
            }
            Console.WriteLine();
        }
        return true;
    }

    public object PartTwo()
    {

        return ranges.Sum(r => r.GetInvalidIdsPartTwo().Sum());
    }
}