using System;
using System.Drawing;
using System.Windows.Forms;

namespace VectorEditor
{
    public partial class MainForm : Form
    {
        private Canvas canvas;
        private DoubleBufferedPanel canvasPanel;
        private Panel toolPanel;
        private Button selectButton;
        private Button lineButton;
        private Button rectangleButton;
        private Button ellipseButton;
        private Button colorButton;
        private Button deleteButton;
        private ColorDialog colorDialog;
        private Label statusLabel;

        public MainForm()
        {
            InitializeComponent();
            canvas = new Canvas();
            colorButton.BackColor = canvas.CurrentColor;
            colorButton.ForeColor = GetContrastColor(canvas.CurrentColor);
            UpdateStatus();
        }

        private void InitializeComponent()
        {
            this.canvasPanel = new VectorEditor.DoubleBufferedPanel();
            this.toolPanel = new System.Windows.Forms.Panel();
            this.selectButton = new System.Windows.Forms.Button();
            this.lineButton = new System.Windows.Forms.Button();
            this.rectangleButton = new System.Windows.Forms.Button();
            this.ellipseButton = new System.Windows.Forms.Button();
            this.colorButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.toolPanel.SuspendLayout();
            this.SuspendLayout();

            this.canvasPanel.BackColor = System.Drawing.Color.White;
            this.canvasPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.canvasPanel.Location = new System.Drawing.Point(150, 10);
            this.canvasPanel.Name = "canvasPanel";
            this.canvasPanel.Size = new System.Drawing.Size(800, 600);
            this.canvasPanel.TabIndex = 0;
            this.canvasPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.CanvasPanel_Paint);
            this.canvasPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CanvasPanel_MouseDown);
            this.canvasPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CanvasPanel_MouseMove);
            this.canvasPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CanvasPanel_MouseUp);

            this.toolPanel.BackColor = System.Drawing.SystemColors.Control;
            this.toolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolPanel.Controls.Add(this.selectButton);
            this.toolPanel.Controls.Add(this.lineButton);
            this.toolPanel.Controls.Add(this.rectangleButton);
            this.toolPanel.Controls.Add(this.ellipseButton);
            this.toolPanel.Controls.Add(this.colorButton);
            this.toolPanel.Controls.Add(this.deleteButton);
            this.toolPanel.Location = new System.Drawing.Point(10, 10);
            this.toolPanel.Name = "toolPanel";
            this.toolPanel.Size = new System.Drawing.Size(130, 600);
            this.toolPanel.TabIndex = 1;

            this.selectButton.Location = new System.Drawing.Point(10, 10);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(110, 30);
            this.selectButton.Text = "Выбор";
            this.selectButton.Click += new System.EventHandler(this.SelectButton_Click);

            this.lineButton.Location = new System.Drawing.Point(10, 50);
            this.lineButton.Name = "lineButton";
            this.lineButton.Size = new System.Drawing.Size(110, 30);
            this.lineButton.Text = "Линия";
            this.lineButton.Click += new System.EventHandler(this.LineButton_Click);

            this.rectangleButton.Location = new System.Drawing.Point(10, 90);
            this.rectangleButton.Name = "rectangleButton";
            this.rectangleButton.Size = new System.Drawing.Size(110, 30);
            this.rectangleButton.Text = "Прямоугольник";
            this.rectangleButton.Click += new System.EventHandler(this.RectangleButton_Click);

            this.ellipseButton.Location = new System.Drawing.Point(10, 130);
            this.ellipseButton.Name = "ellipseButton";
            this.ellipseButton.Size = new System.Drawing.Size(110, 30);
            this.ellipseButton.Text = "Эллипс";
            this.ellipseButton.Click += new System.EventHandler(this.EllipseButton_Click);

            this.colorButton.Location = new System.Drawing.Point(10, 200);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(110, 30);
            this.colorButton.BackColor = System.Drawing.Color.Black;
            this.colorButton.ForeColor = System.Drawing.Color.White;
            this.colorButton.Text = "Цвет";
            this.colorButton.Click += new System.EventHandler(this.ColorButton_Click);

            this.deleteButton.Location = new System.Drawing.Point(10, 240);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(110, 30);
            this.deleteButton.Text = "Удалить";
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);

            this.statusLabel.Location = new System.Drawing.Point(150, 620);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(800, 20);
            this.statusLabel.Text = "Готово";

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 650);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.toolPanel);
            this.Controls.Add(this.canvasPanel);
            this.Name = "MainForm";
            this.Text = "Векторный графический редактор";
            this.toolPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void CanvasPanel_Paint(object sender, PaintEventArgs e)
        {
            canvas.Draw(e.Graphics);
        }

        private void CanvasPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (canvas.CurrentTool == ToolType.Select)
                {
                    canvas.SelectShape(e.Location);
                    if (canvas.SelectedShape != null)
                    {
                        canvas.StartMoving(e.Location);
                    }
                }
                else
                {
                    canvas.StartDrawing(e.Location);
                }
                UpdateStatus();
                canvasPanel.Invalidate();
            }
        }

        private void CanvasPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (canvas.CurrentTool == ToolType.Select && canvas.IsMoving)
                {
                    canvas.UpdateMoving(e.Location);
                }
                else if (canvas.IsDrawing)
                {
                    canvas.UpdateDrawing(e.Location);
                }

                canvasPanel.Invalidate();
            }
        }

        private void CanvasPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (canvas.CurrentTool == ToolType.Select)
                {
                    canvas.FinishMoving();
                }
                else
                {
                    canvas.FinishDrawing();
                }
                UpdateStatus();
                canvasPanel.Invalidate();
            }
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            canvas.CurrentTool = ToolType.Select;
            UpdateStatus();
        }

        private void LineButton_Click(object sender, EventArgs e)
        {
            canvas.CurrentTool = ToolType.Line;
            UpdateStatus();
        }

        private void RectangleButton_Click(object sender, EventArgs e)
        {
            canvas.CurrentTool = ToolType.Rectangle;
            UpdateStatus();
        }

        private void EllipseButton_Click(object sender, EventArgs e)
        {
            canvas.CurrentTool = ToolType.Ellipse;
            UpdateStatus();
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            colorDialog.Color = canvas.CurrentColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                canvas.CurrentColor = colorDialog.Color;
                colorButton.BackColor = colorDialog.Color;
                colorButton.ForeColor = GetContrastColor(colorDialog.Color);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            canvas.DeleteSelectedShape();
            canvasPanel.Invalidate();
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            string toolText = canvas.CurrentTool switch
            {
                ToolType.Select => "Выбор",
                ToolType.Line => "Линия",
                ToolType.Rectangle => "Прямоугольник",
                ToolType.Ellipse => "Эллипс",
                _ => "Неизвестно"
            };

            statusLabel.Text = $"Инструмент: {toolText} | Фигур на холсте: {canvas.Shapes.Count}";
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Delete:
                    canvas.DeleteSelectedShape();
                    canvasPanel.Invalidate();
                    UpdateStatus();
                    return true;
                case Keys.L:
                    canvas.CurrentTool = ToolType.Line;
                    UpdateStatus();
                    return true;
                case Keys.R:
                    canvas.CurrentTool = ToolType.Rectangle;
                    UpdateStatus();
                    return true;
                case Keys.E:
                    canvas.CurrentTool = ToolType.Ellipse;
                    UpdateStatus();
                    return true;
                case Keys.S:
                    canvas.CurrentTool = ToolType.Select;
                    UpdateStatus();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private Color GetContrastColor(Color color)
        {
            int luminance = (int)(0.299 * color.R + 0.587 * color.G + 0.114 * color.B);
            return luminance > 128 ? Color.Black : Color.White;
        }
    }
}