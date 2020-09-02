using System;
using System.Threading.Tasks;
using Xunit;

namespace PrabalGhosh.Utilities.Tests {
    public class AiracCycleTests {
        [Fact]
        public async Task TestGetStartDate () {
            string airacCycle = "1306";
            var airac = new Airac (airacCycle);
            var effective = await Task.Run (() => { return airac.GetEffectiveDate (); });
            Assert.Equal (new DateTime (2013, 5, 30, 2, 0, 0, DateTimeKind.Utc), effective);
            var discontinue = await Task.Run (() => { return airac.GetDiscontinueDate (); });
            Assert.Equal (new DateTime (2013, 6, 27, 1, 59, 59), discontinue);
        }

        [Fact]
        public async Task TestGetCurrentAirac () {
            var airac = new Airac (DateTime.UtcNow);
            var current = $"{airac.ToString()}";
            Console.WriteLine($"Current AIRAC Cycle is {current}");
            Assert.True(true);
        }
    }
}