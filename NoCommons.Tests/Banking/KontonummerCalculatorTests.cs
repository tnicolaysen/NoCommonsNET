using System.Linq;
using NUnit.Framework;
using NoCommons.Banking;

namespace NoCommons.Tests.Banking
{
    [TestFixture]
    public class KontonummerCalculatorTests
    {
        private const int LIST_LENGTH = 100;
        private const string TEST_ACCOUNT_TYPE = "45";
        private const string TEST_REGISTERNUMMER = "9710";

        [Test]
        public void testGetKontonummerList()
        {
            var options = KontonummerFactory.GetKontonummerList(LIST_LENGTH);
            Assert.AreEqual(LIST_LENGTH, options.Count());
            foreach (Kontonummer k in options)
            {
                Assert.IsTrue(KontonummerValidator.IsValid(k.ToString()));
            }
        }

        [Test]
        public void testGetKontonummerListForAccountType()
        {
            var options = KontonummerFactory.GetKontonummerListForAccountType(TEST_ACCOUNT_TYPE, LIST_LENGTH);
            Assert.AreEqual(LIST_LENGTH, options.Count());
            foreach (Kontonummer option in options)
            {
                Assert.IsTrue(KontonummerValidator.IsValid(option.ToString()), "Invalid kontonr. ");
                Assert.IsTrue(option.GetKontogruppe().Equals(TEST_ACCOUNT_TYPE), "Invalid account type. ");
            }
        }

        [Test]
        public void testGetKontonummerListForRegisternummer()
        {
            var options = KontonummerFactory.GetKontonummerListForRegisternummer(TEST_REGISTERNUMMER, LIST_LENGTH);
            Assert.AreEqual(LIST_LENGTH, options.Count());
            foreach (Kontonummer option in options)
            {
                
                Assert.IsTrue(KontonummerValidator.IsValid(option.ToString()));
                Assert.IsTrue(option.GetRegisternummer().Equals(TEST_REGISTERNUMMER));
            }
        }
    }


}