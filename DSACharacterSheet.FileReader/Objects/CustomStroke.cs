using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;
using System.Windows.Input;

namespace DSACharacterSheet.FileReader.Objects
{
    public class CustomStroke : BindableBase, IEnumerable<Point>
    {
        private Color _color = Color.Black;
        public Color Color
        {
            get { return _color; }
            set
            {
                if (_color == value)
                    return;
                _color = value;
                OnPropertyChanged();
            }
        }

        private double _width = 0;
        public double Width
        {
            get { return _width; }
            set
            {
                if (_width == value)
                    return;
                _width = value;
                OnPropertyChanged();
            }
        }

        private List<Point> _points = new List<Point>();
        public List<Point> Points
        {
            get { return _points; }
            private set
            {
                if (_points == value)
                    return;
                _points = value;
                OnPropertyChanged();
            }
        }

        public int Count
        {
            get { return Points.Count; }
        }

        public Point this[int index]
        {
            get { return Points[index]; }
        }

        #region c'tor

        public CustomStroke(Stroke stroke)
        {
            Color = Color.FromArgb(
                stroke.DrawingAttributes.Color.A,
                stroke.DrawingAttributes.Color.R,
                stroke.DrawingAttributes.Color.G,
                stroke.DrawingAttributes.Color.B
                );

            Width = stroke.DrawingAttributes.Width;

            foreach (var point in stroke.GetBezierStylusPoints())
                Points.Add(new Point(Convert.ToInt32(point.X), Convert.ToInt32(point.Y)));
        }

        #endregion c'tor

        public IEnumerator<Point> GetEnumerator()
        {
            return Points.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Points.GetEnumerator();
        }
    }
}
