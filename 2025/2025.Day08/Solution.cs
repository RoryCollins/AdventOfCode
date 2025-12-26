using System.Collections;

namespace _2025.Day08;

public class Solution
{
    public record Coordinate3D(int X, int Y, int Z)
    {
        public double DistanceTo(Coordinate3D other)
        {
            var x = Math.Pow(other.X -  this.X, 2);
            var y = Math.Pow(other.Y -  this.Y, 2);
            var z = Math.Pow(other.Z -  this.Z, 2);

            return Math.Sqrt(x + y + z);
        }

        public override string ToString()
        {
            return $"({this.X},{this.Y},{this.Z})";
        }
    }

    private const bool UseTestInput = false;

    private readonly Coordinate3D[] input;
    private List<KeyValuePair<(Coordinate3D a, Coordinate3D b), double>> orderedDistances;

    public Solution(IEnumerable<string> testInput, IEnumerable<string> input)
    {
        this.input = (UseTestInput ? testInput : input).Select(x =>
        {
            var values = x.Split(',').Select(int.Parse).ToArray();
            return new Coordinate3D(values[0], values[1], values[2]);
        }).ToArray();
        var distances = new Dictionary<(Coordinate3D a, Coordinate3D b), double>();
        for(int i = 0; i < this.input.Length - 1; i++)
        {
            var a = this.input[i];
            for (int j = i + 1; j < this.input.Length; j++)
            {
                var b = this.input[j];
                distances.Add((a, b), a.DistanceTo(b));
            }
        }
        this.orderedDistances = distances.OrderBy(x => x.Value).ToList();
    }

    public object PartOne()
    {
       var groupMap = new Dictionary<Coordinate3D, int>();
       var groupNo = 0;

       foreach (var pair in this.orderedDistances.Take(UseTestInput ? 10 : 1000))
       {
           if (groupMap.TryGetValue(pair.Key.a, out var groupA))
           {
               if (groupMap.TryGetValue(pair.Key.b, out var groupB))
               {
                   if (groupA == groupB) continue;
                   foreach (var entry in groupMap.Where(g => g.Value == groupB).Select(g => g.Key))
                   {
                       groupMap[entry] = groupA;
                   }
               }
               else
               {
                   groupMap.Add(pair.Key.b, groupA);
               }
           }
           else if (groupMap.TryGetValue(pair.Key.b, out var groupB))
           {
               groupMap.Add(pair.Key.a, groupB);
           }
           else
           {    
               groupMap.Add(pair.Key.a, groupNo);
               groupMap.Add(pair.Key.b, groupNo);
               groupNo++;
           }
       }

       var t = groupMap.GroupBy(x => x.Value).OrderByDescending(g => g.Count()).Select(g => g.Count()).Take(3).ToArray();
       return t.Aggregate(1, (acc, x) => acc * x);
    }

    public object PartTwo()
    {
        var groupMap = new Dictionary<Coordinate3D, int>();
        var groupNo = 0;
        var queue = new Queue<(Coordinate3D a, Coordinate3D b)>(this.orderedDistances.Select(it => it.Key));

        while (queue.Count > 0)
        {
            var pair = queue.Dequeue();
            if (groupMap.TryGetValue(pair.a, out var groupA))
            {
                if (groupMap.TryGetValue(pair.b, out var groupB))
                {
                    if (groupA == groupB) continue;
                    foreach (var entry in groupMap.Where(g => g.Value == groupB).Select(g => g))
                    {
                        groupMap[entry.Key] = groupA;
                    }
                }
                else
                {
                    groupMap.Add(pair.b, groupA);
                }
            }
            else if (groupMap.TryGetValue(pair.b, out var groupB))
            {
                groupMap.Add(pair.a, groupB);
            }
            else
            {    
                groupMap.Add(pair.a, groupNo);
                groupMap.Add(pair.b, groupNo);
                groupNo++;
            }

            if (groupMap.Count == this.input.Length && groupMap.DistinctBy(x => x.Value).Count() == 1)
            {
                return (long)pair.a.X * (long)pair.b.X;
            }
        }

        return "foo";
    }
}