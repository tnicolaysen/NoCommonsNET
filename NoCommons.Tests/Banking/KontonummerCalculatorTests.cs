using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NoCommons.Banking;

namespace NoCommons.Tests.Banking
{
    [TestFixture]
    public class KontonummerCalculatorTests
    {
        private const int AmountOfKontonummerToCreate = 100;
        private const string TestKontogruppe = "45";
        private const string TestRegisternummer = "9710";

        [Test]
        public void Should_get_a_list_of_randomly_created_kontonummer_that_are_all_valid()
        {
            var kontonummer = KontonummerFactory.GetKontonummerList(AmountOfKontonummerToCreate).ToList();

            Assert.That(kontonummer.Count, Is.EqualTo(AmountOfKontonummerToCreate));
            Assert.That(AllKontonummerIsValid(kontonummer), Is.True, "One or more kontonummer was not valid");
        }

        [Test]
        public void Should_get_a_list_of_randomly_created_kontonummer_for_a_given_kontogruppe_that_are_all_valid()
        {
            var kontonummer = KontonummerFactory.GetKontonummerListForAccountType(TestKontogruppe, AmountOfKontonummerToCreate).ToList();

            Assert.That(kontonummer.Count, Is.EqualTo(AmountOfKontonummerToCreate));
            Assert.That(AllKontonummerIsValid(kontonummer), Is.True, "One or more kontonummer was not valid");
            Assert.That(AllKontonummerContainsKontogruppe(kontonummer), Is.True, "One or more kontonummer did not contain the correct kontogruppenummer");
        }

        [Test]
        public void Should_get_a_list_of_randomly_created_kontonummer_for_a_given_registernummer_that_are_all_valid()
        {
            var kontonummer = KontonummerFactory.GetKontonummerListForRegisternummer(TestRegisternummer, AmountOfKontonummerToCreate).ToList();

            Assert.That(kontonummer.Count, Is.EqualTo(AmountOfKontonummerToCreate));
            Assert.That(AllKontonummerIsValid(kontonummer), Is.True, "One or more kontonummer was not valid");
            Assert.That(AllKontonummerContainsRegisternummer(kontonummer), Is.True, "One or more kontonummer did not contain the correct registernummer");
        }

        #region Helpers

        private static bool AllKontonummerIsValid(IEnumerable<Kontonummer> kontonummer)
        {
            return kontonummer.All(k => KontonummerValidator.IsValid(k.ToString()));
        }

        private static bool AllKontonummerContainsKontogruppe(IEnumerable<Kontonummer> kontonummer)
        {
            return kontonummer.All(k => string.Equals(k.GetKontogruppe(), TestKontogruppe));
        }

        private static bool AllKontonummerContainsRegisternummer(IEnumerable<Kontonummer> kontonummer)
        {
            return kontonummer.All(k => string.Equals(k.GetRegisternummer(), TestRegisternummer));
        }

        #endregion
    }
}