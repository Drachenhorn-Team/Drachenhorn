using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Drachenhorn.Map.Common
{
    public class Room
    {
        #region Properties

        private int _x;

        public int X
        {
            get { return _x; }
            set
            {
                if (_x == value)
                    return;
                _x = value;
            }
        }

        private int _y;

        public int Y
        {
            get { return _y; }
            set
            {
                if (_y == value)
                    return;
                _y = value;
            }
        }

        private int _width;

        public int Width
        {
            get { return _width; }
            set
            {
                if (_width == value)
                    return;
                _width = value;
            }
        }

        private int _height;

        public int Height
        {
            get { return _height; }
            set
            {
                if (_height == value)
                    return;
                _height = value;
            }
        }

        #endregion Properties

        public Room(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
