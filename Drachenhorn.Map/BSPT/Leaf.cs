using System;
using Drachenhorn.Map.Random;

namespace Drachenhorn.Map.BSPT
{
    public class Leaf
    {
        private static readonly int MIN_SIZE = 5;

        #region c'tor

        public Leaf(int top, int left, int height, int width)
        {
            _top = top;
            _left = left;
            _width = width;
            _height = height;
        }

        #endregion

        #region Properties

        internal readonly int _top, _left, _width, _height;
        internal Leaf Dungeon;
        internal Leaf LeftChild;
        internal Leaf RightChild;

        #endregion

        internal bool Split()
        {
            if (LeftChild != null) //if already split, bail out
                return false;
            var horizontal = Randomizer.Get(0, 1) == 0; //direction of split
            var max = (horizontal ? _height : _width) - MIN_SIZE; //maximum height/width we can split off
            if (max <= MIN_SIZE) // area too small to split, bail out
                return false;
            var split = Randomizer.Get(0, max); // generate split point 
            if (split < MIN_SIZE) // adjust split point so there's at least MIN_SIZE in both partitions
                split = MIN_SIZE;
            if (horizontal)
            {
                //populate child areas
                LeftChild = new Leaf(_top, _left, split, _width);
                RightChild = new Leaf(_top + split, _left, _height - split, _width);
            }
            else
            {
                LeftChild = new Leaf(_top, _left, _height, split);
                RightChild = new Leaf(_top, _left + split, _height, _width - split);
            }

            return true; //split successful
        }

        public void GenerateDungeon()
        {
            if (LeftChild != null)
            {
                //if current are has child areas, propagate the call
                LeftChild.GenerateDungeon();
                RightChild.GenerateDungeon();
            }
            else
            {
                // if leaf node, create a dungeon within the minimum size constraints
                var dungeonTop = _height - MIN_SIZE <= 0 ? 0 : Randomizer.Get(0, _height - MIN_SIZE);
                var dungeonLeft = _width - MIN_SIZE <= 0 ? 0 : Randomizer.Get(0, _width - MIN_SIZE);

                var dungeonHeight = Math.Max(Randomizer.Get(0, _height - dungeonTop), MIN_SIZE);

                var dungeonWidth = Math.Max(Randomizer.Get(0, _width - dungeonLeft), MIN_SIZE);

                Dungeon = new Leaf(_top + dungeonTop, _left + dungeonLeft, dungeonHeight, dungeonWidth);
            }
        }
    }
}