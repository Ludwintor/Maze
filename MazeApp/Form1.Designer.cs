namespace MazeApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            _canvas = new PictureBox();
            _generateGroup = new GroupBox();
            _generateRandomButton = new Button();
            _generateButton = new Button();
            _seedInput = new TextBox();
            _seedLabel = new Label();
            _sizeLabel = new Label();
            _sizeInput = new NumericUpDown();
            _solutionGroup = new GroupBox();
            _solveButton = new Button();
            _saveLoadGroup = new GroupBox();
            _exportPngButton = new Button();
            _loadButton = new Button();
            _saveButton = new Button();
            _exportGifButton = new Button();
            ((System.ComponentModel.ISupportInitialize)_canvas).BeginInit();
            _generateGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_sizeInput).BeginInit();
            _solutionGroup.SuspendLayout();
            _saveLoadGroup.SuspendLayout();
            SuspendLayout();
            // 
            // _canvas
            // 
            _canvas.Location = new Point(12, 12);
            _canvas.Name = "_canvas";
            _canvas.Size = new Size(620, 620);
            _canvas.TabIndex = 0;
            _canvas.TabStop = false;
            _canvas.Paint += Canvas_Paint;
            // 
            // _generateGroup
            // 
            _generateGroup.Controls.Add(_generateRandomButton);
            _generateGroup.Controls.Add(_generateButton);
            _generateGroup.Controls.Add(_seedInput);
            _generateGroup.Controls.Add(_seedLabel);
            _generateGroup.Controls.Add(_sizeLabel);
            _generateGroup.Controls.Add(_sizeInput);
            _generateGroup.Location = new Point(653, 12);
            _generateGroup.Name = "_generateGroup";
            _generateGroup.Size = new Size(213, 199);
            _generateGroup.TabIndex = 1;
            _generateGroup.TabStop = false;
            _generateGroup.Text = "Generation";
            // 
            // _generateRandomButton
            // 
            _generateRandomButton.Location = new Point(37, 156);
            _generateRandomButton.Name = "_generateRandomButton";
            _generateRandomButton.Size = new Size(146, 30);
            _generateRandomButton.TabIndex = 5;
            _generateRandomButton.Text = "Generate (random seed)";
            _generateRandomButton.UseVisualStyleBackColor = true;
            _generateRandomButton.Click += GenerateRandomButton_Click;
            // 
            // _generateButton
            // 
            _generateButton.Location = new Point(37, 120);
            _generateButton.Name = "_generateButton";
            _generateButton.Size = new Size(146, 30);
            _generateButton.TabIndex = 4;
            _generateButton.Text = "Generate (from seed)";
            _generateButton.UseVisualStyleBackColor = true;
            _generateButton.Click += GenerateButton_Click;
            // 
            // _seedInput
            // 
            _seedInput.Location = new Point(6, 91);
            _seedInput.Name = "_seedInput";
            _seedInput.Size = new Size(201, 23);
            _seedInput.TabIndex = 3;
            // 
            // _seedLabel
            // 
            _seedLabel.AutoSize = true;
            _seedLabel.Location = new Point(6, 73);
            _seedLabel.Name = "_seedLabel";
            _seedLabel.Size = new Size(32, 15);
            _seedLabel.TabIndex = 2;
            _seedLabel.Text = "Seed";
            // 
            // _sizeLabel
            // 
            _sizeLabel.AutoSize = true;
            _sizeLabel.Location = new Point(6, 19);
            _sizeLabel.Name = "_sizeLabel";
            _sizeLabel.Size = new Size(51, 15);
            _sizeLabel.TabIndex = 1;
            _sizeLabel.Text = "Grid size";
            // 
            // _sizeInput
            // 
            _sizeInput.Location = new Point(6, 37);
            _sizeInput.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            _sizeInput.Name = "_sizeInput";
            _sizeInput.Size = new Size(70, 23);
            _sizeInput.TabIndex = 0;
            _sizeInput.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // _solutionGroup
            // 
            _solutionGroup.Controls.Add(_solveButton);
            _solutionGroup.Location = new Point(653, 217);
            _solutionGroup.Name = "_solutionGroup";
            _solutionGroup.Size = new Size(213, 62);
            _solutionGroup.TabIndex = 6;
            _solutionGroup.TabStop = false;
            _solutionGroup.Text = "Solution";
            // 
            // _solveButton
            // 
            _solveButton.Location = new Point(37, 22);
            _solveButton.Name = "_solveButton";
            _solveButton.Size = new Size(146, 30);
            _solveButton.TabIndex = 5;
            _solveButton.Text = "Pathfind";
            _solveButton.UseVisualStyleBackColor = true;
            _solveButton.Click += SolveButton_Click;
            // 
            // _saveLoadGroup
            // 
            _saveLoadGroup.Controls.Add(_exportGifButton);
            _saveLoadGroup.Controls.Add(_exportPngButton);
            _saveLoadGroup.Controls.Add(_loadButton);
            _saveLoadGroup.Controls.Add(_saveButton);
            _saveLoadGroup.Location = new Point(653, 285);
            _saveLoadGroup.Name = "_saveLoadGroup";
            _saveLoadGroup.Size = new Size(213, 173);
            _saveLoadGroup.TabIndex = 7;
            _saveLoadGroup.TabStop = false;
            _saveLoadGroup.Text = "Save/Load";
            // 
            // _exportPngButton
            // 
            _exportPngButton.Location = new Point(37, 94);
            _exportPngButton.Name = "_exportPngButton";
            _exportPngButton.Size = new Size(146, 30);
            _exportPngButton.TabIndex = 7;
            _exportPngButton.Text = "Export as PNG";
            _exportPngButton.UseVisualStyleBackColor = true;
            _exportPngButton.Click += ExportPngButton_Click;
            // 
            // _loadButton
            // 
            _loadButton.Location = new Point(37, 58);
            _loadButton.Name = "_loadButton";
            _loadButton.Size = new Size(146, 30);
            _loadButton.TabIndex = 6;
            _loadButton.Text = "Load";
            _loadButton.UseVisualStyleBackColor = true;
            _loadButton.Click += LoadButton_Click;
            // 
            // _saveButton
            // 
            _saveButton.Location = new Point(37, 22);
            _saveButton.Name = "_saveButton";
            _saveButton.Size = new Size(146, 30);
            _saveButton.TabIndex = 5;
            _saveButton.Text = "Save";
            _saveButton.UseVisualStyleBackColor = true;
            _saveButton.Click += SaveButton_Click;
            // 
            // _exportGifButton
            // 
            _exportGifButton.Location = new Point(37, 130);
            _exportGifButton.Name = "_exportGifButton";
            _exportGifButton.Size = new Size(146, 30);
            _exportGifButton.TabIndex = 8;
            _exportGifButton.Text = "Export as GIF";
            _exportGifButton.UseVisualStyleBackColor = true;
            _exportGifButton.Click += ExportGifButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(878, 641);
            Controls.Add(_saveLoadGroup);
            Controls.Add(_solutionGroup);
            Controls.Add(_generateGroup);
            Controls.Add(_canvas);
            DoubleBuffered = true;
            Name = "Form1";
            Text = "Maze";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)_canvas).EndInit();
            _generateGroup.ResumeLayout(false);
            _generateGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_sizeInput).EndInit();
            _solutionGroup.ResumeLayout(false);
            _saveLoadGroup.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox _canvas;
        private GroupBox _generateGroup;
        private Button _generateButton;
        private TextBox _seedInput;
        private Label _seedLabel;
        private Label _sizeLabel;
        private NumericUpDown _sizeInput;
        private Button _generateRandomButton;
        private GroupBox _solutionGroup;
        private Button _solveButton;
        private GroupBox _saveLoadGroup;
        private Button _saveButton;
        private Button _loadButton;
        private Button _exportPngButton;
        private Button _exportGifButton;
    }
}