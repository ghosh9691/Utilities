using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Flitesys.Utilities.Tests
{
    public class StringExtensionsTests
    {
        [Fact]
        public void TestFromAiracLatitude()
        {
            double expected = 40.63975;
            var testLat = "N40382310";
            var result = testLat.FromAiracLatitude();
            Assert.True(Math.Abs(result - expected) <= 0.000001);
        }

        [Fact]
        public void TestFromAiracLatitudeSouthernHemisphere()
        {
            double expected = -40.63975;
            var testLat = "S40382310";
            var result = testLat.FromAiracLatitude();
            Assert.True(Math.Abs(result - expected) <= 0.000001);
        }

        [Fact]
        public void TestFromAiracLongitudeWesternHemisphere()
        {
            double expected = -73.778925;
            var testLat = "W073464413";
            var result = testLat.FromAiracLongitude();
            Assert.True(Math.Abs(result - expected) <= 0.000001);
        }

        [Fact]
        public void TestFromAiracLongitudeEasternHemisphere()
        {
            double expected = 73.778925;
            var testLat = "E073464413";
            var result = testLat.FromAiracLongitude();
            Assert.True(Math.Abs(result - expected) <= 0.000001);
        }
    }
}