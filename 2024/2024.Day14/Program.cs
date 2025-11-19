using System.Diagnostics;
using _2024.Day14;

var testInput = File.ReadAllLines("testInput.txt")
    .ToList();
var input = File.ReadAllLines("input.txt")
    .ToList();
var solution = new Solution(testInput, input);
var stopwatch = new Stopwatch();

stopwatch.Start();
Console.WriteLine($"Part one: {solution.PartOne()}");
stopwatch.Stop();
var partOneSpeed = stopwatch.ElapsedMilliseconds;
stopwatch.Restart();
Console.WriteLine($"Part two: {solution.PartTwo()}");
stopwatch.Stop();
var partTwoSpeed = stopwatch.ElapsedMilliseconds;

Console.WriteLine();
Console.WriteLine($"Part one took {partOneSpeed}ms");
Console.WriteLine($"Part two took {partTwoSpeed}ms");