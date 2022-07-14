using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrabalGhosh.Utilities.Enums;

namespace PrabalGhosh.Utilities.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void TestEnumGetDescription()
        {
            var expected = "IAS";
            var speedUnit = SpeedTypes.Indicated;
            var result = speedUnit.GetDescription<SpeedTypes>();
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result, false, "SpeedUnit description is not what was expected!");
        }    
    }
}