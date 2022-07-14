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
            var speedUnit = SpeedUnits.Indicated;
            var result = speedUnit.GetDescription<SpeedUnits>();
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result, false, "SpeedUnit description is not what was expected!");
        }    
    }
}