using System;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PrabalGhosh.Utilities.Geographic;
using Xunit;

namespace PrabalGhosh.Utilities.Tests
{
    /// <summary>
    /// The distance between 2 points are computed using Vincenty's formula
    /// and is expressed in meters. For more details, see
    /// https://en.wikipedia.org/wiki/Vincenty's_formulae
    /// </summary>
    public class GeographicPointTests
    {
        private readonly GeographicPoint _jfk = new GeographicPoint(40.63992778, -73.77869167);
        private readonly GeographicPoint _lhr = new GeographicPoint(51.4775, -0.46138889);
        private readonly GeographicPoint _nrt = new GeographicPoint(35.76527778, 140.38555556);
        private readonly GeographicPoint _syd = new GeographicPoint(-33.946, 151.17711111);
        private readonly GeographicPoint _yvr = new GeographicPoint(49.19469722, -123.18396944);
        private readonly GeographicPoint _yyc = new GeographicPoint(51.12261389, -114.01334722);
        private readonly GeographicPoint _lax = new GeographicPoint(33.94249722, -118.40805 );

        [Fact]
        public void JfkToLhrShouldPass()
        {
            var r = _jfk.DistanceTo(_lhr);
            var distInKm = r.ToKilometers();
            Assert.True(Math.Abs(5545.49 - distInKm) <= 0.001);
            var dr = _jfk.GetDestinationPoint(r.InitialCourse, r.Distance);
            Assert.NotNull(dr);
        }

        [Fact]
        public void SydToYvrShouldPass()
        {
            var r = _syd.DistanceTo(_yvr);
            var disInKm = r.ToKilometers();
            Assert.True(Math.Abs(12480.82 - disInKm) <= 0.001);
        }

        [Fact]
        public void SydToYvrAndNrtToLaxShouldIntersect()
        {
            var line1 = new GeographicLine(_syd, _yvr);
            var line2 = new GeographicLine(_nrt, _lax);
            var intxn = line1.GetIntersection(line2);
            Assert.NotNull(intxn);
        }
    }
}