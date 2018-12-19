using System.Collections.Generic;
using Drachenhorn.Map.Common;
using Drachenhorn.Map.Random;

namespace Drachenhorn.Map.BSPT
{
    public static class LeafGenerator
    {
        public static TileType[,] GenerateLeaf()
        {
            var leafs = new List<Leaf>(); // flat rectangle store to help pick a random one
            var root = new Leaf(0, 0, 60, 120); //
            leafs.Add(root); //populate rectangle store with root area
            while (leafs.Count < 19)
            {
                // this will give us 10 leaf areas
                var splitIdx = Randomizer.Get(0, leafs.Count - 1); // choose a random element
                var toSplit = leafs[splitIdx];
                if (toSplit.Split())
                {
                    //attempt to split
                    leafs.Add(toSplit.LeftChild);
                    leafs.Add(toSplit.RightChild);
                }
            }

            root.GenerateDungeon(); //generate dungeons

            return PrintDungeons(leafs);
        }


        private static TileType[,] PrintDungeons(IList<Leaf> leafes)
        {
            var lines = new TileType[60, 120];

            //for (int i = 0; i < lines.GetLength(0); i++)
            //{
            //    lines[i] = new TileType[120];
            //    for (int j = 0; j < 120; j++)
            //        lines[i][j] = TileType.Wall;
            //}

            foreach (var leaf in leafes)
            {
                if (leaf.Dungeon == null)
                    continue;
                var d = leaf.Dungeon;
                for (var i = 0; i < d._height; i++)
                for (var j = 0; j < d._width; j++)

                    lines[d._top + i, d._left + j] = TileType.Floor;
            }

            return lines;
        }
    }
}