using System.Diagnostics;
using _2024.Day11;

var testInput = File.ReadAllText("testInput.txt");
var input = File.ReadAllText("input.txt");
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