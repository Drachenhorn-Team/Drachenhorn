using System;
using System.Collections.Generic;
using System.Text;

namespace Drachenhorn.Map.Common
{
    public interface IDrawingItem
    {
        void DrawToGrid(ref TileType[,] grid);
    }
}
