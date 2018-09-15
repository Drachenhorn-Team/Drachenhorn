using System;
using Drachenhorn.Map.BSPT;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drachenhorn.UnitTest
{
    [TestClass]
    public class TestMap
    {
        [TestMethod]
        public void BSPTMap()
        {
            var grid = BSPTManager.GenerateMap(50, 50);
        }
    }
}
