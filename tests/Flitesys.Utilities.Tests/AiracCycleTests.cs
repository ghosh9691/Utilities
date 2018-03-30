using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Flitesys.Utilities.Tests
{
    public class AiracCycleTests
    {
        [Fact]
        public async Task TestGetStartDate()
        {
            string airacCycle = "1306";
            var airac = new Airac(airacCycle);
            var effective = airac.GetEffectiveDate();
            Assert.Equal(new DateTime(2013, 5, 30, 2, 0, 0, DateTimeKind.Utc), effective);
            var discontinue = airac.GetDiscontinueDate();
            Assert.Equal(new DateTime(2013, 6, 27, 1, 59, 59), discontinue);
        }
    }
}