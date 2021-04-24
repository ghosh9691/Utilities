using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrabalGhosh.Utilities.Aviation;

namespace PrabalGhosh.Utilities.Tests
{
    [TestClass]
    public class AtmosphereTests
    {

        [TestMethod]
        public void ThetaAboveTropopauseShouldPass()
        {
            var expected = 0.7519;
            var altitude = 37000;
            var actual = Atmosphere.Theta(altitude);
            Assert.AreEqual(expected, actual, 0.0001);
        }
    }
}