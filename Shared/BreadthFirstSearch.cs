namespace Shared;

public class BreadthFirstSearch<TState>
    where TState : IEquatable<TState>
{
    private readonly TState end;
    private readonly Func<TState, IEnumerable<TState>> getNeighbours;
    private readonly Queue<TState> queue = new();
    private readonly HashSet<TState> visited = [];
    private readonly Dictionary<TState, int> distances = new();

    public BreadthFirstSearch(TState start, TState end, Func<TState, IEnumerable<TState>> getNeighbours)
    {
        this.end = end;
        this.getNeighbours = getNeighbours;
        this.queue.Enqueue(start);
        this.distances.Add(start, 0);
    }

    public int Search()
    {
        while (this.queue.Count > 0)
        {
            var current = this.queue.Dequeue();
            var currentDistance = this.distances[current];

            if (current.Equals(this.end))
            {
                return currentDistance;
            }

            this.visited.Add(current);

            var neighbours = this.getNeighbours(current);
            foreach (var neighbour in neighbours)
            {
                if (this.visited.Contains(neighbour)) continue;
                this.distances.TryAdd(neighbour, currentDistance+1);
                this.queue.Enqueue(neighbour);
            }
        }

        return -1;
    }
}