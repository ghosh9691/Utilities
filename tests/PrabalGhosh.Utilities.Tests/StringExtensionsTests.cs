using System;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
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
    }
}