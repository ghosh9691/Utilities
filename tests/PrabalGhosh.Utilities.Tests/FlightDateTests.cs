using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrabalGhosh.Utilities.Types;

namespace PrabalGhosh.Utilities.Tests
{
    [TestClass]
    public class FlightDateTests
    {
        [TestMethod]
        public void EdtToUtcShouldPass()
        {
            var expected = new DateTime(2021, 4, 1, 4, 0, 0);
            var fd = new FlightDate(
                    new DateTime(2021, 4, 1, 0,0,0),
                    "America/New_York"
                );
            var actual = fd.ToUtc();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UtcToIstShouldPass()
        {
            var expected = new DateTime(2021, 4, 1, 5, 30, 0);
            var fd = new FlightDate(
                new DateTime(2021, 4, 1, 0, 0, 0, 0),
                "Etc/UTC"
            );
            var actual = fd.ToLocal("Asia/Kolkata");
            Assert.AreEqual(expected, actual);
        }
    }
}