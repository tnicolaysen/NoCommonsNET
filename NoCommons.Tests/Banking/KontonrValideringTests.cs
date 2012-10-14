using System;
using System.Text;
using NUnit.Framework;
using NoCommons.Banking;
using NoCommons.Common;

namespace NoCommons.Tests.Banking
{
    [TestFixture]
    public class KontonrValideringTests
    {
        private const string ValidKontonummer = "99990000006";
        private const string KontonummerWithInvalidChecksum = "99990000005";

        [TestCase("97104133219")]
        [TestCase("97105302049")]
        [TestCase("97104008309")]
        [TestCase("97102749069")]
        [TestCase(ValidKontonummer)]
        public void TestValidNumberEndingOn9(string kontonrEndingOn9)
        {
            Assert.IsTrue(KontonummerValidator.IsValid(kontonrEndingOn9));
        }

        [TestCase("", "Blank kontonummer")]
        [TestCase(KontonummerWithInvalidChecksum, "Invalid checksum")]
        public void TestIsInvalid(string kontonr, string description)
        {
            Assert.IsFalse(KontonummerValidator.IsValid(kontonr));
        }

        [TestCase("123456789012", "Wrong length")]
        [TestCase("abcdefghijk", "Not digits")]
        public void TestInvalidKontonummer(string kontoNr, string description)
        {
            try
            {
                KontonummerValidator.ValidateSyntax(kontoNr);
                Assert.Fail(description);
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, StringNumberValidator.SyntaxErrorMessage);
            }
        }

        [Test]
        public void TestInvalidKontonummerWrongChecksum()
        {
            try
            {
                KontonummerValidator.ValidateChecksum(KontonummerWithInvalidChecksum);
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, StringNumberValidator.InvalidChecksumErrorMessage);
            }
        }

        [Test]
        public void TestInvalidAccountTypeWrongLength()
        {
            var b = new StringBuilder(KontonummerValidator.KontogruppeNumDigits + 1);
            for (int i = 0; i < KontonummerValidator.KontogruppeNumDigits + 1; i++)
            {
                b.Append("0");
            }
            try
            {
                KontonummerValidator.ValidateAccountTypeSyntax(b.ToString());
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, StringNumberValidator.SyntaxErrorMessage);
            }
        }

        [Test]
        public void TestInvalidAccountTypeNotDigits()
        {
            var b = new StringBuilder(KontonummerValidator.KontogruppeNumDigits);
            for (int i = 0; i < KontonummerValidator.KontogruppeNumDigits; i++)
            {
                b.Append("A");
            }
            try
            {
                KontonummerValidator.ValidateAccountTypeSyntax(b.ToString());
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, StringNumberValidator.SyntaxErrorMessage);
            }
        }

        [Test]
        public void TestInvalidRegisternummerNotDigits()
        {
            var b = new StringBuilder(KontonummerValidator.RegisternummerNumDigits);
            for (int i = 0; i < KontonummerValidator.RegisternummerNumDigits; i++)
            {
                b.Append("A");
            }
            try
            {
                KontonummerValidator.ValidateRegisternummerSyntax(b.ToString());
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, StringNumberValidator.SyntaxErrorMessage);
            }
        }

        [Test]
        public void TestInvalidRegisternummerWrongLength()
        {
            var b = new StringBuilder(KontonummerValidator.RegisternummerNumDigits + 1);
            for (int i = 0; i < KontonummerValidator.RegisternummerNumDigits + 1; i++)
            {
                b.Append("0");
            }
            try
            {
                KontonummerValidator.ValidateRegisternummerSyntax(b.ToString());
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, StringNumberValidator.SyntaxErrorMessage);
            }
        }

        [Test]
        public void TestGetValidKontonummerFromInvalidKontonummerWrongChecksum()
        {
            Kontonummer knr = KontonummerValidator.GetAndForceValidKontonummer(KontonummerWithInvalidChecksum);
            Assert.IsTrue(KontonummerValidator.IsValid(knr.ToString()));
        }
    }
}
