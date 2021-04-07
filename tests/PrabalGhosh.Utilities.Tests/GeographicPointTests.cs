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
        [Fact]
        public void TestAntiPodalPoints()
        {
            GeographicPoint jfk = new GeographicPoint(40.639801, -73.7789002);
            GeographicPoint pek = new GeographicPoint(40.080101, 116.5849991);

            GeographicResult result = jfk.DistanceTo(pek);
            Assert.NotNull(result);
            double expectedDistance = 11003766.14;
            Assert.True(Math.Abs(result.Distance - expectedDistance) <= 0.001);
            double expectedInitialCourse = 352.0;
            Assert.True(Math.Abs(result.InitialCourse - expectedInitialCourse) <= 0.001);
        }

        [Fact]
        public void TestDirect()
        {
            GeographicPoint jfk = new GeographicPoint(40.639801, -73.7789002);
            GeographicPoint result = jfk.DestinationPoint(51.381, 5554539.949);
            Assert.NotNull(result);
        }

        [Fact]
        public void TestJFKToLHR()
        {
            GeographicPoint jfk = new GeographicPoint(40.639801, -73.7789002);
            GeographicPoint lhr = new GeographicPoint(51.4706001, -0.461941);

            GeographicResult result = jfk.DistanceTo(lhr);
            Assert.NotNull(result);
            double expectedDistance = 5554539.95;  //in meters
            Assert.True(Math.Abs(result.Distance - expectedDistance) <= 0.001);
            double expectedInitialCourse = 51.0;
            Assert.True(Math.Abs(result.InitialCourse - expectedInitialCourse) <= 0.001);
            var toNM = result.ToNauticalMiles();
            var toSM = result.ToStatuteMiles();
            var toKM = result.ToKilometers();
            Assert.Equal(5554.54, toKM, 2);
            Assert.Equal(3451.43, toSM, 2);
            Assert.Equal(2999.21, toNM, 2);
            var gr = jfk.GetGreatCircle(lhr);
            Assert.NotNull(gr);
        }

        [Fact]
        public void TestLHRToSYD()
        {
            GeographicPoint lhr = new GeographicPoint(51.4706001, -0.461941);
            GeographicPoint syd = new GeographicPoint(-33.9460983, 151.177002);

            GeographicResult result = lhr.DistanceTo(syd);
            Assert.NotNull(result);
            double expectedDistance = 17016029.30; //in meters; using Vicenty's formula
            Assert.True(Math.Abs(result.Distance - expectedDistance) <= 0.001);
            double expectedInitialCourse = 60.0;
            Assert.True(Math.Abs(result.InitialCourse - expectedInitialCourse) <= 0.001);
        }

        [Fact]
        public void AAECloserThanAAUForVABBToVIDP()
        {
            var gf = NtsGeometryServices.Instance.CreateGeometryFactory(4326);
            var orig = gf.CreatePoint(new Coordinate(72.86611111, 19.09166667));    //VABB
            var dest = gf.CreatePoint(new Coordinate(77.11222222, 28.56861111));    //VIDP
            var point1 = gf.CreatePoint(new Coordinate(75.40520833, 19.861125));    //AAU
            var point2 = gf.CreatePoint(new Coordinate(72.62908333, 23.06823056));  //AAE
            var expected1 = 215254.65;    //meter
            var expected2 = 184354.04;    //meter
            var gcTrack = new GeographicLine()
            {
                Start = new GeographicPoint(orig), 
                End = new GeographicPoint(dest)
            };
            var aau = new GeographicPoint(point1);
            var aae = new GeographicPoint(point2);
            var actual1 = aau.Intercept(gcTrack);
            var actual2 = aae.Intercept(gcTrack);
            Assert.True(Math.Abs(actual1.Distance - expected1) <= 0.001);
            Assert.True(Math.Abs(actual2.Distance - expected2) <= 0.001);
            Assert.True(actual2.Distance < actual1.Distance);
        }

        [Fact]
        public void TestHNLDistanceFromNRTToLAX()
        {
            var nrt = new GeographicPoint(35.76527778, 140.38555556);
            var lax = new GeographicPoint(33.94249722, -118.40805);
            var hnl = new GeographicPoint(21.31781667, -157.92022778);
            var gcTrack = new GeographicLine(nrt, lax);
            var actual = hnl.Intercept(gcTrack);
            var expected = 2811110.02;   //meters
            Assert.True(Math.Abs(actual.Distance - expected) <= 0.001);
        }

        [Fact]
        public void TestIntersectionAcross180Meridian()
        {
            var point1 = new GeographicPoint(-34.0, -118.0);
            var point2 = new GeographicPoint(55.0, -123.0);
            var point4 = new GeographicPoint(51.0, -100.0);
            var point3 = new GeographicPoint(-3, 151);
            var point5 = new GeographicPoint(52.086845142064014, -122.65508307660909);
            var point6 = new GeographicPoint(-2, 151);

            var gd = point3.DistanceTo(point5);
            var gd2 = point6.DistanceTo(point5);

            var line1 = new GeographicLine(point1, point2);
            point3 = new GeographicPoint(-2.0, 151.0);
            var line2 = new GeographicLine(point3, point4);
            var intxn = line1.Intersect(line2);
            if (intxn.IsNotNull())
            {
                ConsoleEx.WriteMessage("Success!");
            }
            point3 = new GeographicPoint(-3.0, 151.0);
            line2 = new GeographicLine(point3, point4);
            intxn = line1.Intersect(line2);
            if (!intxn.IsNotNull())
            {
                ConsoleEx.WriteMessage("Failure!");
            }
        }

        [Fact]
        public void BomToDelAndCcuToSinShouldNotIntersect()
        {
            var bom = new GeographicPoint(19.09166667, 72.86611111);
            var del = new GeographicPoint(28.56861111, 77.11222222);
            var ccu = new GeographicPoint(22.65396389, 88.446725);
            var sin = new GeographicPoint(1.35921111, 103.989325);

            var bomToDel = new GeographicLine(bom, del);
            var ccuToSin = new GeographicLine(ccu, sin);

            var intxn = bomToDel.Intersect(ccuToSin);
            Assert.Null(intxn);
        }

        [Fact]
        public void BomToDelAndCcuToDxbShouldIntersect()
        {
            var bom = new GeographicPoint(19.09166667, 72.86611111);
            var del = new GeographicPoint(28.56861111, 77.11222222);
            var ccu = new GeographicPoint(22.65396389, 88.446725);
            var dxb = new GeographicPoint(25.25277778, 55.36444444);

            var bomToDel = new GeographicLine(bom, del);
            var ccuToDxb = new GeographicLine(ccu, dxb);

            var intxn = bomToDel.Intersect(ccuToDxb);
            Assert.NotNull(intxn);
        }
    }
}