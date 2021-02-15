using PrabalGhosh.Utilities.Aviation;
using Xunit;

namespace PrabalGhosh.Utilities.Tests
{
    public class AtmosphereTests
    {
        [Fact]
        public async void ThetaAboveTroposphere()
        {
            var result = Atmosphere.Theta(35000);
            Assert.Equal(0.7519, result, 4);
        }
    }
}