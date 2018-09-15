using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Drachenhorn.Map.Common;
using Drachenhorn.Map.Random;

namespace Drachenhorn.Map.BSPT
{
    internal class Leaf : IDrawingItem
    {
        // Code from: https://gamedevelopment.tutsplus.com/tutorials/how-to-use-bsp-trees-to-generate-game-maps--gamedev-12268

        private const ushort MIN_LEAF_SIZE = 6;

        public int Y, X, Width, Height; // the position and size of this Leaf

        public Leaf Child1; // the Leaf's left child Leaf
        public Leaf Child2; // the Leaf's right child Leaf
        public Rectangle Room; // the room that is inside this Leaf
        public List<Rectangle> Halls; // hallways to connect this Leaf to other Leafs


        private Rectangle RandomConnector
        {
            get
            {
                var random = Randomizer.Get();
                //if (random > 0.8)
                //{
                //    return Halls == null ? Room : Halls[Randomizer.Get(0, Halls.Count - 1)];
                //}
                if (random < 0.5)
                    return Child1 == null ? Room : Child1.RandomConnector;
                else
                    return Child1 == null ? Room : Child2.RandomConnector;
            }
        }

        public Leaf(int x, int y, int width, int height)
        {
            // initialize our leaf
            X = x;
            Y = y;
            Width = width;
            Height = height;

            if (Split())
            {
                CreateRooms();
                //CreateHalls();
            }
        }

        private bool Split()
        {
            // begin splitting the leaf into two children
            if (Child1 != null || Child2 != null)
                return false; // we're already split! Abort!

            // determine direction of split
            // if the width is >25% larger than height, we split vertically
            // if the height is >25% larger than the width, we split horizontally
            // otherwise we split randomly
            bool splitH = Randomizer.Get() > 0.5;

            if (Width > Height && (double) Width / Height >= 1.25)
                splitH = false;
            else if (Height > Width && (double) Height / Width >= 1.25)
                splitH = true;

            int max = (splitH ? Height : Width) - MIN_LEAF_SIZE; // determine the maximum height or width
            if (max <= MIN_LEAF_SIZE)
                return false; // the area is too small to split any more...

            int split = Randomizer.Get(MIN_LEAF_SIZE, max); // determine where we're going to split

            // create our left and right children based on the direction of the split
            if (splitH)
            {
                Child1 = new Leaf(X, Y, Width, split);
                Child2 = new Leaf(X, Y + split, Width, Height - split);
            }
            else
            {
                Child1 = new Leaf(X, Y, split, Height);
                Child2 = new Leaf(X + split, Y, Width - split, Height);
            }

            return true; // split successful!
        }


        private void CreateRooms()
        {
            // this function generates all the rooms and hallways for this Leaf and all of its children.
            if (Child1 != null || Child2 != null)
            {
                // this leaf has been split, so go into the children leafs
                if (Child1 != null)
                {
                    Child1.CreateRooms();
                }

                if (Child2 != null)
                {
                    Child2.CreateRooms();
                }
            }
            else
            {
                // this Leaf is the ready to make a room
                Point roomSize;
                Point roomPos;
                // the room can be between 3 x 3 tiles to the size of the leaf - 2.
                roomSize = new Point(Randomizer.Get(3, Width - 2), Randomizer.Get(3, Height - 2));
                // place the room within the Leaf, but don't put it right 
                // against the side of the Leaf (that would merge rooms together)
                roomPos = new Point(Randomizer.Get(1, Width - roomSize.X - 1), Randomizer.Get(1, Height - roomSize.Y - 1));
                Room = new Rectangle(X + roomPos.X, Y + roomPos.Y, roomSize.X, roomSize.Y);
            }
        }

        public void CreateHalls()
        {
            // now we connect these two rooms together with hallways.
            // this looks pretty complicated, but it's just trying to figure out which point is where and then either draw a straight line, or a pair of lines to make a right-angle to connect them.
            // you could do some extra logic to make your halls more bendy, or do some more advanced things if you wanted.

            if (Child1 == null || Child2 == null) return;

            Halls = new List<Rectangle>();

            var room1 = Child1.RandomConnector;
            var room2 = Child2.RandomConnector;

            Point point1 = new Point(Randomizer.Get(room1.X, room1.X + room1.Width), Randomizer.Get(room1.Y, room1.Y + room1.Height));
            Point point2 = new Point(Randomizer.Get(room2.X, room2.X + room2.Width), Randomizer.Get(room2.Y, room2.Y + room2.Height));

            int w = point2.X - point1.X;
            int h = point2.Y - point1.Y;

            if (w < 0)
            {
                if (h < 0)
                {
                    if (Randomizer.Get() < 0.5)
                    {
                        Halls.Add(new Rectangle(point2.X, point1.Y, Math.Abs(w), 1));
                        Halls.Add(new Rectangle(point2.X, point2.Y, 1, Math.Abs(h)));
                    }
                    else
                    {
                        Halls.Add(new Rectangle(point2.X, point2.Y, Math.Abs(w), 1));
                        Halls.Add(new Rectangle(point1.X, point2.Y, 1, Math.Abs(h)));
                    }
                }
                else if (h > 0)
                {
                    if (Randomizer.Get() < 0.5)
                    {
                        Halls.Add(new Rectangle(point2.X, point1.Y, Math.Abs(w), 1));
                        Halls.Add(new Rectangle(point2.X, point1.Y, 1, Math.Abs(h)));
                    }
                    else
                    {
                        Halls.Add(new Rectangle(point2.X, point2.Y, Math.Abs(w), 1));
                        Halls.Add(new Rectangle(point1.X, point1.Y, 1, Math.Abs(h)));
                    }
                }
                else // if (h == 0)
                {
                    Halls.Add(new Rectangle(point2.X, point2.Y, Math.Abs(w), 1));
                }
            }
            else if (w > 0)
            {
                if (h < 0)
                {
                    if (Randomizer.Get() < 0.5)
                    {
                        Halls.Add(new Rectangle(point1.X, point2.Y, Math.Abs(w), 1));
                        Halls.Add(new Rectangle(point1.X, point2.Y, 1, Math.Abs(h)));
                    }
                    else
                    {
                        Halls.Add(new Rectangle(point1.X, point1.Y, Math.Abs(w), 1));
                        Halls.Add(new Rectangle(point2.X, point2.Y, 1, Math.Abs(h)));
                    }
                }
                else if (h > 0)
                {
                    if (Randomizer.Get() < 0.5)
                    {
                        Halls.Add(new Rectangle(point1.X, point1.Y, Math.Abs(w), 1));
                        Halls.Add(new Rectangle(point2.X, point1.Y, 1, Math.Abs(h)));
                    }
                    else
                    {
                        Halls.Add(new Rectangle(point1.X, point2.Y, Math.Abs(w), 1));
                        Halls.Add(new Rectangle(point1.X, point1.Y, 1, Math.Abs(h)));
                    }
                }
                else // if (h == 0)
                {
                    Halls.Add(new Rectangle(point1.X, point1.Y, Math.Abs(w), 1));
                }
            }
            else // if (w == 0)
            {
                if (h < 0)
                {
                    Halls.Add(new Rectangle(point2.X, point2.Y, 1, Math.Abs(h)));
                }
                else if (h > 0)
                {
                    Halls.Add(new Rectangle(point1.X, point1.Y, 1, Math.Abs(h)));
                }
            }
        }

        public void DrawToGrid(ref TileType[,] grid)
        {
            if (Child1 != null || Child2 != null)
            {
                Child1.DrawToGrid(ref grid);
                Child2.DrawToGrid(ref grid);
            }

            if (Room != null)
                for (int i = Room.X; i < Room.X + Room.Width; ++i)
                for (int j = Room.Y; j < Room.Y + Room.Height; ++j)
                    grid[i, j] = TileType.Floor;

            if (Halls != null)
                foreach (var hallway in Halls)
                {
                    for (int i = hallway.X; i < hallway.X + hallway.Width; ++i)
                    for (int j = hallway.Y; j < hallway.Y + hallway.Height; ++j)
                        grid[i, j] = TileType.Floor;
                }
        }


        public override string ToString()
        {
            return "X = " + X + " Y = " + Y + " Width = " + Width + " Height = " + Height;
        }
    }
}
