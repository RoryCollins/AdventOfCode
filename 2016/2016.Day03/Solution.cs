namespace _2016.Day03;

public class Solution(IEnumerable<string> input)
{
    private IEnumerable<(int, int, int)> sortedValues = new List<(int, int, int)>();

    public object PartOne()
    {
        this.sortedValues = input.Select(line =>
        {
            var gs = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] vals = [gs[0], gs[1], gs[2]];
            var parsedInts = vals.Select(it => int.Parse(it)).ToList();
            parsedInts.Sort();
            return (parsedInts[0], parsedInts[1], parsedInts[2]);
        });
        return this.sortedValues.Count(it => it.Item1 + it.Item2 > it.Item3);
    }

    public object PartTwo()
    {
        this.sortedValues = input.Chunk(3)
            .SelectMany(lines =>
            {
                List<int>[] triangles = [[], [], []];

                for (var i = 0; i < 3; i++)
                {
                    var gs = lines[i]
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    for (var j = 0; j < 3; j++)
                    {
                        triangles[j].Add(int.Parse(gs[j]));
                    }
                }

                return triangles.Select(t =>
                {
                    t.Sort();
                    return (t[0], t[1], t[2]);
                });

            });
        return this.sortedValues.Count(it => it.Item1 + it.Item2 > it.Item3);
    }
}