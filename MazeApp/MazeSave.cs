using System.Drawing;
using MazeApp.Maze;
using Newtonsoft.Json;

namespace MazeApp
{
    public class MazeSave
    {
        public ConnectionGrid Grid { get; set; } = null!;

        public int Seed { get; set; }

        public static MazeSave LoadFromFile(string path)
        {
            string json = File.ReadAllText(path);
            Serialized serialized = JsonConvert.DeserializeObject<Serialized>(json);
            return new MazeSave
            {
                Seed = serialized.Seed,
                Grid = DeserializeGrid(serialized.Connections)
            };
        }

        public void SaveToFile(string path)
        {
            Connection[,] serializedGrid = SerializeGrid();
            string json = JsonConvert.SerializeObject(new Serialized { Connections = serializedGrid, Seed = Seed }, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        private Connection[,] SerializeGrid()
        {
            Vector2 size = Grid!.Size;
            Connection[,] result = new Connection[size.X, size.Y];
            for (int y = 0; y < size.Y; y++)
                for (int x = 0; x < size.X; x++)
                {
                    Connection connection = Connection.None;
                    Vector2 current = new(x, y);
                    if (x < size.X - 1 && Grid.HasConnection(current, current + Vector2.Right))
                        connection = Connection.Right;
                    if (y < size.Y - 1 && Grid.HasConnection(current, current + Vector2.Down))
                        connection = connection == Connection.Right ? Connection.Both : Connection.Down;
                    result[x, y] = connection;
                }
            return result;
        }

        private static ConnectionGrid DeserializeGrid(Connection[,] connections)
        {
            ConnectionGrid grid = new(connections.GetLength(0), connections.GetLength(1));
            Vector2 size = grid.Size;
            for (int y = 0; y < size.Y; y++)
                for (int x = 0; x < size.X; x++)
                {
                    Connection connection = connections[x, y];
                    Vector2 current = new(x, y);
                    if (x < size.X - 1 && (connection == Connection.Right || connection == Connection.Both))
                        grid.Connect(current, current + Vector2.Right);
                    if (y < size.Y - 1 && (connection == Connection.Down || connection == Connection.Both))
                        grid.Connect(current, current + Vector2.Down);
                }
            return grid;
        }

        private enum Connection
        {
            None = 0,
            Right = 1,
            Down = 2,
            Both = 3
        }

        private struct Serialized
        {
            [JsonProperty("seed")]
            public int Seed { get; set; }
            [JsonProperty("connections")]
            public Connection[,] Connections { get; set; }
        }
    }
}
