using NUnit.Framework;
using NoCommons.Banking;

namespace NoCommons.Tests.Banking
{
    [TestFixture]
    public class KontonummerTests
    {
        [SetUp]
        public void CreateTestAccountNumber()
        {
            _kontonr = new Kontonummer(TestAccountNumber);
        }

        private const string TestAccountNumber = "xxxxyyzzzzc";
        private Kontonummer _kontonr;

        [Test]
        public void Should_create_grouped_string()
        {
            Assert.That(_kontonr.GetGroupedValue(), Is.EqualTo("xxxx.yy.zzzzc"));
        }

        [Test]
        public void Should_get_kontogruppe_from_kontonr()
        {
            Assert.That(_kontonr.GetKontogruppe(), Is.EqualTo("yy"));
        }

        [Test]
        public void Should_get_kontrollsiffer_from_kontonr()
        {
            Assert.That(_kontonr.GetKontrollsiffer(), Is.EqualTo("c"));
        }

        [Test]
        public void Should_get_kundenr_from_kontonr()
        {
            Assert.That(_kontonr.GetKundenummer(), Is.EqualTo("zzzzc"));
        }

        [Test]
        public void Should_get_registernummer_from_kontonr()
        {
            Assert.That(_kontonr.GetRegisternummer(), Is.EqualTo("xxxx"));
        }
    }
}