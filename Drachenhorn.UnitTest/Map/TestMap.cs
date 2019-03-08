using Drachenhorn.Map.BSPT;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drachenhorn.UnitTest.Map
{
    [TestClass]
    public class TestMap
    {
        [TestMethod]
        public void BSPTMap()
        {
            var grid = LeafGenerator.GenerateLeaf();
        }
    }
}