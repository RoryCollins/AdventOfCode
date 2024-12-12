using _2024.Day10;

var testInput = File.ReadAllLines("testInput.txt")
    .ToList();
var input = File.ReadAllLines("input.txt")
    .ToList();
var solution = new Solution(testInput, input);

Console.WriteLine($"Part one: {solution.PartOne()}");
Console.WriteLine($"Part two: {solution.PartTwo()}");