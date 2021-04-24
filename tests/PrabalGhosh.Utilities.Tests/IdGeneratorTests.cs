using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PrabalGhosh.Utilities.Tests
{
    [TestClass]
    public class IdGeneratorTests
    {
        [TestMethod]
        public void GetDefaultIdShouldPass()
        {
            var expectedLength = 16;
            var actual = IdGenerator.GetId();
            Assert.AreEqual(expectedLength, actual.Length);
        }

        [TestMethod]
        public void GetPnrShouldPass()
        {
            var expectedLength = 6;
            var actual = IdGenerator.GetPNR();
            Assert.AreEqual(expectedLength, actual.Length);
        }
    }
}