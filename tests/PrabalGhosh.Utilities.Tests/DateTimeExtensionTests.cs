using System;
using Xunit;

namespace PrabalGhosh.Utilities.Tests
{
    public class DateTimeExtensionTests
    {
        [Fact]
        public void StartOfMonthShouldReturnCorrect()
        {
            var month = "jan";
            var year = "2020";
            var expected = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var value = (new DateTime()).GetMonthStart(month, year);
            Assert.Equal(expected, value);
        }
        
        [Fact]
        public  void EndOfMonthShouldReturnCorrect()
        {
            var month = "jan";
            var year = "2020";
            var expected = new DateTime(2020, 1, 31, 0, 0, 0, DateTimeKind.Utc);
            var value = (new DateTime()).GetMonthEnd(month, year);
            Assert.Equal(expected, value);
        }
        
        [Fact]
        public  void StartOfFebShouldReturnCorrect()
        {
            var month = "Feb";
            var year = "2020";
            var expected = new DateTime(2020, 2, 1, 0, 0, 0, DateTimeKind.Utc);
            var value = (new DateTime()).GetMonthStart(month, year);
            Assert.Equal(expected, value);
        }
        
        [Fact]
        public  void EndOfFebInLeapYearShouldReturnCorrect()
        {
            //it was a leap year
            var month = "Feb";
            var year = "2020";
            var expected = new DateTime(2020, 2, 29, 0, 0, 0, DateTimeKind.Utc);
            var value = (new DateTime()).GetMonthEnd(month, year);
            Assert.Equal(expected, value);
        }
        
        [Fact]
        public  void EndOfFebNonLeapYearShouldReturnCorrect()
        {
            var month = "FEB";
            var year = "2021";
            var expected = new DateTime(2021, 2, 28, 0, 0, 0, DateTimeKind.Utc);
            var value = (new DateTime()).GetMonthEnd(month, year);
            Assert.Equal(expected, value);
        }
    }
}