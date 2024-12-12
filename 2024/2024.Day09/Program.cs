using _2024.Day09;

var testInput = File.ReadAllText("testInput.txt");
var input = File.ReadAllText("input.txt");
var solution = new Solution(testInput, input);

Console.WriteLine($"Part one: {solution.PartOne()}");
Console.WriteLine($"Part two: {solution.PartTwo()}");