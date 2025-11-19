namespace _2017.Day03;

using Shared;
using static Shared.Direction;

public class Solution
{
    private const bool USE_TEST_INPUT = false;

    private readonly int _input;

    public Solution(int testInput, int input)
    {
        _input = USE_TEST_INPUT ? testInput : input;
    }

    public object PartOne()
    {
        var layer = 1;
        var centres = new int[4];
        while (true)
        {
            layer += 1;
            var sideLength = (layer * 2) + 1;
            var squareMaximumValue = sideLength * sideLength;
            if (squareMaximumValue <= _input) continue;

            for (int i = 0; i < 4; i++)
            {
                centres[i] = (squareMaximumValue - layer) - i * (sideLength - 1);
            }
            break;
        }
        var distanceFromCentre = centres.Min(c => Math.Abs(_input - c));
        return layer + distanceFromCentre;
    }

    public object PartTwo()
    {
        var currentCoordinate = new Coordinate2D(0, 0);
        var dict = new Dictionary<Coordinate2D, int>
        {
            { currentCoordinate, 1 }
        };

        var currentData = 1;
        var stepsForward = 1;
        var currentStepsForward = 0;
        var direction = East;
        while (currentData < _input)
        {
            currentCoordinate = currentCoordinate.Move(direction);

            currentStepsForward++;
            if (currentStepsForward == stepsForward)
            {
                direction = direction.TurnLeft();
                currentStepsForward = 0;
                if (new[]{East, West}.Contains(direction))
                {
                    stepsForward++;
                }
            }

            currentData = currentCoordinate
                .GetNeighbors()
                .Sum(c => dict.GetValueOrDefault(c));

            dict[currentCoordinate] = currentData;
        }
        return currentData;
    }

}