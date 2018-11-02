using Xunit;

namespace Flitesys.Utilities.Tests
{
    public class IdGeneratorTests
    {
        [Fact]
        public void TestGetId()
        {
            var generated = IdGenerator.GetId();
            Assert.NotNull(generated);
            Assert.True(generated.Length == 16);
        }

        [Fact]
        public void TestGetPNR()
        {
            var pnr = IdGenerator.GetPNR();
            Assert.NotNull(pnr);
            Assert.True(pnr.Length == 6);
        }
    }
}