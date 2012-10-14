using System;
using System.Collections.Generic;
using System.Text;

namespace NoCommons.Banking
{
    /// <summary>
    /// Creates valid Kontonummer instances.
    /// </summary>
    public static class KontonummerFactory
    {
        private static IEnumerable<Kontonummer> CreateKontonummerWithGenerator(int amountToCreate, string registerNummer = null, string kontogruppeNummer = null)
        {
            var returnedNumbers = 0;
            while (returnedNumbers < amountToCreate)
            {
                Kontonummer kontoNr;
                try
                {
                    var generatedKontonummer = KontonummerDigitGenerator.GenerateKontonummer(registerNummer, kontogruppeNummer);
                    kontoNr = KontonummerValidator.GetAndForceValidKontonummer(generatedKontonummer);
                }
                catch (ArgumentException)
                {
                    continue; // this number has no valid checksum
                }

                ++returnedNumbers;
                yield return kontoNr;
            }
        }

        /// <param name="kontogruppeNummer">A String representing the AccountType to use for all Kontonummer instances</param>
        /// <param name="length">Specifies the number of Kontonummer instances to create in the returned List</param>
        /// <returns>A list with random, but syntactically valid, Kontonummer instances for a given kontogruppe.</returns>
        public static IEnumerable<Kontonummer> GetKontonummerListForAccountType(string kontogruppeNummer, int length)
        {
            KontonummerValidator.ValidateAccountTypeSyntax(kontogruppeNummer);
            return CreateKontonummerWithGenerator(length, kontogruppeNummer: kontogruppeNummer);
        }

        /// <param name="registerNummer">A String representing the Registernummer to use for all Kontonummer instances</param>
        /// <param name="length">Specifies the number of Kontonummer instances to create in the returned List</param>
        /// <returns>A list with random, but syntactically valid, Kontonummer instances for a given registernummer.</returns>
        public static IEnumerable<Kontonummer> GetKontonummerListForRegisternummer(string registerNummer, int length)
        {
            KontonummerValidator.ValidateRegisternummerSyntax(registerNummer);
            return CreateKontonummerWithGenerator(length, registerNummer: registerNummer);
        }

        /// <param name="length">Specifies the number of Kontonummer instances to create in the returned List</param>
        /// <returns>a List with completely random but syntactically valid Kontonummer</returns>
        public static IEnumerable<Kontonummer> GetKontonummerList(int length)
        {
            return CreateKontonummerWithGenerator(length);
        }
    }

    public static class KontonummerDigitGenerator
    {
        private static readonly Random RandomGenerator = new Random();

        public static string GenerateKontonummer(string registerNummerToUse = null, string kontogruppeNummerToUse = null)
        {
            string registerNummer = registerNummerToUse ?? GetRandomNumbers(amount: 4);
            string kontotypeNummer = kontogruppeNummerToUse ?? GetRandomNumbers(amount: 2);
            string kundeNummer = GetRandomNumbers(amount: 5);

            return string.Format("{0}{1}{2}", registerNummer, kontotypeNummer, kundeNummer);
        }

        private static string GetRandomNumbers(int amount, int min = 0, int max = 10)
        {
            var sb = new StringBuilder(amount);
            for (int i = 0; i < amount; i++)
                sb.Append(GetRandomNumber(min, max));

            return sb.ToString();
        }

        private static int GetRandomNumber(int min = 0, int max = 10)
        {
            return RandomGenerator.Next(min, max);
        }
    }
}