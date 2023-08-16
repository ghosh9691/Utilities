using PrabalGhosh.Utilities;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrabalGhosh.Utilities.Enums;

namespace PrabalGhosh.Utilities.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void TestEnumGetDescription()
        {
            var expected = "IAS";
            var speedUnit = SpeedTypes.Indicated;
            var result = speedUnit.GetDescription<SpeedTypes>();
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result, false, "SpeedUnit description is not what was expected!");
        }


        [TestMethod]
        public void TestSecureHashMethod()
        {
            string testPassword = "qV8E8fjpZWfRCj";
            var salt = RandomNumberGenerator.GetBytes(16);
            var hashedPassword = testPassword.GetSecureHash(salt);

            var hashedAgain = testPassword.GetSecureHash(salt);
            Assert.IsTrue(hashedPassword.SequenceEqual(hashedAgain));
        }

        [TestMethod]
        public void TestFromAiracLatitude()
        {
            var expected = 41.0;
            var result = "N41".FromAiracLatitude();
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result, "Latitude is not what was expected!");
        }

        [TestMethod()]
        public void AiracDiscontinueDateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AiracEffectiveDateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FromAiracElevationTest()
        {
        }

        [TestMethod()]
        public void FromAiracLatitudeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FromAiracLongitudeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FromBase64Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FromMagneticVariationTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetHashTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetHashTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetSecureHashTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsEmptyTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsNotEmptyTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ToBase64Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ToInt32Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ToNullableInt32Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ToInt64Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ToNullableInt64Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ToDoubleTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ToNullableDoubleTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsDigitsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveUnwantedCharactersTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveSpecialCharactersTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsValidEmailTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetDescriptionTest()
        {
            Assert.Fail();
        }
    }
}