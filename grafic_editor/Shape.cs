using System.Drawing;

namespace VectorEditor
{
    public abstract class Shape
    {
        protected Point startPoint;
        protected Point endPoint;
        protected Color color;
        protected bool isSelected;

        public Point StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }

        public Point EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }

        public abstract void Draw(Graphics graphics);
        public abstract bool Contains(Point point);
        public abstract void Move(int deltaX, int deltaY);
    }
}