using System;
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
            double expectedDistance = 11003766.139;
            Assert.True(Math.Abs(result.Distance - expectedDistance) <= 0.001);
            double expectedInitialCourse = 352.006;
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
            double expectedDistance = 5554539.949;  //in meters
            Assert.True(Math.Abs(result.Distance - expectedDistance) <= 0.001);
            double expectedInitialCourse = 51.381;
            Assert.True(Math.Abs(result.InitialCourse - expectedInitialCourse) <= 0.001);
        }

        [Fact]
        public void TestLHRToSYD()
        {
            GeographicPoint lhr = new GeographicPoint(51.4706001, -0.461941);
            GeographicPoint syd = new GeographicPoint(-33.9460983, 151.177002);

            GeographicResult result = lhr.DistanceTo(syd);
            Assert.NotNull(result);
            double expectedDistance = 17016029.303; //in meters; using Vicenty's formula
            Assert.True(Math.Abs(result.Distance - expectedDistance) <= 0.001);
            double expectedInitialCourse = 60.115;
            Assert.True(Math.Abs(result.InitialCourse - expectedInitialCourse) <= 0.001);
        }
    }
}