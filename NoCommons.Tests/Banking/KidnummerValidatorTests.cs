using System;
using NUnit.Framework;
using NoCommons.Banking;
using NoCommons.Common;

namespace NoCommons.Tests.Banking
{
    [TestFixture]
    public class KidnummerValidatorTests
    {
        [TestCase("2345676", "Valid MOD10")]
        [TestCase("12345678903", "Valid MOD11")]
        public void TestValidKidnummmer(string kidnummer, string description)
        {
            Assert.IsTrue(KidnummerValidator.IsValid(kidnummer), description);
        }

        [TestCase("", "Blank (invalid) kidnummer")]
        [TestCase("abcdefghijk", "No digits")]
        [TestCase("2345674", "Invalid checksum")]
        public void TestInvalidKidnummmer(string kidnummer, string description)
        {
            try
            {
                bool validationResult = KidnummerValidator.IsValid(kidnummer);
                Assert.IsFalse(validationResult, description);
            }
            catch (ArgumentException e)
            {
                Assert.That(e.Message, Is.StringContaining(StringNumberValidator.SyntaxErrorMessage));
            }
        }

        [TestCase("1", "Too short")]
        [TestCase("12345678901234567890123456", "Too long")]
        public void TestKidnummerLengthValidation(string kidnummer, string description)
        {
            try
            {
                KidnummerValidator.ValidateSyntax(kidnummer);
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                Assert.That(e.Message, Is.StringContaining(KidnummerValidator.LenghtErrorMessage));
            }
        }
    }
}