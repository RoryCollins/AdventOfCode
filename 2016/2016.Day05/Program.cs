﻿using _2016.Day05;

var input = File.ReadAllLines("input.txt")
    .ToList();
var solution = new Solution();

Console.WriteLine($"Part one: {solution.PartOne()}");
Console.WriteLine($"Part two: {solution.PartTwo()}");