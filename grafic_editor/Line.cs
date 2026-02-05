using System.Drawing;
using System;

namespace VectorEditor
{
    public class Line : Shape
    {
        public override void Draw(Graphics graphics)
        {
            using (Pen pen = new Pen(color, 2))
            {
                if (isSelected)
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                }
                graphics.DrawLine(pen, startPoint, endPoint);
            }
        }

        public override bool Contains(Point point)
        {
            float distance = DistanceToLine(point, startPoint, endPoint);
            return distance < 5;
        }

        public override void Move(int deltaX, int deltaY)
        {
            startPoint = new Point(startPoint.X + deltaX, startPoint.Y + deltaY);
            endPoint = new Point(endPoint.X + deltaX, endPoint.Y + deltaY);
        }

        private float DistanceToLine(Point point, Point lineStart, Point lineEnd)
        {
            float A = point.X - lineStart.X;
            float B = point.Y - lineStart.Y;
            float C = lineEnd.X - lineStart.X;
            float D = lineEnd.Y - lineStart.Y;

            float dot = A * C + B * D;
            float lenSq = C * C + D * D;
            float param = (lenSq != 0) ? dot / lenSq : -1;

            float xx, yy;

            if (param < 0)
            {
                xx = lineStart.X;
                yy = lineStart.Y;
            }
            else if (param > 1)
            {
                xx = lineEnd.X;
                yy = lineEnd.Y;
            }
            else
            {
                xx = lineStart.X + param * C;
                yy = lineStart.Y + param * D;
            }

            float dx = point.X - xx;
            float dy = point.Y - yy;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }
    }
}