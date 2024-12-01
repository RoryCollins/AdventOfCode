namespace _2016.Day07;

using Shared;

internal record Ip(List<string> HypernetSequences, List<string> SupernetSequences)
{
    public bool SupportsTls()
    {
        return this.HypernetSequences.All(it => !HasAbba(it))
               && this.SupernetSequences.Any(it => HasAbba(it));
    }

    public bool SupportsSsl()
    {
        var abas = this.SupernetSequences
            .SelectMany(it => it.Windowed(3))
            .Select(it => new string(it))
            .Where(this.IsAba);

        var babs = this.HypernetSequences
            .SelectMany(it => it.Windowed(3))
            .Select(it => new string(it))
            .Where(this.IsAba);

        return abas.Any(it => HasCorresponding(babs, it));
    }

    private bool HasCorresponding(IEnumerable<string> babs, string aba)
    {
        return babs.Any(bab =>  bab[0] == aba[1] && bab[1] == aba[0] );
    }

    private bool IsAba(string it)
    {
        return it[0] == it[2]
               && it[0] != it[1];
    }


    private static bool HasAbba(string s)
    {
        return s.Windowed(4)
            .Any(IsAbba);
    }

    private static bool IsAbba(char[] s)
    {
        return s[0] == s[3]
               && s[1] == s[2]
               && s[0] != s[1];
    }
}