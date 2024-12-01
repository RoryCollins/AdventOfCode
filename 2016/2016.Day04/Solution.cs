namespace _2016.Day04;

using System.Text.RegularExpressions;

public class Solution
{
    private readonly IEnumerable<Room> rooms;

    private record Room(string Name, int Value, string Checksum)
    {
        public bool IsReal()
        {
            var realChecksum = string.Join("", this.Name
                .Replace("-", "")
                .GroupBy(i => i)
                .OrderByDescending(it => it.Count())
                .ThenBy(it => it.Key)
                .Select(it => it.Key)
                .Take(5));
            return realChecksum == this.Checksum;
        }

        public string DecryptedName()
        {
            var alphabet = "abcdefghijklmnopqrstuvwxyz";
            return string.Join("", this.Name
                .Select(it => it switch
                    {
                        '-' => ' ',
                        _ => alphabet[(alphabet.IndexOf(it) + this.Value % 26) % 26]
                    }
                ));
        }
    }

    public Solution(IEnumerable<string> input)
    {
        var rx = new Regex(@"([a-z\-]+)(\d+)\[([a-z]{5})\]");

        this.rooms = input.Select(s =>
        {
            var gs = rx.Match(s).Groups;
            return new Room(gs[1].Value, int.Parse(gs[2].Value), gs[3].Value);
        });

    }

    public object PartOne()
    {
        return rooms
            .Where(it => it.IsReal())
            .Sum(it => it.Value);
    }

    public object PartTwo()
    {
        return this.rooms.Single(it =>
                it.IsReal() &&
                it.DecryptedName().Contains("north")).Value;
    }
}