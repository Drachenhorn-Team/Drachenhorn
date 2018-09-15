using System;
using System.Collections.Generic;
using System.Text;

namespace Drachenhorn.Map.Common
{
    public class Hallway
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

        private int _length;

        public int Length
        {
            get { return _length; }
            set
            {
                if (_length == value)
                    return;
                _length = value;
            }
        }

        private Direction _direction;

        public Direction Direction
        {
            get { return _direction; }
            set
            {
                if (_direction == value)
                    return;
                _direction = value;
            }
        }

        #endregion Properties

        public Hallway(int x, int y, int length, Direction direction)
        {
            X = x;
            Y = y;
            Length = length;
            Direction = direction;
        }
    }
}
