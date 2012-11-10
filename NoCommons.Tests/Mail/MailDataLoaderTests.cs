using System.IO;
using NUnit.Framework;
using NoCommons.Mail;

namespace NoCommons.Tests.Mail
{
public class MailDataLoaderTest {

    [SetUp]
	public void setUpLocaleAndLoadData() 
    {
        var f = Resources.Resources.tilbud5;
        using(var s = GenerateStreamFromString(f))
        {
            MailDataLoader.loadFromInputStream(s); 
        }
	}

    public Stream GenerateStreamFromString(string s)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }

	[Test]
	public void testAntallPoststed()
	{
		Assert.AreEqual(1852, MailValidator.getAntallPoststed());
	}

	[Test]
	public void testAntallPostnummer() {
		Assert.AreEqual(4586, MailValidator.getAntallPostnummer());
	}

	[Test]
	public void testAntallPostnummerForHamar() {
		var options = MailValidator.getPostnummerForPoststed("HAMAR");
		Assert.AreEqual(17, options.Count);
	}

	[Test]
	public void testPoststedForPostnummer2315() {
		Assert.AreEqual("HAMAR", MailValidator.getPoststedForPostnummer("2315").ToString());
	}

	[Test]
	public void testLoadDataFromResource() {
		var success = MailDataLoader.loadFromResource();
		Assert.IsTrue(success);
	}
}
}
