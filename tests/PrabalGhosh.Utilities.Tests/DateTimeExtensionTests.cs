using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PrabalGhosh.Utilities.Tests
{
    [TestClass]
    public class DateTimeExtensionTests
    {
        [TestMethod]
        public void StartOfMonthShouldPass()
        {
            var month = "jan";
            var year = "2021";
            var expected = new DateTime(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var actual = (new DateTime()).GetMonthStart(month, year);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EndOfMonthShouldPass()
        {
            var month = "jan";
            var year = "2021";
            var expected = new DateTime(2021, 1, 31, 0, 0, 0, DateTimeKind.Utc);
            var actual = (new DateTime()).GetMonthEnd(month, year);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EndOfFebInLeapYearShouldPass()
        {
            var month = "feb";
            var year = "2020";
            var expected = new DateTime(2020, 2, 29, 0, 0, 0, DateTimeKind.Utc);
            var actual = (new DateTime()).GetMonthEnd(month, year);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EndOfFebNonLeapYearShouldPass()
        {
            var month = "feb";
            var year = "2021";
            var expected = new DateTime(2021, 2, 28, 0, 0, 0, DateTimeKind.Utc);
            var actual = (new DateTime()).GetMonthEnd(month, year);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GarbageDateShouldFail()
        {
            var month = "janu";
            var year = "21";
            var expected = DateTime.MinValue;
            var actual = (new DateTime()).GetMonthStart(month, year);
            Assert.AreEqual(expected, actual);
            actual = (new DateTime()).GetMonthEnd(month, year);
            Assert.AreEqual(expected, actual);
            month = "jaf";
            actual = (new DateTime()).GetMonthStart(month, year);
            Assert.AreEqual(expected, actual);
            actual = (new DateTime()).GetMonthEnd(month, year);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetSafeMinimumShouldPass()
        {
            var expectedLocal = new DateTime(1753, 1, 1, 0, 0, 0, DateTimeKind.Local);
            var expectedZulu = new DateTime(1753, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var actualLocal = (new DateTime()).GetSafeMinimumDateLocal();
            var actualZulu = (new DateTime()).GetSafeMinimumDateZulu();
            Assert.AreEqual(expectedLocal, actualLocal);
            Assert.AreEqual(expectedZulu, actualZulu);
        }

        [TestMethod]
        public void ParseDateTimeShouldPass()
        {
            var expected = new DateTime(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var actual = (new DateTime()).SafeParse("01-JAN-2021");
            Assert.AreEqual(expected, actual);
        }
    }
}