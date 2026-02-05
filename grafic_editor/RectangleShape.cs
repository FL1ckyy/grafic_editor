using System.Drawing;
using System;

namespace VectorEditor
{
    public class RectangleShape : Shape
    {
        public override void Draw(Graphics graphics)
        {
            int x = Math.Min(startPoint.X, endPoint.X);
            int y = Math.Min(startPoint.Y, endPoint.Y);
            int width = Math.Abs(endPoint.X - startPoint.X);
            int height = Math.Abs(endPoint.Y - startPoint.Y);

            using (Pen pen = new Pen(color, 2))
            using (Brush brush = new SolidBrush(Color.FromArgb(50, color)))
            {
                graphics.FillRectangle(brush, x, y, width, height);
                if (isSelected)
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                }
                graphics.DrawRectangle(pen, x, y, width, height);
            }
        }

        public override bool Contains(Point point)
        {
            int x = Math.Min(startPoint.X, endPoint.X);
            int y = Math.Min(startPoint.Y, endPoint.Y);
            int width = Math.Abs(endPoint.X - startPoint.X);
            int height = Math.Abs(endPoint.Y - startPoint.Y);

            Rectangle rect = new Rectangle(x, y, width, height);
            return rect.Contains(point);
        }

        public override void Move(int deltaX, int deltaY)
        {
            startPoint = new Point(startPoint.X + deltaX, startPoint.Y + deltaY);
            endPoint = new Point(endPoint.X + deltaX, endPoint.Y + deltaY);
        }
    }
}