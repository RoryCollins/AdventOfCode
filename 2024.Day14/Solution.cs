using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Shared;

namespace _2024.Day14;

record Robot(Coordinate2D Position, Coordinate2D Velocity);

public partial class Solution
{
    private const bool UseTestInput = false;

    private readonly IEnumerable<Robot> robots;
    private readonly int maxX;
    private readonly int maxY;

    public Solution(IEnumerable<string> testInput, IEnumerable<string> input)
    {
        maxX = UseTestInput ? 11 : 101;
        maxY = UseTestInput ? 7 : 103;

        this.robots = (UseTestInput ? testInput : input).Select(line =>
        {
            var match = RobotRegex().Match(line);
            var px = int.Parse(match.Groups[1].Value);
            var py = int.Parse(match.Groups[2].Value);
            var vx = int.Parse(match.Groups[3].Value);
            var vy = int.Parse(match.Groups[4].Value);
            return new Robot(new Coordinate2D(px, py), new Coordinate2D(vx, vy));
        });
    }

    public object PartOne()
    {
        var oneHundredSecondsLater = Enumerable.Range(1, 100).Aggregate(robots, (acc, _) => acc.Select(Move)).ToList();

        var midX = maxX / 2;
        var midY = maxY / 2;
        
        var qa = oneHundredSecondsLater.Where(r => r.Position.X < midX && r.Position.Y < midY);
        var qb = oneHundredSecondsLater.Where(r => r.Position.X > midX && r.Position.Y < midY);
        var qc = oneHundredSecondsLater.Where(r => r.Position.X < midX && r.Position.Y > midY);
        var qd = oneHundredSecondsLater.Where(r => r.Position.X > midX && r.Position.Y > midY);
        
        return new[] {qa, qb, qc, qd}.Aggregate(1, (acc, it) => acc * it.Count());
    }

    public object PartTwo()
    {
        var rs = this.robots.ToList();
        var count = 0;
        while (true)
        {
            rs = rs.Select(Move).ToList();
            count++;

            if (rs.GroupBy(r => r.Position.Y).Any(g =>
                {
                    var xs = g.Select(r => r.Position.X).Distinct().ToList();
                    return Enumerable.Range(1,10).All(n => xs.Contains(xs.Min() + n));
                }))
            {
                Print(rs);
                break;
            }

        }

        return count;
    }

    private void Print(List<Robot> robots)
    {
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                var cellRobots = robots.Count(r => r.Position == new Coordinate2D(x, y));
                Console.Write(cellRobots > 0 ? 'X' : '.');
            }
            Console.WriteLine();
        }
    }


    private Robot Move(Robot robot)
    {
        var nextPosition = robot.Position.Add(robot.Velocity);
        var nextX = robot.Position.X + robot.Velocity.X;
        if (nextX >= maxX) nextX %= maxX;
        else if (nextX < 0) nextX += maxX;

        var nextY = robot.Position.Y + robot.Velocity.Y;
        if (nextY >= maxY) nextY %= maxY;
        else if (nextY < 0) nextY += maxY;

        return robot with { Position = new Coordinate2D(nextX, nextY) };
    }

    [GeneratedRegex(@"^p=(\d+),(\d+) v=(-?\d+),(-?\d+)$")]
    private static partial Regex RobotRegex();
}