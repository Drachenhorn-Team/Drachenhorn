using System;
using System.Collections.Generic;
using System.Text;
using Drachenhorn.Map.Common;

namespace Drachenhorn.Map.BSPT
{
    public static class BSPTManager
    {
        public static TileType[,] GenerateMap(int width, int height)
        {
            var leaf = new Leaf(0, 0, width, height);

            var grid = new TileType[width, height];
            leaf.DrawToGrid(ref grid);

            return grid;
        }
    }
}
