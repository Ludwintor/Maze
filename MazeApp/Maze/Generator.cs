using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeApp.Maze
{
    public static class Generator
    {
        public static void Generate(ConnectionGrid grid, int seed, Action? onStep = null)
        {
            grid.ConnectAll();
            Random rng = new(seed);
            Divide(grid, rng, onStep, Vector2.Zero, grid.Size - Vector2.One);
        }

        private static void Divide(ConnectionGrid grid, Random rng, Action? onStep, Vector2 start, Vector2 end)
        {
            if (end.X - start.X < 1 || end.Y - start.Y < 1)
                return;
            Vector2 point = new(rng.Next(start.X, end.X), rng.Next(start.Y, end.Y));
            for (int x = start.X; x <= end.X; x++)
            {
                Vector2 disconnect = new(x, point.Y);
                grid.Disconnect(disconnect, disconnect + Vector2.Down);
            }
            onStep?.Invoke();
            for (int y = start.Y; y <= end.Y; y++)
            {
                Vector2 disconnect = new(point.X, y);
                grid.Disconnect(disconnect, disconnect + Vector2.Right);
            }
            Span<int> gaps = stackalloc int[4];
            gaps[0] = rng.Next(start.Y, point.Y + 1); // Top
            gaps[1] = rng.Next(point.Y + 1, end.Y + 1); // Bottom
            gaps[2] = rng.Next(start.X, point.X + 1); // Left
            gaps[3] = rng.Next(point.X + 1, end.X + 1); // Right
            int blocked = rng.Next(4);
            for (int i = 0; i < 4; i++)
            {
                if (i == blocked)
                    continue;
                Vector2 gapStart;
                Vector2 gapEnd;
                if (i <= 1)
                {
                    gapStart = new(point.X, gaps[i]);
                    gapEnd = gapStart + Vector2.Right;
                }
                else
                {
                    gapStart = new(gaps[i], point.Y);
                    gapEnd = gapStart + Vector2.Down;
                }
                grid.Connect(gapStart, gapEnd);
                onStep?.Invoke();
            }
            Divide(grid, rng, onStep, start, point);
            Divide(grid, rng, onStep, new(point.X + 1, start.Y), new(end.X, point.Y));
            Divide(grid, rng, onStep, new(start.X, point.Y + 1), new(point.X, end.Y));
            Divide(grid, rng, onStep, point + Vector2.One, end);
        }
    }
}
