using Flitesys.Utilities.Types;
using System;
using Xunit;

namespace Flitesys.Utilities.Tests
{
    public class FlightDateTests
    {
        [Fact]
        public void TestUtcToUSEastern()
        {
            FlightDate fd = new FlightDate(
                new DateTime(2018, 1, 28, 17, 0, 0),
                "Etc/UTC"
            );

            DateTime expected = new DateTime(2018, 1, 28, 12, 0, 0);
            DateTime actual = fd.ToLocal("America/New_York");

            Assert.True(actual != DateTimeOffset.MinValue.DateTime);
            Assert.True(actual == expected);
        }

        [Fact]
        public void TestUSEasternToUtc()
        {
            var fd = new FlightDate
            {
                DateTime = new DateTime(2018, 1, 28, 15, 0, 0),
                TimeZone = "America/New_York"
            };
            DateTime expected = new DateTime(2018, 1, 28, 20, 0, 0);
            DateTime actual = fd.ToUtc();

            Assert.False(actual == DateTimeOffset.MinValue.DateTime);
            Assert.True(actual == expected);
        }
    }
}