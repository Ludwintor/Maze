namespace MazeApp.Maze
{
    public static class Solver
    {
        public static void Levit(ConnectionGrid grid, List<Vector2> path, Vector2 start, Vector2 end, Action? onStep = null)
        {
            path.Clear();
            Vector2 size = grid.Size;
            int[,] distances = new int[size.X, size.Y];
            HashSet<Vector2> closedSet = new();
            HashSet<Vector2> openSet = new();
            PriorityQueue<Vector2, Priority> openQueue = new();
            openQueue.Enqueue(start, Priority.Normal);
            for (int y = 0; y < size.Y; y++)
                for (int x = 0; x < size.X; x++)
                {
                    distances[x, y] = int.MaxValue;
                    openSet.Add(new(x, y));
                }
            openSet.Remove(start);
            distances[start.X, start.Y] = 0;
            while (openQueue.Count > 0)
            {
                Vector2 current = openQueue.Dequeue();
                if (onStep != null)
                {
                    RestorePath(grid, path, distances, current);
                    onStep.Invoke();
                }
                foreach (Vector2 near in grid.GetConnections(current))
                {
                    if (openSet.Contains(near))
                    {
                        openQueue.Enqueue(near, Priority.Normal);
                        openSet.Remove(near);
                        RecalculateDistance(distances, current, near);
                    }
                    else if (openQueue.UnorderedItems.Contains((near, Priority.Normal)))
                    {
                        RecalculateDistance(distances, current, near);
                    }
                    else if (closedSet.Contains(near) && distances[near.X, near.Y] > distances[current.X, current.Y] + 1)
                    {
                        openQueue.Enqueue(near, Priority.Urgent);
                        closedSet.Remove(near);
                        RecalculateDistance(distances, current, near);
                    }
                }
                closedSet.Add(current);
                if (closedSet.Contains(end))
                {
                    RestorePath(grid, path, distances, end);
                    return;
                }
            }
        }

        private static void RestorePath(ConnectionGrid grid, List<Vector2> path, int[,] distances, Vector2 end)
        {
            path.Clear();
            path.Add(end);
            Vector2 current = end;
            int distance = distances[end.X, end.Y];
            while (distance > 0)
            {
                Vector2 next = Vector2.Zero;
                int minDistance = distance;
                foreach (Vector2 near in grid.GetConnections(current))
                {
                    int nearDistance = distances[near.X, near.Y];
                    if (nearDistance < minDistance)
                    {
                        minDistance = nearDistance;
                        next = near;
                    }
                }
                current = next;
                distance = minDistance;
                path.Add(next);
            }
            path.Reverse();
        }

        private static void RecalculateDistance(int[,] distances, Vector2 from, Vector2 to)
        {
            distances[to.X, to.Y] = Math.Min(distances[to.X, to.Y], distances[from.X, from.Y] + 1);
        }

        private enum Priority
        {
            Urgent = 0,
            Normal = 1
        }
    }
}
