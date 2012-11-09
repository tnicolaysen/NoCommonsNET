using System.Collections.Generic;
using NUnit.Framework;
using NoCommons.Mail;

namespace NoCommons.Tests.Mail
{
    public class MailValidatorTest
    {
        private readonly Poststed OSLO = new Poststed("Oslo");
        private readonly Poststed HAMAR = new Poststed("Hamar");
        private readonly Postnummer PN0102 = MailValidator.getPostnummer("0102");
        private readonly Postnummer PN0357 = MailValidator.getPostnummer("0357");
        private readonly Postnummer PN0457 = MailValidator.getPostnummer("0457");
        private readonly Postnummer PN2316 = MailValidator.getPostnummer("2316");
        private readonly Postnummer PN2315 = MailValidator.getPostnummer("2315");

        [SetUp]
        public void setUpMailValidator()
        {
            var poststedMap = new Dictionary<Poststed, List<Postnummer>>();
            var hamarList = new List<Postnummer>();
            hamarList.Add(PN2315);
            hamarList.Add(PN2316);
            poststedMap.Add(HAMAR, hamarList);
            var osloList = new List<Postnummer>();
            osloList.Add(PN0457);
            osloList.Add(PN0357);
            osloList.Add(PN0102);
            poststedMap.Add(OSLO, osloList);
            MailValidator.setPoststedMap(poststedMap);

            var postnummerMap = new Dictionary<Postnummer, Poststed>();
            postnummerMap.Add(PN2315, HAMAR);
            postnummerMap.Add(PN2316, HAMAR);
            postnummerMap.Add(PN0357, OSLO);
            postnummerMap.Add(PN0457, OSLO);
            postnummerMap.Add(PN0102, OSLO);
            MailValidator.setPostnummerMap(postnummerMap);
        }

        [Test]
        public void testGetPostnummerForPoststed()
        {
            var options = MailValidator.getPostnummerForPoststed("Hamar");
            Assert.AreEqual(2, options.Count);
            options = MailValidator.getPostnummerForPoststed("Oslo");
            Assert.AreEqual(3, options.Count);
        }

        [Test]
        public void testGetPostnummerForPoststedWithDifferentCase()
        {
            var options = MailValidator.getPostnummerForPoststed("HAMAR");
            Assert.AreEqual(2, options.Count);
        }

        [Test]
        public void testGetPostnummerForPoststedThatDoesNotExist()
        {
            var options = MailValidator.getPostnummerForPoststed("StedSomIkkeFinnes");
            Assert.AreEqual(0, options.Count);
        }

        [Test]
        public void testGetPoststedForPostnummer()
        {
            Assert.AreEqual(HAMAR, MailValidator.getPoststedForPostnummer("2315"));
        }

        [Test]
        public void testValidPostnummer()
        {
            Assert.IsTrue(MailValidator.isValidPostnummer("0102"));
            Assert.IsTrue(MailValidator.isValidPostnummer("2315"));
        }

        [Test]
        public void testInvalidPostnummerNotDigits()
        {
            Assert.IsFalse(MailValidator.isValidPostnummer("ABCD"));
        }

        [Test]
        public void testInvalidPostnummerLength()
        {
            Assert.IsFalse(MailValidator.isValidPostnummer("012"));
        }
    }
}