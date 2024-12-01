namespace _2016.Day07;

public class Solution
{

    private readonly List<Ip> ips = new();

    public Solution(IEnumerable<string> input)
    {
        foreach (var s in input)
        {
            var hypernets = new List<string>();
            var supernets = new List<string>();
            var deconstructed = s.Split(['[', ']']);

            for (int i = 0; i < deconstructed.Length; i++)
            {
                if (i % 2 == 0)
                {
                    supernets.Add(deconstructed[i]);
                }
                else
                {
                    hypernets.Add(deconstructed[i]);
                }
            }

            this.ips.Add(new Ip(hypernets, supernets));
        }
    }

    public object PartOne()
    {
        return this.ips.Count(it => it.SupportsTls());
    }


    public object PartTwo()
    {
        return this.ips.Count(it => it.SupportsSsl());
    }
}