using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NoCommons.Mail
{
    public class MailDataLoader
    {
        public static void loadFromInputStream(Stream ist)
        {
            if (ist == null)
            {
                throw new ArgumentException();
            }
            var poststedMap = new Dictionary<Poststed, List<Postnummer>>();
            var postnummerMap = new Dictionary<Postnummer, Poststed>();
            using (var isr = new StreamReader(ist, Encoding.UTF8))
            {
                string line;
                while ((line = isr.ReadLine()) != null)
                {
                    var st = line.Split('\t');
                    var pn = MailValidator.getPostnummer(st[0]);
                    var ps = new Poststed(st[1]);

                    // add to poststedMap
                    var postnummerList = new List<Postnummer>();
                    if (poststedMap.ContainsKey(ps))
                    {
                        postnummerList = poststedMap[ps];
                    }
                    if (!postnummerList.Contains(pn))
                    {
                        postnummerList.Add(pn);
                    }

                    if(poststedMap.ContainsKey((ps)))
                    {
                        poststedMap[ps] = postnummerList;
                    }
                    else
                    {
                        poststedMap.Add(ps, postnummerList);
                    }
                    
                    // add to postnummerMap
                    if (!postnummerMap.ContainsKey(pn))
                    {
                        postnummerMap.Add(pn, ps);
                    }
                }
            }
            MailValidator.setPoststedMap(poststedMap);
            MailValidator.setPostnummerMap(postnummerMap);
        }

        public static bool loadFromResource()
        {
            bool success = false;
            Stream ist = null;
            try
            {
                ist = GenerateStreamFromString(Resources.Resources.tilbud5);

                loadFromInputStream(ist);
                success = true;
            }
            catch (IOException e)
            {
                // ignore
            }
            finally
            {
                try
                {
                    if (ist != null)
                    {
                        ist.Close();
                    }
                }
                catch (IOException e)
                {
                    // ignore
                }
            }
            return success;
        }

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

    }
}