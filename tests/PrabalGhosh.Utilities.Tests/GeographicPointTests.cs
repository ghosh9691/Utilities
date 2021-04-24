using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrabalGhosh.Utilities.Geographic;

namespace PrabalGhosh.Utilities.Tests
{
    [TestClass]
    public class GeographicPointTests
    {
        private readonly GeographicPoint _jfk = new GeographicPoint(40.63992778, -73.77869167);
        private readonly GeographicPoint _lhr = new GeographicPoint(51.4775, -0.46138889);
        private readonly GeographicPoint _nrt = new GeographicPoint(35.76527778, 140.38555556);
        private readonly GeographicPoint _syd = new GeographicPoint(-33.946, 151.17711111);
        private readonly GeographicPoint _yvr = new GeographicPoint(49.19469722, -123.18396944);
        private readonly GeographicPoint _yyc = new GeographicPoint(51.12261389, -114.01334722);
        private readonly GeographicPoint _lax = new GeographicPoint(33.94249722, -118.40805 );
        private readonly GeographicPoint _bos = new GeographicPoint(42.36294444, -71.00638889);

        [TestMethod]
        public void JfkToLhrShouldPass()
        {
            var expected = 5545.49; //expected distance in kilometers
            var gc = _jfk.DistanceTo(_lhr);
            var actual = gc.ToKilometers();
            Assert.AreEqual(expected, actual, 0.0001);
        }

        [TestMethod]
        public void SydToYvrShouldPass()
        {
            var expected = 12480.82;
            var gc = _syd.DistanceTo(_yvr);
            var actual = gc.ToKilometers();
            Assert.AreEqual(expected, actual, 0.0001);
        }

        [TestMethod]
        public void SydToYvrAndNrtToLaxShouldIntersect()
        {
            var line1 = new GeographicLine(_syd, _yvr);
            var line2 = new GeographicLine(_nrt, _lax);

            var intxn = line1.GetIntersection(line2);
            Assert.IsNotNull(intxn);
            Assert.AreNotEqual(-1, intxn.Distance);
            Assert.IsTrue(intxn.Distance > 0);
        }
    }
}