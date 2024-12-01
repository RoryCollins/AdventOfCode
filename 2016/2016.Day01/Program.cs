using _2016.Day01;

var input = File.ReadAllText("input.txt")
    .Split(',')
    .Select(f => f.Trim())
    .ToList();
var solution = new Solution(input);

Console.WriteLine($"Part one: {solution.PartOne()}");
Console.WriteLine($"Part two: {solution.PartTwo()}");