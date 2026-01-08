namespace _2025.Day11;

public class Solution
{
    private const bool USE_TEST_INPUT = false;

    private readonly Dictionary<string, string[]> _paths;

    public Solution(IEnumerable<string> testInput, IEnumerable<string> input)
    {
        this._paths = (USE_TEST_INPUT ? testInput : input).Select(line =>
        {
            var temp = line.Split(':');
            var key = temp[0];
            var vs = temp[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return (key, vs);
        }).ToDictionary();
        this._paths.Add("out", []);
    }

    public object PartOne()
    {
        return CountPathsToDestination("you", "out", []);
    }

    public object PartTwo()
    {
        var pathOne = CountPathsToDestination("svr", "fft", [])
                      * CountPathsToDestination("fft", "dac", [])
                      * CountPathsToDestination("dac", "out", []);

        var pathTwo = CountPathsToDestination("svr", "dac", [])
                      * CountPathsToDestination("dac", "fft", [])
                      * CountPathsToDestination("fft", "out", []);

        return pathOne + pathTwo;
    }

    private long CountPathsToDestination(string source, string destination, Dictionary<string, long> pathsDictionary)
    {
        var next = _paths[source];

        var result = next.Sum(s => s == destination
            ? 1
            : pathsDictionary.TryGetValue(s, out var c)
                ? c
                : CountPathsToDestination(s, destination, pathsDictionary));

        pathsDictionary[source] = result;
        return result;
    }
}