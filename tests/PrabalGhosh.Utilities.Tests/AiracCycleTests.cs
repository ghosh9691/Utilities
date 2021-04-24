using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrabalGhosh.Utilities.Aviation;

namespace PrabalGhosh.Utilities.Tests
{
    [TestClass]
    public class AiracCycleTests
    {
        [TestMethod]
        public void GetEffectiveDateShouldPass()
        {
            var airacCycle = "2101";
            var airac = new Airac(airacCycle);
            Assert.IsNotNull(airac);
            var effectiveDate = airac.GetEffectiveDate();
            //expected effective date is 28/JAN/2021 and time is 0200z
            var expected = new DateTime(2021, 01, 28, 2, 0, 0, DateTimeKind.Utc);
            Assert.AreEqual(expected, effectiveDate);
        }
        
        [TestMethod]
        public void GetDiscontinueDateShouldPass()
        {
            var airacCycle = "2101";
            var airac = new Airac(airacCycle);
            Assert.IsNotNull(airac);
            var discontinueDate = airac.GetDiscontinueDate();
            //expected effective date is 25/FEB/2021 and time is 0159z
            var expected = new DateTime(2021, 02, 25, 1, 59, 59, DateTimeKind.Utc);
            Assert.AreEqual(expected, discontinueDate);
        }

        [TestMethod]
        public void ExtraCycleShouldPass()
        {
            var testDate = new DateTime(2020, 12, 31, 10, 0, 0, DateTimeKind.Utc);
            var airac = new Airac(testDate);
            Assert.IsNotNull(airac);
            var expected = "2014";
            var actual = airac.ToString();
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void InvalidAiracCycleShouldThrow()
        {
            var cycleDate = "2019";
            Assert.ThrowsException<ArgumentException>(() => new Airac(cycleDate));
        }
    }
}