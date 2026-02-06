using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace VectorEditor
{
    public enum ToolType
    {
        Select,
        Line,
        Rectangle,
        Ellipse
    }

    public class Canvas
    {
        private List<Shape> shapes;
        private Shape selectedShape;
        private Shape previewShape;
        private ToolType currentTool;
        private bool isDrawing;
        private bool isMoving;
        private Point drawingStartPoint;
        private Color currentColor;

        public List<Shape> Shapes => shapes;
        public Shape SelectedShape => selectedShape;
        public ToolType CurrentTool
        {
            get { return currentTool; }
            set { currentTool = value; }
        }
        public bool IsDrawing => isDrawing;
        public bool IsMoving => isMoving;
        public Color CurrentColor
        {
            get { return currentColor; }
            set { currentColor = value; }
        }

        public Canvas()
        {
            shapes = new List<Shape>();
            currentTool = ToolType.Select;
            currentColor = Color.Black;
        }

        public void StartDrawing(Point point)
        {
            isDrawing = true;
            drawingStartPoint = point;

            switch (currentTool)
            {
                case ToolType.Line:
                    previewShape = new Line();
                    break;
                case ToolType.Rectangle:
                    previewShape = new RectangleShape();
                    break;
                case ToolType.Ellipse:
                    previewShape = new Ellipse();
                    break;
            }

            if (previewShape != null)
            {
                previewShape.StartPoint = point;
                previewShape.EndPoint = point;
                previewShape.Color = currentColor;
            }
        }

        public void UpdateDrawing(Point point)
        {
            if (isDrawing && previewShape != null)
            {
                previewShape.EndPoint = point;
            }
        }

        public void FinishDrawing()
        {
            if (isDrawing && previewShape != null)
            {
                shapes.Add(previewShape);
                selectedShape = previewShape;
                previewShape = null;
            }
            isDrawing = false;
        }

        public void SelectShape(Point point)
        {
            foreach (var shape in shapes)
            {
                shape.IsSelected = false;
            }

            selectedShape = shapes.LastOrDefault(shape => shape.Contains(point));

            if (selectedShape != null)
            {
                selectedShape.IsSelected = true;
            }
        }

        public void StartMoving(Point point)
        {
            if (selectedShape != null)
            {
                isMoving = true;
                drawingStartPoint = point;
            }
        }

        public void UpdateMoving(Point point)
        {
            if (isMoving && selectedShape != null)
            {
                int deltaX = point.X - drawingStartPoint.X;
                int deltaY = point.Y - drawingStartPoint.Y;
                selectedShape.Move(deltaX, deltaY);
                drawingStartPoint = point;
            }
        }

        public void FinishMoving()
        {
            isMoving = false;
        }

        public void DeleteSelectedShape()
        {
            if (selectedShape != null)
            {
                shapes.Remove(selectedShape);
                selectedShape = null;
            }
        }

        public void Draw(Graphics graphics)
        {
            graphics.Clear(Color.White);

            foreach (var shape in shapes)
            {
                shape.Draw(graphics);
            }

            if (isDrawing && previewShape != null)
            {
                previewShape.Draw(graphics);
            }
        }
    }
}