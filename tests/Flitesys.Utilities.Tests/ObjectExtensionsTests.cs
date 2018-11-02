using Xunit;

namespace Flitesys.Utilities.Tests
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public void ObjectIsNull()
        {
            string isNotNull = "This is not null";
            string isNull = null;

            Assert.True(isNull.IsNull());
            Assert.False(isNotNull.IsNull());
        }

        [Fact]
        public void ObjectIsNotNull()
        {
            string isNotNull = "This is not null";
            string isNull = null;

            Assert.False(isNull.IsNotNull());
            Assert.True(isNotNull.IsNotNull());
        }
    }
}