using MazeApp.Maze;

namespace MazeApp
{
    public partial class Form1 : Form
    {
        private readonly Pen _pen;
        private readonly List<Vector2> _path;
        private ConnectionGrid? _grid;
        private int _seed;
        private bool _isBusy;

        public Form1()
        {
            InitializeComponent();
            _pen = new Pen(Color.Black);
            _path = new();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private async Task GenerateGrid(int seed)
        {
            _isBusy = true;
            _path.Clear();
            int size = (int)_sizeInput.Value;
            _grid = new ConnectionGrid(size, size);
            await Task.Run(() => Generator.Generate(_grid!, seed, OnCanvasStep));
            _isBusy = false;
        }

        private async Task FindPath(ConnectionGrid grid)
        {
            _isBusy = true;
            await Task.Run(() => Solver.Levit(grid, _path, Vector2.Zero, grid.Size - Vector2.One, OnCanvasStep));
            _isBusy = false;
        }

        private async void GenerateButton_Click(object sender, EventArgs e)
        {
            if (_isBusy)
                return;
            if (!int.TryParse(_seedInput.Text, out int seed))
                seed = string.IsNullOrWhiteSpace(_seedInput.Text) ? Guid.NewGuid().GetHashCode()
                                                                  : _seedInput.Text.GetHashCode();
            _seedInput.Text = seed.ToString();
            _seed = seed;
            await GenerateGrid(seed);
            _canvas.Invalidate();
        }

        private async void GenerateRandomButton_Click(object sender, EventArgs e)
        {
            if (_isBusy)
                return;
            int seed = Guid.NewGuid().GetHashCode();
            _seedInput.Text = seed.ToString();
            _seed = seed;
            await GenerateGrid(seed);
            _canvas.Invalidate();
        }

        private async void SolveButton_Click(object sender, EventArgs e)
        {
            if (_grid == null || _isBusy)
                return;
            await FindPath(_grid);
            _canvas.Invalidate();
        }


        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (_grid == null || _isBusy)
                return;
            SaveFileDialog dialog = new()
            {
                Filter = "MAZ file|*.maz",
                ValidateNames = true
            };
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            string path = dialog.FileName;
            dialog.Dispose();
            MazeSave save = new() { Grid = _grid, Seed = _seed };
            save.SaveToFile(path);
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            if (_isBusy)
                return;
            OpenFileDialog dialog = new()
            {
                Filter = "MAZ file|*.maz",
                ValidateNames = true
            };
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            string path = dialog.FileName;
            dialog.Dispose();
            if (!Path.Exists(path))
                return;
            MazeSave save = MazeSave.LoadFromFile(path);
            _grid = save.Grid;
            _seed = save.Seed;
            _seedInput.Text = save.Seed.ToString();
            _sizeInput.Value = save.Grid.Size.X;
            _canvas.Invalidate();
        }

        private void ExportPngButton_Click(object sender, EventArgs e)
        {
            if (_isBusy)
                return;
            SaveFileDialog dialog = new()
            {
                Filter = "PNG file|*.png",
                ValidateNames = true
            };
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            string path = dialog.FileName;
            dialog.Dispose();
            using (Bitmap bitmap = new(_canvas.ClientSize.Width, _canvas.ClientSize.Height))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                    DrawGrid(_grid, _path, graphics, bitmap.Size);
                bitmap.Save(path);
            }
            MessageBox.Show("PNG saved!");
        }

        private void ExportGifButton_Click(object sender, EventArgs e)
        {
            if (_grid == null || _isBusy)
                return;
            SaveFileDialog dialog = new()
            {
                Filter = "GIF file|*.gif",
                ValidateNames = true
            };
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            string path = dialog.FileName;
            dialog.Dispose();
            using (Bitmap frame = new(_canvas.ClientSize.Width, _canvas.ClientSize.Height))
            {
                using GifWriter writer = new(path);
                ConnectionGrid grid = new(_grid.Size.X, _grid.Size.Y);
                List<Vector2> gridPath = new();
                Generator.Generate(grid, _seed, () => OnGifStep(grid, gridPath, frame, writer));
                Solver.Levit(grid, gridPath, Vector2.Zero, grid.Size - Vector2.One, () => OnGifStep(grid, gridPath, frame, writer));
                writer.WriteFrame(frame, 4000);
            }
            MessageBox.Show("GIF saved!");
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            DrawGrid(_grid, _path, e.Graphics, _canvas.ClientSize);
        }

        private void DrawGrid(ConnectionGrid? grid, List<Vector2>? path, Graphics graphics, Size size)
        {
            if (grid == null)
                return;
            graphics.Clear(Color.White);
            DrawWalls(grid, graphics, size);
            if (path != null && path.Count > 0)
                DrawPath(grid, path, graphics, size);
        }

        private void DrawWalls(ConnectionGrid grid, Graphics graphics, Size size)
        {
            Vector2 gridSize = grid.Size;
            float lineSize = (size.Width - 1) / (float)gridSize.X;
            _pen.Width = 1;
            _pen.Color = Color.Black;
            graphics.DrawLine(_pen, PointF.Empty, new PointF(size.Width, 0f));
            graphics.DrawLine(_pen, PointF.Empty, new PointF(0f, size.Height));
            for (int y = 0; y < gridSize.Y; y++)
                for (int x = 0; x < gridSize.X; x++)
                {
                    Vector2 current = new(x, y);
                    if (!grid.HasConnection(current, current + Vector2.Right))
                    {
                        PointF start = new((x + 1) * lineSize, y * lineSize);
                        PointF end = new(start.X, start.Y + lineSize);
                        graphics.DrawLine(_pen, start, end);
                    }
                    if (!grid.HasConnection(current, current + Vector2.Down))
                    {
                        PointF start = new(x * lineSize, (y + 1) * lineSize);
                        PointF end = new(start.X + lineSize, start.Y);
                        graphics.DrawLine(_pen, start, end);
                    }
                }
        }

        private void DrawPath(ConnectionGrid grid, List<Vector2> path, Graphics graphics, Size size)
        {
            Vector2 gridSize = grid.Size;
            float lineSize = (size.Width - 1) / (float)gridSize.X;
            float halfSize = lineSize / 2f;
            _pen.Color = Color.LightGreen;
            _pen.Width = 3;
            Vector2 point = path[0];
            for (int i = 1; i < path.Count; i++)
            {
                Vector2 next = path[i];
                PointF start = new(point.X * lineSize + halfSize, point.Y * lineSize + halfSize);
                PointF end = new(next.X * lineSize + halfSize, next.Y * lineSize + halfSize);
                graphics.DrawLine(_pen, start, end);
                point = next;
            }
        }

        private void OnCanvasStep()
        {
            _canvas.Invalidate();
            Thread.Sleep(1);
        }

        private void OnGifStep(ConnectionGrid grid, List<Vector2> path, Bitmap frame, GifWriter writer)
        {
            using (Graphics graphics = Graphics.FromImage(frame))
                DrawGrid(grid, path, graphics, frame.Size);
            writer.WriteFrame(frame, 20);
        }
    }
}