using System.Diagnostics;

namespace MazeApp.Maze
{
    public sealed class ConnectionGrid
    {
        private readonly int _width;
        private readonly int _height;
        private readonly List<Vector2>?[,] _connectionsGrid;

        public ConnectionGrid(int width, int height)
        {
            _width = width;
            _height = height;
            _connectionsGrid = new List<Vector2>[width, height];
        }

        public Vector2 Size => new(_width, _height);

        public void ConnectAll()
        {
            for (int y = 0; y < _height; y++)
                for (int x = 0; x < _width; x++)
                {
                    Vector2 current = new(x, y);
                    if (x < _width - 1)
                        Connect(current, current + Vector2.Right);
                    if (y < _height - 1)
                        Connect(current, current + Vector2.Down);
                }
        }

        public void Connect(Vector2 lhs, Vector2 rhs)
        {
            ValidatePoint(lhs);
            ValidatePoint(rhs);
            AddConnection(lhs, rhs);
            AddConnection(rhs, lhs);
        }

        public void Disconnect(Vector2 lhs, Vector2 rhs)
        {
            ValidatePoint(lhs);
            ValidatePoint(rhs);
            RemoveConnection(lhs, rhs);
            RemoveConnection(rhs, lhs);
        }

        public bool HasConnection(Vector2 from, Vector2 to)
        {
            ValidatePoint(from);
            if (!Contains(to))
                return false;
            List<Vector2>? connections = _connectionsGrid[from.X, from.Y];
            return connections != null && connections.Contains(to);
        }

        public IEnumerable<Vector2> GetConnections(Vector2 from)
        {
            ValidatePoint(from);
            List<Vector2>? connections = _connectionsGrid[from.X, from.Y];
            if (connections == null)
                yield break;
            foreach (Vector2 connection in connections)
                yield return connection;
        }

        public bool Contains(Vector2 point)
        {
            return point.X >= 0 && point.Y >= 0 && point.X < _width && point.Y < _height;
        }

        private void AddConnection(Vector2 from, Vector2 to)
        {
            if (_connectionsGrid[from.X, from.Y] == null)
                _connectionsGrid[from.X, from.Y] = new List<Vector2>();
            _connectionsGrid[from.X, from.Y]!.Add(to);
        }

        private void RemoveConnection(Vector2 from, Vector2 to)
        {
            _connectionsGrid[from.X, from.Y]!.Remove(to);
        }

        private void ValidatePoint(Vector2 point)
        {
            if (!Contains(point))
                throw new ArgumentOutOfRangeException(nameof(point), "Point outside grid");
        }
    }
}
